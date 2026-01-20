using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemyAI : MonoBehaviour
{
    // Target to attack (usually the player)
    public Transform target;

    // Attack settings
    public float attackHeight = 2f;        // How high enemy moves when attacking
    public float attackSpeed = 5f;         // Speed of enemy movement during attack
    public float cooldownTime = 2f;        // Cooldown in seconds between attacks

    private bool isAttacking = false;      // Track attack state
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;    // Record initial position
        if (target == null)
        {
            Debug.LogError("Target not assigned!");
        }
    }

    void Update()
    {
        // Start attack if not already attacking
        if (!isAttacking && target != null)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // Step 1: Move up towards attack position
        Vector3 attackPos = new Vector3(transform.position.x, startPos.y + attackHeight, transform.position.z);
        while (Vector3.Distance(transform.position, attackPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, attackPos, attackSpeed * Time.deltaTime);
            yield return null; // Wait for next frame
        }

        // Step 2: Move back down to original position
        while (Vector3.Distance(transform.position, startPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, attackSpeed * Time.deltaTime);
            yield return null;
        }

        // Step 3: Wait for cooldown
        yield return new WaitForSeconds(cooldownTime);

        isAttacking = false; // Ready for next attack
    }
}
