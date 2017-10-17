using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GustController : MonoBehaviour
{
    public float speed;
    public GameObject gust;

    private PlayerController player;

    AudioSource audio;
    public AudioClip DeathSfx;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (player.transform.localScale.x < 0)
        {
            speed = -speed;
        }

        audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Barnacle" || other.tag == "Spider" || other.tag == "Bat")
        {
            audio.PlayOneShot(DeathSfx, 1.0f);
            Destroy(other.gameObject);
        }

        if (other.tag == "Gust Container")
        {
            Destroy(gameObject);
        }
    }
}
