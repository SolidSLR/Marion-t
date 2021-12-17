using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    //Declaramos variable para la velocidad de la tortura
    float speed=-2f;

    Collider2D collider;

    Rigidbody2D rb;

    Animator animator;

    ContactPoint2D[] puntosContacto;

    LayerMask mask;

    private Vector3 leftSpawnPoint = new Vector3(-10f, 2.7f, 0f);

    private Vector3 rightSpawnPoint = new Vector3(10f, 2.7f, 0f);
    // Start is called before the first frame update
    void Start()
    {
       
       collider = GetComponent<Collider2D>();
       rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
       puntosContacto = new ContactPoint2D[5];

    }

    // Update is called once per frame
    void Update()
    {
        //bool grounded=false;
        transform.position = new Vector3(transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);
        /*if(IsGrounded()){
            animator.SetBool("Walking", true);
            //transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
            transform.position = new Vector3(transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);
        }*/

        /*Función que controla si la tortuga toca el suelo*/

           /* bool IsGrounded(){

                    int numeroPuntos = collider.GetContacts(puntosContacto);
                    Vector3 contactoLocal = transform.InverseTransformPoint(new Vector3(puntosContacto[0].point.x, puntosContacto[0].point.y, 0));
                    if(numeroPuntos > 0){
                        //Se comprueba que Mariont toque plataformas con el centro de la parte inferior
                        
                        grounded=true;

                }

            return grounded;
        }*/

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
}
