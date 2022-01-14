using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : Personaje
{
    //Velocidad de movimiento absoluta de Mariont
    private float speed = 3f;
    //Velocididad de movimiento actual (para controlar deceleración) 
    public float actualSpeed;
    //Deceleración
    private float deceleration = 7f;
    //Controlador del animador
    Animator animator;
    
    //Controlador de la velocidad del salto
    private float jump = 6.5f;
    //Variable para impedir que se acelere hacia ningún lado mientras estás frenando
    private bool stopping = false;
    
    
    // Start is called before the first frame update
    void Start()
    {

        base.Start();
        animator = GetComponent<Animator>();
        starJump = false;
    }

    // Update is called once per frame
    void Update()
    {

        bool isGrounded = IsGrounded();

        if(isGrounded){
        
            animator.SetBool("Jumping", false);
        }

        /*Comprobamos que Mariont no esté parado, la tecla pulsada y evitamos que se mueva al lado 
        contrario si no se ha parado antes de cambiar de sentido.Los camios de velocidad sólo se
        permiten si Mariont está en el suelo*/
        if(isGrounded){
            if(!stopping && Input.GetKey(KeyCode.LeftArrow) && actualSpeed<=0){
                //Si Mariont se mueve a la izquierda, asignamos a actualSpeed la velocidad negativa
                actualSpeed = -speed;
                animator.SetBool("Walking", true);
                transform.localScale = new Vector3(1,1,1);

            }else if(!stopping && Input.GetKey(KeyCode.RightArrow) && actualSpeed >=0){
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
                //Debug.Log(actualSpeed);
                if(Mathf.Abs(actualSpeed)<0.01f){
                    
                    stopping = false;
                    animator.SetBool("Stopping", false);
                    actualSpeed = 0;
                }
            }
        }

        transform.Translate(Vector3.right * actualSpeed * Time.deltaTime, Space.Self);
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded){

            rb.AddForce(Vector2.up*jump, ForceMode2D.Impulse);
            animator.SetBool("Jumping", true);
            starJump = true;
            Invoke("EndJump",0.05f);
        }
    }

    override public float getContactPoint(){
        return 0.5f;
    }

    public void OnCollisionEnter(Collision colision) {
        if(colision.gameObject.tag=="Tortuga"){
            Turtle tortuga = colision.gameObject.GetComponent<Turtle>();

            /*
            Revisar esto
            if(!tortuga.IsDanger()){
                actualSpeed = 0;
                colision.enabled=false;
            }*/
        }
    }
}
