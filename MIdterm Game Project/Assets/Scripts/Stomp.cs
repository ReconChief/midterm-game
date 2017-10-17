using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Stomp : MonoBehaviour
{
    public AudioClip enemyDeath;
    public float delay;

    AudioSource audio;

    public GameObject slimeDeathEffect;

    private Rigidbody2D playerBody;
    private PlayerController player;
    public float bounceOnEnemy;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine("DeathDelay");

            playerBody.velocity = new Vector2(playerBody.velocity.x, bounceOnEnemy);
        }
    }

    IEnumerator DeathDelay()
    {
        audio.PlayOneShot(enemyDeath, 1.0f);
        yield return new WaitForSeconds(delay);
        Destroy(transform.parent.gameObject);
    }
}
