using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
     
    public float speed = 3f;
    Transform player;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Move toward the player
            transform.position = Vector2.MoveTowards(transform.position,  player.position, speed * Time.deltaTime);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Optional: Check the tag of the other object to ensure the right collision
        if (collision.CompareTag("Player"))
        {
            // Destroy the GameObject this script is attached to
            Destroy(gameObject);
        }
    }
}
