using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuralManager : MonoBehaviour
{
    public string NewSceneName;

    public AudioClip Audio;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Mutes the audio this script is attached to.
    public void Mute()
    {
        audioSource.Pause();
    }

    //Skips the Mural Scene.
    public void Skip()
    {
        SceneManager.LoadScene(NewSceneName);
    }
}
