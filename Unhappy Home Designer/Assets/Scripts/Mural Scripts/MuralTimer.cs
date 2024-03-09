using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MuralTimer : MonoBehaviour
{
    public GameObject TextObject;

    public float TargetTime = 60.0f;

    public string NewSceneName;

    private bool TimerIsOn = true;

    void Update()
    {
        if (TimerIsOn)
        {
            Timer();
        }
    }

    void Timer()
    {
        if (TargetTime > 0.0f)
        {
            TargetTime -= Time.deltaTime;

            int TargetTimeInt = (int)TargetTime;

            TextMeshProUGUI Text = TextObject.GetComponent<TextMeshProUGUI>();
            Text.text = TargetTimeInt.ToString();
        }
        else
        {
            TimerEnded();
            TimerIsOn = false;
        }
    }

    void TimerEnded()
    {
        //Debug.Log("Timer has ended");
        SceneManager.LoadScene(NewSceneName);
    }
}
