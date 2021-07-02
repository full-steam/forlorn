using UnityEngine;

/// <summary>
/// Handles volume settings for both BGM and SFX.
/// </summary>
public class VolumeHandler : MonoBehaviour
{
    private void Start()
    {
        // self-assign to Blackboard
        GetComponent<GameManager>().Blackboard.Volume = this;
        LoadAudioSettings();
    }
    
    /// <summary>
    /// Loads initial audio settings.
    /// </summary>
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

    /// <summary>
    /// Toggles background music on or off.
    /// </summary>
    /// <param name="on">Whether the BGM should be turned on or off.</param>
    public void ToggleBGM(bool on)
    {
        if (on)
        {
            var vol = PlayerPrefs.GetFloat("PrevBGMVolume", 1.0f);
            PlayerPrefs.SetInt("BGMOn", 1);
            ChangeBGMVolume(vol);
        }
        else
        {
            PlayerPrefs.SetInt("BGMOn", 0);
            ChangeBGMVolume(0);
        }
    }

    /// <summary>
    /// Sets the BGM volume value.
    /// </summary>
    /// <param name="value">New BGM volume.</param>
    public void ChangeBGMVolume(float value)
    {
        AudioController.SetCategoryVolume("BGM", value);
        PlayerPrefs.SetFloat("BGMVolume", value);
        if (value > 0f) PlayerPrefs.SetFloat("PrevBGMVolume", value);
    }

    /// <summary>
    /// Toggles sound effects on or off.
    /// </summary>
    /// <param name="on">Whether the SFX should be turned on or off.</param>
    public void ToggleSFX(bool on)
    {
        if (on)
        {
            var vol = PlayerPrefs.GetFloat("PrevSFXVolume", 1.0f);
            PlayerPrefs.SetInt("SFXOn", 1);
            ChangeSFXVolume(vol);
        }
        else
        {
            PlayerPrefs.SetInt("SFXOn", 0);
            ChangeSFXVolume(0);
        }
    }

    /// <summary>
    /// Sets the SFX volume value.
    /// </summary>
    /// <param name="value">New SFX volume.</param>
    public void ChangeSFXVolume(float value)
    {
        AudioController.SetCategoryVolume("SFX", value);
        AudioController.SetCategoryVolume("Dictionary", value);
        PlayerPrefs.SetFloat("SFXVolume", value);
        if (value > 0f) PlayerPrefs.SetFloat("PrevSFXVolume", value);
    }
}