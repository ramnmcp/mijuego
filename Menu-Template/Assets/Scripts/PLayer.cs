using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PLayer : MonoBehaviour
{
    float runSpeed = 5f;
    float jumpSpeed = 5f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D mybodyCollider;
    BoxCollider2D myfeetCollider;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mybodyCollider = GetComponent<CapsuleCollider2D>();
        myfeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FLipSprite();
        Jump();
    }

    void Run()
    {
        //obtener  el float del control que va de -1 a 1 
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        //Inicializar un vector de dos dimensiones que solo modifica el componente x 
        Vector2 playerVelocity = new Vector2(controlThrow*runSpeed, myRigidBody.velocity.y);
        //asignar la nueva velocidad a mi rifid body
        myRigidBody.velocity = playerVelocity;


        //bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > 0;
        ////aplicar animacion de correr seteando la condicion de " running" del animador 
        //if (playerHasHorizontalSpeed)
        //{
        //    myAnimator.SetBool("Running", true);
        //}else
        //{
        //    myAnimator.SetBool("Running", false);
        //}
      

    }
    void FLipSprite()
    {
        /*Verificar si esciste velocidad en x independientemente del signo (por eso usamos valor absoluto) 
         *  Guardamos esta verificacion como un valor booleanao (true/false)
         */
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > 0;
        if(playerHasHorizontalSpeed)
        {
            myAnimator.SetBool("running", true);
            //Si si es verdadera, toma el signo de la velocidad en x y altera la escala en esa dimension
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);

        }else
        {
            myAnimator.SetBool("running", false);
        }

    }

    void Jump()
    {

        if(!myfeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        //  !ValorBooleano

        // obtener el booleano (true/false) del boton representado por el tag"jump"
        bool isJumpButtonPressed = CrossPlatformInputManager.GetButtonDown("Jump");
        if(isJumpButtonPressed)
        {
            Vector2 jumpVelocity = new Vector2(0, jumpSpeed);
            //sumarle a la velocidad que ya tiene, mi nuevo vector de velocidad 
            myRigidBody.velocity += jumpVelocity;

        }

    }
}
