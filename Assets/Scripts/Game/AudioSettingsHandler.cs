using System.Collections;
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
        if (PlayerPrefs.GetInt("BGMOn", 1) == 1)
        {
            BGMToggle.isOn = true;
            BGMSlider.interactable = true;
        }
        if (PlayerPrefs.GetInt("SFXOn", 1) == 1) 
        { 
            SFXToggle.isOn = true;
            SFXSlider.interactable = true;
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
        GameManager.Instance.Blackboard.Volume.BGM(on);
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
        GameManager.Instance.Blackboard.Volume.SFX(on);
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
