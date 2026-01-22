using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iconanim : MonoBehaviour
{
    public Animator anim;
    private SpriteRenderer SRender;

    private bool playerInRange;

    private void Start()
    {
        SRender = GetComponent<SpriteRenderer>();
        SRender.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SRender.enabled = true;
            anim.SetBool("playerInRange", true);
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", false);
            playerInRange = false;
            SRender.enabled = false;
        }
    }
}
