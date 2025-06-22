using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{
    [Header("Audio")]
    public Slider globalSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Display")]
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    void Start()
    {
        SetupResolutionDropdown();
        SetupInitialValues();
    }

    private void SetupResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var options = new List<string>();
        int currentIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            var res = resolutions[i];
            string option = $"{res.width} x {res.height}";
            options.Add(option);

            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
            {
                currentIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void SetupInitialValues()
    {
        fullscreenToggle.isOn = Screen.fullScreen;

        if (AudioController.Instance != null)
        {
            globalSlider.value = Mathf.Pow(10, AudioController.Instance.GetGlobalVolume() / 20f);
            musicSlider.value = Mathf.Pow(10, AudioController.Instance.GetMusicVolume() / 20f);
            sfxSlider.value = Mathf.Pow(10, AudioController.Instance.GetSFXVolume() / 20f);
        }
    }

    public void OnGlobalVolumeChanged(float value)
    {
        AudioController.Instance.SetGlobalVolume(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        AudioController.Instance.SetMusicVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        AudioController.Instance.SetSFXVolume(value);
    }

    public void OnFullscreenToggled(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void OnResolutionChanged(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
