using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mariont : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int speed = 5;

        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
