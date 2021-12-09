using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    private float speed = 3f; 
    Animator animator;
    private Rigidbody2D rb;

    private float jump = 6.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walking", false);
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
            animator.SetBool("Walking", true);
            transform.localScale = new Vector3(1,1,1);
        }else if(Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
            animator.SetBool("Walking", true);
            transform.localScale = new Vector3(-1,1,1);
        }else {
            animator.SetBool("Walking", false);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            rb.AddForce(Vector2.up*jump, ForceMode2D.Impulse);
        }/*else {
            animator.SetBool("Walking", false);
        }*/
    }
}
