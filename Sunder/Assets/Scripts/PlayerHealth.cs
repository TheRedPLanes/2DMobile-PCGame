using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    //store the players health
    public float health = 10;
    float maxHealth;
    public Image healthBar;
    float Timer = 0f;
    public float flashRed = 0.1f;
    public GameObject newCanvasGameObject; // Drag your new Canvas GameObject here in the Inspector
    //where do we want to play the sound
    AudioSource audioSource;
    //what sound do we want to play when we jump
    public AudioClip hitSound;
    //if we collide with something tagged as enemy, take damage
    //if health gets too low, we die (reload the level)
    //if we collide with something tagged as health pack, increase health

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && Timer > 1f)
        {
            if (audioSource != null && hitSound != null)
            {
                //play the jump sound
                audioSource.PlayOneShot(hitSound);
            }
            Timer = 0;
            health--;
            healthBar.fillAmount = health / maxHealth;
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            if (health <= 0f)
            {
               
                GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                newCanvasGameObject.SetActive(true); //activate a new canvas (whatever you select)
                Time.timeScale = 0f;                    
            }
            if (health <= 0f)
            {
                GetComponentInChildren<Animator>().SetTrigger("Death");
            }
            
        }
        if (collision.gameObject.tag == "DeathScreenTrigger")
        {
            health = 0;
            if (health <= 0f)
            {
                GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                newCanvasGameObject.SetActive(true); //activate a new canvas (whatever you select)
                Time.timeScale = 0f;
            }
        }
        //if we collide with the health pack collectable
        if(collision.gameObject.tag == "HealthPack")
        {
            //increase the health value
            health++;
            healthBar.fillAmount = health / maxHealth;
            Destroy(collision.gameObject);
            //if our health is trying to exceed our max health
            if(health > maxHealth)
            {
                //cap our health at max health
                health = maxHealth;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss" && Timer > 1f)
        {
            if (audioSource != null && hitSound != null)
            {
                //play the jump sound
                audioSource.PlayOneShot(hitSound);
            }
            Timer = 0;
            health--;
            healthBar.fillAmount = health / maxHealth;
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            if (health <= 0f)
            {
                GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                newCanvasGameObject.SetActive(true); //activate a new canvas (whatever you select)
                Time.timeScale = 0f;
            }

        }
        if (collision.gameObject.tag == "DeathScreenTrigger")
        {
            health = 0;
            if (health <= 0f)
            {
                GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                newCanvasGameObject.SetActive(true); //activate a new canvas (whatever you select)
                Time.timeScale = 0f;
            }
        }
        //if we collide with the health pack collectable
        if (collision.gameObject.tag == "HealthPack")
        {
            //increase the health value
            health++;
            healthBar.fillAmount = health / maxHealth;
            Destroy(collision.gameObject);
            //if our health is trying to exceed our max health
            if (health > maxHealth)
            {
                //cap our health at max health
                health = maxHealth;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        healthBar.fillAmount = health / maxHealth;
        audioSource = Camera.main.GetComponent<AudioSource>();
        newCanvasGameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > flashRed)
        {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
