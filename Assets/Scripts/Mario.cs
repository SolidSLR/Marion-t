using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    //Velocidad de movimiento absoluta de Mariont
    private float speed = 3f;
    //Velocididad de movimiento actual (para controlar deceleración) 
    private float actualSpeed;
    //Deceleración
    private float deceleration = 7f;
    //Controlador del animador
    Animator animator;
    //Controlador del Rigidbody
    private Rigidbody2D rb;
    //Controlador de colisiones
    private Collider2D collider;
    //Controlador de la velocidad del salto
    private float jump = 6.5f;
    //Variable para impedir que se acelere hacia ningún lado mientras estás frenando
    private bool stopping = false;
    //Variable para controlar el inicio del salto y no controlar si toca suelo
    private bool starJump;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        //starJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded()){
        
            animator.SetBool("Jumping", false);
            Debug.Log("Suelo");
        }else if(animator.GetBool("Jumping")){
            Debug.Log("En aire");
        }
        //actualSpeed = speed;

        //Comprobamos que Mariont no esté parado, la tecla pulsada y evitamos que se mueva al lado 
        //contrario si no se ha parado antes de cambiar de sentido
        if(!stopping && Input.GetKey(KeyCode.LeftArrow) && actualSpeed<=0){
            //transform.Translate(Vector3.left * actualSpeed * Time.deltaTime, Space.Self);
            //Si Mariont se mueve a la izquierda, asignamos a actualSpeed la velocidad negativa
            actualSpeed = -speed;
            animator.SetBool("Walking", true);
            transform.localScale = new Vector3(1,1,1);

        }else if(!stopping && Input.GetKey(KeyCode.RightArrow) && actualSpeed >=0){
            //transform.Translate(Vector3.right * actualSpeed * Time.deltaTime, Space.Self);
            //Asignamos a actualSpeed la velocidad positiva
            actualSpeed = speed;
            animator.SetBool("Walking", true);
            transform.localScale = new Vector3(-1,1,1);

        }else if(actualSpeed!=0){

            stopping = true;
            animator.SetBool("Stopping", true);
            animator.SetBool("Walking", false);

            if(actualSpeed>0){

                actualSpeed -= deceleration*Time.deltaTime;

            }else if(actualSpeed<0){

                actualSpeed += deceleration*Time.deltaTime;
            }
            Debug.Log(actualSpeed);
            if(Mathf.Abs(actualSpeed)<0.01f){
                stopping = false;
                animator.SetBool("Stopping", false);
                actualSpeed = 0;
                //animator.SetBool("Stopping", false);
            }
        }

        transform.Translate(Vector3.right * actualSpeed * Time.deltaTime, Space.Self);
        
        if (Input.GetKeyDown(KeyCode.UpArrow) /*&& (!stopping && (actualSpeed<=0||actualSpeed>=0))*/){

            rb.AddForce(Vector2.up*jump, ForceMode2D.Impulse);
            animator.SetBool("Jumping", true);
            starJump = true;
            Invoke("endJump",0.05f);

            Debug.Log("Salta");
        }/*else if(isGrounded()){
            animator.SetBool("Jumping", false);
        }*/
    }

    private void endJump(){
        starJump=false;
    }

    private bool isGrounded(){
        if(starJump){
            return false;
        }else{
            return collider.IsTouchingLayers();
        }
    }
}
