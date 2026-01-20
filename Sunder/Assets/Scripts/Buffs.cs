using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    public enum PowerUp { Health, speed, damage }
    public PowerUp powerUps;

    public float amountToGive;

    private PlayerMovement Player;
    private PlayerHealth player;
    private EnemyHealth damage;
    private void Awake()
    {
        Player = FindObjectOfType<PlayerMovement>();
        player = FindObjectOfType<PlayerHealth>();
        damage = FindObjectOfType<EnemyHealth>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (powerUps)
            {
                case PowerUp.Health:
                    player.health += amountToGive;
                    break;
                case PowerUp.speed:
                    Player.speed += amountToGive;
                    break;
                case PowerUp.damage:
                    damage.PlayerDamage += amountToGive;
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}

