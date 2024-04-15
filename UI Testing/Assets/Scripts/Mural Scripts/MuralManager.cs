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
        StartCoroutine(SceneIntro());
    }

    //Mutes the audio this script is attached to.
    public void Mute()
    {
        audioSource.Pause();
    }

    #region Scene Transition Functions

    void GetAnimation(GameObject obj, string animName)
    {
        obj.GetComponent<Animator>().Play(animName);
    }

    public void EndMuralScene()
    {
        //Debug.Log("Timer has ended");
        ImageDragging imageDragging = Mural.GetComponent<ImageDragging>();
        imageDragging.ImageIsDraggable = false;

        StartCoroutine(SceneTransition());
    }

    IEnumerator SceneIntro()
    {
        Transition.SetActive(true);
        GetAnimation(Transition, "SceneTransitionIn");
        yield return new WaitForSeconds(1);
        Transition.SetActive(false);
    }

    IEnumerator SceneTransition()
    {
        Transition.SetActive(true);
        GetAnimation(Transition, "SceneTransitionOut");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(NewSceneName);
    }
    #endregion
}
