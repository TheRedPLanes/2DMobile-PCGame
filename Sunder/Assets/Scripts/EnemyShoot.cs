using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
     
    public float speed = 3f;
    Transform player;
    public float leadAmt = 5;
    private float timer = 0;
    public float shootDelay = 2;
    public float shootTriggerDistance = 5;
    public GameObject prefab;
    public float shootSpeed = 10;
    public float bulletLifetime = 3;
    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (player != null && timer > shootDelay)
        {
            timer = 0;
            //shoot towards where they player is going to be
            //first, figure out where the player is, and which direction they are going
            Vector3 playerPos = player.position;
            Vector3 dir = player.GetComponent<Rigidbody2D>().velocity;
            Vector3 shootDir = (playerPos + (dir * leadAmt)) - transform.position;
            if (shootDir.magnitude < shootTriggerDistance)
            {
                //shoot towards the player
                timer = 0;
                shootDir.Normalize();
                GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;
                Destroy(bullet, bulletLifetime);
            }
            // Move toward the player
            //transform.position = Vector2.MoveTowards(transform.position,  player.position, speed * Time.deltaTime);
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
