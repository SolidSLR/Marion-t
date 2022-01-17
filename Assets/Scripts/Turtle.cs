using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Personaje
{
    //Declaramos variable para la velocidad de la tortuga
    float speed=-2f;

    float oldSpeed;

    private float jump = 2.0f;
    private Vector3 leftSpawnPoint = new Vector3(-10f, 3.5f, 0f);

    private Vector3 rightSpawnPoint = new Vector3(10f, 3.5f, 0f);

    public Vector3 LeftSpawnPoint{
        get { return leftSpawnPoint;}
    }

    public Vector3 RightSpawnPoint{
        get { return rightSpawnPoint;}
    }

    public Animator animador;

    //Layers de enemigos y hitter
    //int layerEnemy;

    //int layerHitter;
    // Start is called before the first frame update
    void Start()
    {
       //Asignamos velocidad positiva o negativa dependiendo del punto de spawn 
       if(transform.position.x<0){
           speed = Mathf.Abs(speed);
           transform.localScale = new Vector3(-1,1,1);
       }else if(transform.position.x>0){
           speed = Mathf.Abs(speed)*-1;
           transform.localScale = new Vector3(1,1,1);
       }
       colision = GetComponent<Collider2D>();

       animador=GetComponent<Animator>();

       animador.SetBool("Turning", false);

       //layerEnemy = LayerMask.NameToLayer("Enemies");

       //layerHitter = LayerMask.NameToLayer("Hitter");
       
       base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(animador.GetBool("Tumbando")){
            if(IsGrounded()){
                speed = 0f;
            }
        }
        transform.position = new Vector3(transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);
        
        /*if(IsGrounded()){
            Physics2D.IgnoreLayerCollision(layerEnemy, layerHitter, false);
        }else if(!IsGrounded()){
            Physics2D.IgnoreLayerCollision(layerEnemy, layerHitter, true);
        }*/
    }
    public void OnTriggerEnter2D(Collider2D otroCollider){

        if(otroCollider.tag == "Ascensor"){
            /*transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            speed = -speed;*/
            if(transform.position.x < 0){
                transform.position=leftSpawnPoint;
            }else{
                transform.position=rightSpawnPoint;
            }

            Girar();
            ReWalk();

        }else if(otroCollider.tag == "Hitter" && animador.GetBool("Tumbando") == false /*&& IsGrounded()*/){

            rb.AddForce(Vector2.up*jump, ForceMode2D.Impulse);
            animador.SetBool("Tumbando", true);
            animador.SetBool("Turning", false);
            starJump = true;
            Invoke("EndJump",0.05f);

        }else if(otroCollider.tag == "Hitter" && animador.GetBool("Tumbando") == true){

            Debug.Log("Ay, me levanto");

        }else if(otroCollider.tag == "TP"){

            transform.position = new Vector3(-transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);

        }
    
    }

    public void OnCollisionEnter2D(Collision2D colision){

        if(colision.gameObject.tag=="Tortuga"){
        
            Girar();
        
        }else if(colision.gameObject.tag == "Mariont" && animador.GetBool("Tumbando") == true){

            Mario mario = colision.gameObject.GetComponent<Mario>();
            
            speed = mario.actualSpeed;
            
            Debug.Log("Ay, me muero");
            
            Death();
        }
    }

    private void Girar(){
        
        //Debug.Log("He girao");
        
        animador.SetBool("Turning", true);

        oldSpeed = speed;

        speed=0;

    }

    public void ReWalk(){

        animador.SetBool("Turning", false);

        speed = -oldSpeed;

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        //Debug.Log("Volviendo a caminar");

    }

    override protected float getContactPoint(){
        return 0.0f;
    }

    public void Death(){

        rb.AddForce(Vector2.up*jump, ForceMode2D.Impulse);

        this.colision.enabled=false;

        Destroy(this.gameObject, 5f);

    }

    public bool IsDanger(){
        return animador.GetBool("Tumbando");
    }

}
