using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundCheck : MonoBehaviour
{
    public GameObject[] backgrounds;
    public int backgroundIndex = 0; // inex of current background in the array

    private void Start()
    {
        checkpointmanager.Instance.OnReachedCheckpoint += ChangeBackground;
    }

    private void OnDisable()
    {
        checkpointmanager.Instance.OnReachedCheckpoint -= ChangeBackground;
    }

    public int getBackgroundIndex() // return current background
    {
        return backgroundIndex;
    }


    public void SetBackground(int index) // set background to index passed
    {
        if (index == 0) return;

        backgrounds[backgroundIndex].SetActive(false);
        backgroundIndex = index -1;
        backgrounds[backgroundIndex].SetActive(true);
    }

    public void ChangeBackground()// increase background
    {
        backgrounds[backgroundIndex].SetActive(false);
        backgroundIndex++;
        backgrounds[backgroundIndex].SetActive(true);
    }
}
