using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class ChangeScene : MonoBehaviour
{
    public AudioClip uiSound;
    AudioSource audio;

    public float delay;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
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

    IEnumerator Delay()
    {
        audio.PlayOneShot(uiSound, 1.0f);
        yield return new WaitForSeconds(delay);
    }
}
