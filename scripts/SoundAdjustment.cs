using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundAdjustment : MonoBehaviour
{
    private Slider slider;
    public soundAdjusters adjustmentTarget;
    public enum soundAdjusters // which slider this instance will control
    {
        sfx, 
        background,
        jump
    }
    void Start()
    {


        switch (adjustmentTarget) // what is the sound adjuster set in the Inspector
        {
            case soundAdjusters.background: 
                {
                    slider.onValueChanged.AddListener((val) => handleBackgroundVolChange(val));
                    break;
                }
            case soundAdjusters.sfx:
                {
                    slider.onValueChanged.AddListener((val) => handleSFXVolChange(val));
                    break;
                }

            case soundAdjusters.jump:
                {
                    slider.onValueChanged.AddListener((val) => handleJumpVolChange(val));
                    break;
                }


        }

    }

    private void OnEnable()
    {
        slider = GetComponent<Slider>();

        switch (adjustmentTarget)
        {
            case soundAdjusters.background:
                {
                    slider.value = soundManager.Instance.getBackgroundVolume();
                    break;
                }
            case soundAdjusters.sfx:
                {
                    slider.value = soundManager.Instance.getSFXvolume();
                    break;
                }

            case soundAdjusters.jump:
                {
                    slider.value = soundManager.Instance.getJumpVolume();
                    break;
                }

        }
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveAllListeners();
    }

    private void handleSFXVolChange(float val) // change sfx volume to val
    {
        soundManager.Instance.ChangeSFXVolume(val);
    }
    private void handleJumpVolChange(float val)// change jump volume to val
    {
        soundManager.Instance.ChangeJumpVolume(val);
    }
    private void handleBackgroundVolChange(float val)// change background volume to val
    {
        soundManager.Instance.ChangeBackgroundVolume(val);
    }
}
