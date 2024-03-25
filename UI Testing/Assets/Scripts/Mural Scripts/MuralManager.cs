using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuralManager : MonoBehaviour
{
    public GameObject Mural;
    public GameObject Transition;

    public string NewSceneName;

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

    #region Scene Transition Functions
    public void EndMuralScene()
    {
        //Debug.Log("Timer has ended");
        ImageDragging imageDragging = Mural.GetComponent<ImageDragging>();
        imageDragging.ImageIsDraggable = false;

        StartCoroutine(SceneTransition());
    }

    IEnumerator SceneTransition()
    {
        Transition.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(NewSceneName);
    }
    #endregion
}
