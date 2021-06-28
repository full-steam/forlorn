using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeHandler : MonoBehaviour
{

    private void Start()
    {
        GetComponent<GameManager>().Blackboard.Volume = this;
        LoadAudioSettings();
    }
    
    private void LoadAudioSettings()
    {
        var bgm = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        var sfx = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        if (PlayerPrefs.GetInt("BGMOn", 1) == 1) AudioController.SetCategoryVolume("BGM", bgm);
        else AudioController.SetCategoryVolume("BGM", 0f);
        if (PlayerPrefs.GetInt("SFXOn", 1) == 1)
        {
            AudioController.SetCategoryVolume("SFX", sfx);
            AudioController.SetCategoryVolume("Dictionary", sfx);
        }
        else
        {
            AudioController.SetCategoryVolume("SFX", 0f);
            AudioController.SetCategoryVolume("Dictionary", 0f);
        }
    }

    public void BGM(bool on)
    {
        if (on)
        {
            var vol = PlayerPrefs.GetFloat("PrevBGMVolume", 1.0f);
            //Debug.Log(vol);
            PlayerPrefs.SetInt("BGMOn", 1);
            ChangeBGMVolume(vol);
        }
        else
        {
            //Debug.Log("Toggle turn off triggered");
            PlayerPrefs.SetInt("BGMOn", 0);
            ChangeBGMVolume(0);
        }
    }

    public void ChangeBGMVolume(float value)
    {
        AudioController.SetCategoryVolume("BGM", value);
        PlayerPrefs.SetFloat("BGMVolume", value);
        if (value > 0f) PlayerPrefs.SetFloat("PrevBGMVolume", value);
    }

    public void SFX(bool on)
    {
        if (on)
        {
            var vol = PlayerPrefs.GetFloat("PrevSFXVolume", 1.0f);
            //Debug.Log(vol);
            PlayerPrefs.SetInt("SFXOn", 1);
            ChangeSFXVolume(vol);
        }
        else
        {
            //Debug.Log("Toggle turn off triggered");
            PlayerPrefs.SetInt("SFXOn", 0);
            ChangeSFXVolume(0);
        }
    }

    public void ChangeSFXVolume(float value)
    {
        AudioController.SetCategoryVolume("SFX", value);
        AudioController.SetCategoryVolume("Dictionary", value);
        PlayerPrefs.SetFloat("SFXVolume", value);
        if (value > 0f) PlayerPrefs.SetFloat("PrevSFXVolume", value);
    }
}