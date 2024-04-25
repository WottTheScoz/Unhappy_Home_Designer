using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Transition;
    public string PlayScene;

    [Space(20)]
    public string SceneTransitionOut;

    void Start()
    {
        Transition.SetActive(false);
    }

    public void StartGame()
    {
        StartCoroutine(LoadOut());
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    #region Generic Methods

    void GetAnimation(GameObject obj, string animName)
    {
        obj.GetComponent<Animator>().Play(animName);
    }
    #endregion

    #region Animations
    IEnumerator LoadOut()
    {
        Transition.SetActive(true);
        GetAnimation(Transition, SceneTransitionOut);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(PlayScene);
    }
    #endregion
}
