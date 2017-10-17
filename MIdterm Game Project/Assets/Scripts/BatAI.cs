using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//credit to this video https://www.youtube.com/watch?v=-yQjn9ekh5I

public class BatAI : MonoBehaviour
{
    private PlayerController player;

    public float speed;

    public float range;

    public LayerMask playerLayer;

    public bool playerInRange;

	void Start ()
    {
        player = FindObjectOfType<PlayerController>();
	}
	
	void Update ()
    {

        playerInRange = Physics2D.OverlapCircle(transform.position, range, playerLayer); //range if player is near enemy

        if (playerInRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); //enemy moves
        }
	}
}
