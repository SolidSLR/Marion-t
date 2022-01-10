using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    //Declaramos variable para la velocidad de la tortura
    float speed=-2f;

    Collider2D collider;

    private Vector3 leftSpawnPoint = new Vector3(-10f, 3.5f, 0f);

    private Vector3 rightSpawnPoint = new Vector3(10f, 3.5f, 0f);

    public Vector3 LeftSpawnPoint{
        get { return leftSpawnPoint;}
    }

    public Vector3 RightSpawnPoint{
        get { return rightSpawnPoint;}
    }
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
       collider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //bool grounded=false;
        transform.position = new Vector3(transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);
        

    }
    public void OnTriggerEnter2D(Collider2D otroCollider){

        if(otroCollider.tag == "Ascensor"){
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            speed = -speed;
            if(transform.position.x < 0){
                transform.position=leftSpawnPoint;
            }else{
                transform.position=rightSpawnPoint;
            }
        }else {

            transform.position = new Vector3(-transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);

        }
    
    }

    public void OnCollisionEnter2D(Collision2D colision){
        
    }

}
