﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class ChangeScene : MonoBehaviour
{
    public AudioClip uiSound;
    AudioSource audio;

    public float delay;

    public GameMaster Master;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        Master = GetComponent<GameMaster>();
    }

    public void ChangetoScene(int sceneToChangeTo)
    {
        StartCoroutine("Delay");
        SceneManager.LoadScene(sceneToChangeTo);

        if (sceneToChangeTo == 10)
        {
            Application.Quit();
        }

       
    }

 public void restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Master.redo();
        }

    IEnumerator Delay()
    {
        audio.PlayOneShot(uiSound, 1.0f);
        yield return new WaitForSeconds(delay);
    }
}
