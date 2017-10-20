using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //give us access to Unity UI methods and variables

public class GameMaster : MonoBehaviour
{
    public int points;
    public Text pointsText;
    public PlayerController player;
    public GameObject GameOver;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        GameOver.SetActive(false);

    }
    void Update()
    {
        pointsText.text = ("Points:" + points);

        if(player.deathCheck)
        {
            GameOver.SetActive(true);
        }
        
    }
}
