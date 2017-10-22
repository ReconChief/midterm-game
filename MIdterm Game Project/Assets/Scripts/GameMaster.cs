using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //give us access to Unity UI methods and variables

public class GameMaster : MonoBehaviour
{
    public static int points;
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
        pointsText.text = ("$:" + points);

        if (player.deathCheck)
        {
            GameOver.SetActive(true);
        }

    }

    public static void AddPoints(int AddPoints)
    {
        points += AddPoints;
    }

    public void redo(){
        points = 0;
    }
    

        
}
