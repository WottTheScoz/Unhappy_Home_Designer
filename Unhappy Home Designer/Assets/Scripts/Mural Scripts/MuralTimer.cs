using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MuralTimer : MonoBehaviour
{
    public GameObject MuralManagerObject;

    public float TargetTime = 60.0f;

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

            TextMeshProUGUI Text = GetComponent<TextMeshProUGUI>();
            Text.text = TargetTimeInt.ToString();
        }
        else
        {
            MuralManager muralManager = MuralManagerObject.GetComponent<MuralManager>(); 
            muralManager.EndMuralScene();
            TimerIsOn = false;
        }
    }
}
