using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Plop;
    public GameObject Grid;

    private GridBuildingSystem GBSystem;

    void Start()
    {
        GBSystem = Grid.GetComponent<GridBuildingSystem>();

        GBSystem.InstantiateFurniture += PlopSound;
    }
    void PlopSound()
    {
        Plop.Play();
    }
}
