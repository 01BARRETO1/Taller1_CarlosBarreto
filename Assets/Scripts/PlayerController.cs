using System;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public float moveSpeed=5f; //Velocidad de movimiento
    public float jumpForce=7f;//Fuerza de salto
    private Rigidbody2D rd;//Referencia al componente
    private bool isGround;//Variable booleana para verificar si está en el piso

    private Animator animator;//variable para; se referencia el componente de animaciones

    private bool facinRigth=true;//Variable para saber a que lado está mirando la ratata

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        rd=GetComponent<Rigidbody2D>();//localitation
        animator=GetComponent<Animator>();//varible referncia para saber donde está la ratataa
        
    }

    // Update is called once per frame
    void Update(){
        float move = Input.GetAxis("Horizontal");//localitation
        float speedAnimation = Mathf.Abs(move);
        animator.SetFloat("Speed", speedAnimation);
       
        rd.linearVelocity=new Vector2(move * moveSpeed, rd.linearVelocityY);

        if(move > 0 && !facinRigth)//para donde ve
        {
            Flip();
        }else if(move < 0 && facinRigth)
        {
             Flip();
        }

        //permitir el salto
        if(Input.GetButtonDown("Jump") && isGround){
            rd.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
            //Introducir animación
            animator.SetBool("isJumping", true);
            
        }
        
    }

    //evento OnCollisionEnter2D, colisiona con la plataforma.
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Piso"))
        {
             isGround = true;
             animator.SetBool("isJumping", false);
        }
       
    }

    //Implementar OnCollisionExit2D
    void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Piso"))
        {
            isGround = false;
        }
        
    }

    void Flip()
    {
       facinRigth=!facinRigth;
       Vector3 scale = transform.localScale;
       scale.x*=-1;
       transform.localScale=scale;
    }
}
