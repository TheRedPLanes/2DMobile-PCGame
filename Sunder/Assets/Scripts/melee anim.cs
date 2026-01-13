using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeanim : MonoBehaviour
{
    public Animator playerAnim;

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown("Fire1"))
        {
            playerAnim.Play("Sword slash");
        }
    }
}
