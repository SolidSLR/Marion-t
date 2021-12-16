using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    //Declaramos variable para la velocidad de la tortura
    float speed=2f;

    Collider2D collider;

    Rigidbody2D rb;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       
       collider = GetComponent<Collider2D>();
       rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        bool grounded=false;

        if(IsGrounded()){
            animator.SetBool("Walking", true);
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
        }

        /*Función que controla si la tortuga toca el suelo*/

        bool IsGrounded(){
            
                ContactPoint2D[] puntosContacto = new ContactPoint2D[5];

                int numeroPuntos = collider.GetContacts(puntosContacto);
                Vector3 contactoLocal = transform.InverseTransformPoint(new Vector3(puntosContacto[0].point.x, puntosContacto[0].point.y, 0));
                if(numeroPuntos > 0){
                    //Se comprueba que Mariont toque plataformas con el centro de la parte inferior
                    
                    grounded=true;
                    Debug.Log("Detección de punto de contacto: "+contactoLocal.y);

            }

        return grounded;
    }
    }
}
