using System;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public float moveSpeed=5f; //Velocidad de movimiento
    public float jumpForce=7f;//Fuerza de salto
    private Rigidbody2D rd;//Referencia al componente
    private bool isGround;//Variable booleana para verificar si está en el suelo
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        rd=GetComponent<Rigidbody2D>();//localitation
        
    }

    // Update is called once per frame
    void Update(){
        float move = Input.GetAxis("Horizontal");//localitation
        rd.linearVelocity=new Vector2(move * moveSpeed, rd.linearVelocityY);
        
    }
}
