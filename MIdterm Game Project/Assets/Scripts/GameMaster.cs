using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //give us access to Unity UI methods and variables

public class GameMaster : MonoBehaviour
{
    public int points;
    public Text pointsText;

    void Update()
    {
        pointsText.text = ("Points:" + points);
    }
}
