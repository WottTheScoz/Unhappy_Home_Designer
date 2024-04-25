using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public string MainMenuSceneName;
    public string MuralSceneName;

    void ToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToMainMenu()
    {
        ToScene(MainMenuSceneName);
    }

    public void ToMural()
    {
        ToScene(MuralSceneName);
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
