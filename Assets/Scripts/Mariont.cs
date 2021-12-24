using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mariont : MonoBehaviour
{
    // Start is called before the first frame update
            float speed = 4f;
            Animator animator;

    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            animator.SetBool("Move", true);
            transform.localScale = new Vector3(1,1,1);
        } else if (Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            animator.SetBool("Move", true);
            transform.localScale = new Vector3(-1,-1,-1);
        }
    }
}
