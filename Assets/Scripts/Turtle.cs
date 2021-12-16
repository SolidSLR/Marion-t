using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    //Declaramos variable para la velocidad de la tortura
    float speed=2f;

    Collider2D collider;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
       
       collider = GetComponent<Collider2D>();
       rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        bool grounded=false;

        if(IsGrounded()){
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
        }

        bool IsGrounded(){
            /*Si hay contacto, miramos que el punto sea el inferior y no uno de los otros. 
            Sólo se cancela la animación si el contacto es con la parte inferior. A mayores, 
            pasamos la máscara Plataformas como parámetro para corregir un bug*/
                ContactPoint2D[] puntosContacto = new ContactPoint2D[5];

                int numeroPuntos = collider.GetContacts(puntosContacto);
                Vector3 contactoLocal = transform.InverseTransformPoint(new Vector3(puntosContacto[0].point.x, puntosContacto[0].point.y, 0));
                if(numeroPuntos == 1){
                    //Se comprueba que Mariont toque plataformas con el centro de la parte inferior
                    if(contactoLocal.y < -0.5f && rb.velocity.y <= 0){
                        grounded=true;
                    }

                    Debug.Log("Detección de punto de contacto: "+contactoLocal.y);

            }else if(numeroPuntos > 1){
                Debug.Log("Detección de puntos de contacto totales: "+contactoLocal);
            }
            //grounded=true;

        return grounded;
    }
    }
}
