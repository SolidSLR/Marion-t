using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Personaje : MonoBehaviour
{
    
    public abstract float getContactPoint();
    public bool starJump;

    public Collider2D colision;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    public void Start()
    {
        starJump = false;

        colision = GetComponent<Collider2D>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EndJump(){
        starJump=false;
    }

    public bool IsGrounded(){
        if(starJump){
            return false;
        }

        bool grounded=false;

            LayerMask mask = LayerMask.GetMask("Plataformas");

            ContactFilter2D filtro = new ContactFilter2D();

            filtro.SetLayerMask(mask);
            //Buscamos los puntos de contacto de Mariont chocando con otro collider
            ContactPoint2D[] puntosContacto = new ContactPoint2D[1];


            /*Si hay contacto, miramos que el punto sea el inferior y no uno de los otros. 
            Sólo se cancela la animación si el contacto es con la parte inferior. A mayores, 
            pasamos la máscara Plataformas como parámetro para corregir un bug*/
            if(colision.IsTouchingLayers(mask)){
                int numeroPuntos = colision.GetContacts(filtro, puntosContacto);
                Vector3 contactoLocal = transform.InverseTransformPoint(new Vector3(puntosContacto[0].point.x, puntosContacto[0].point.y, 0));
                if(numeroPuntos >= 1){
                    //Se comprueba que Mariont toque plataformas con el centro de la parte inferior
                    if(contactoLocal.y < getContactPoint() && rb.velocity.y <= 0){
                        grounded=true;
                    }

                    //Debug.Log("Detección de punto de contacto: "+contactoLocal.y);

            }
        }

        return grounded;
    }

    /*
    Esto funciona como clase heredada. Sin embargo, esta clase tiene poco código. Revisar como poder usar
    este método tanto con Mariont como con las tortugas.
    public void Death(float jump){

        rb.AddForce(Vector2.up*jump, ForceMode2D.Impulse);
        
        this.colision.enabled=false;

    }*/

}
