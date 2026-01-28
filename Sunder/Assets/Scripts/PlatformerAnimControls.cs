using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerAnimControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<Animator>();
        float moveX = Input.GetAxis("Horizontal");
        float moveY = GetComponent<Rigidbody2D>().velocity.y;
        GetComponentInChildren<Animator>().SetFloat("x", moveX);
        GetComponentInChildren<Animator >().SetFloat("y", moveY);
        if(moveX < 0)
        {
            //we're moving to the left
            //flip our sprite to the left
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
