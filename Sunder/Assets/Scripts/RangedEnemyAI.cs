using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedenemyAI : MonoBehaviour
{
    public float speed;
    public float stopDistance; // Distance at which enemy stops chasing
    public float retreatDistance; // Distance at which enemy backs away
    public float timeBetweenShots;
    private float nextShotTime;

    public GameObject projectile;
    public Transform player;

    void Update()
    {
        Vector3 chaseDir = player.transform.position - transform.position;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            // Move toward player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distance < stopDistance && distance > retreatDistance)
        {
            // Stay still (Attack Range)
            transform.position = transform.position;
        }
        else if (distance < retreatDistance)
        {
            // Retreat if player is too close
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        // Shooting 
        if (Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            chaseDir.Normalize();
            GetComponent<Rigidbody2D>().velocity = chaseDir * speed;
            nextShotTime = Time.time + timeBetweenShots;
        }
    }
}
