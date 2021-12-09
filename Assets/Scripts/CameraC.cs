using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraC : MonoBehaviour
{
    //Referencia al objeto que debe seguir la cámara
    public Transform player;
    private float MinX = -4.3f;
    private float MaxX = 4.3f;
    // Start is called before the first frame update
    void Start()
    {
        if(player==null){
            Debug.Log("Cámara: la variable player no está inicializada");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Variable declarada para controlar la posición del jugador
        float xPlayer = player.position.x;
        float yPlayer = player.position.y;
        if(xPlayer < MinX){
            xPlayer = MinX;
        }else if(xPlayer > MaxX){
            xPlayer = MaxX;
        }
        //Ponemos en la x del vector la posición en x del personaje
        transform.position = new Vector3(xPlayer, transform.position.y, transform.position.z);
    }
}
