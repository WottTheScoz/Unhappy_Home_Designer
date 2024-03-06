using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static int Funds = 100;

    //public GameObject MoneyTextObject;
    public TextMeshProUGUI Text;

    //private bool HasChanged;

    void Start()
    {
        ChangeScreenText();
    }

    /*void Update()
    {
        if(HasChanged)
        {
            ChangeScreenText();
            Debug.Log("Screen Text Has Changed.");
            HasChanged = false;
        }
    }*/


    public void CalculateCost(int cost)
    {
        Funds -= cost;
        Debug.Log("$" + Funds + " left.");
        //HasChanged = true;
        //Debug.Log("HasChanged is " + HasChanged);
        ChangeScreenText();
    }

    public void ChangeScreenText()
    {
        //TextMeshProUGUI Text = MoneyTextObject.GetComponent<TextMeshProUGUI>();
        Text.text = Funds.ToString();
    }
}