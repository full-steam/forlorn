﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsHandler : MonoBehaviour
{

    public Slider BGMSlider;
    public Slider SFXSlider;

    public Toggle BGMToggle;
    public Toggle SFXToggle;

    private void Start()
    {
        BGMSlider.value = AudioController.GetCategoryVolume("BGM");
        SFXSlider.value = AudioController.GetCategoryVolume("SFX");
        if (PlayerPrefs.GetInt("BGMOn", 1) == 0)
        {
            BGMToggle.isOn = false;
            BGMSlider.interactable = false;
        }
        if (PlayerPrefs.GetInt("SFXOn", 1) == 0) 
        { 
            SFXToggle.isOn = false;
            SFXSlider.interactable = false;
        }
        BGMSlider.onValueChanged.AddListener(ChangeBGMVol);
        SFXSlider.onValueChanged.AddListener(ChangeSFXVol);
        BGMToggle.onValueChanged.AddListener(ToggleBGM);
        SFXToggle.onValueChanged.AddListener(ToggleSFX);
    }


    public void ChangeBGMVol(float value)
    {
        GameManager.Instance.Blackboard.Volume.ChangeBGMVolume(value);
    }

    public void ToggleBGM(bool on)
    {
        GameManager.Instance.Blackboard.Volume.ToggleBGM(on);
        if (on)
        {
            BGMSlider.value = AudioController.GetCategoryVolume("BGM");
            //Debug.Log(PlayerPrefs.GetFloat("PrevBGMVolume", 1.0f));
            BGMSlider.interactable = true;
        }
        else
        {
            //BGMSlider.value = 0;
            BGMSlider.interactable = false;
        }
    }

    public void ChangeSFXVol(float value)
    {
        GameManager.Instance.Blackboard.Volume.ChangeSFXVolume(value);
    }

    public void ToggleSFX(bool on)
    {
        GameManager.Instance.Blackboard.Volume.ToggleSFX(on);
        if (on)
        {
            SFXSlider.value = AudioController.GetCategoryVolume("SFX");
            //Debug.Log(PlayerPrefs.GetFloat("PrevSFXVolume"));
            SFXSlider.interactable = true;
        }
        else
        {
            //SFXSlider.value = 0;
            SFXSlider.interactable = false;
        }
    }
}
