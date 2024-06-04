using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    Resolution[] resolutions;

    void Start()
    {
        if (resolutionDropdown == null || qualityDropdown == null || fullscreenToggle == null)
        {
            Debug.LogError("One or more UI elements are not assigned in the inspector.");
            return;
        }

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRateRatio.numerator + "/" + resolutions[i].refreshRateRatio.denominator + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width
                  && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);

        // Привязка метода к событию вручную
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        // Логируем текущее значение флага
        Debug.Log("SetFullscreen called with value: " + isFullscreen);
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Разрешение изменено");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("Качество изменено");
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetInt("FullscreenTogglePreference", fullscreenToggle.isOn ? 1 : 0);
        Debug.Log("Настройки сохранены");
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        if (qualityDropdown != null)
        {
            if (PlayerPrefs.HasKey("QualitySettingPreference"))
                qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
            else
                qualityDropdown.value = 3;
        }
        else
        {
            Debug.LogError("qualityDropdown is not assigned in the inspector.");
        }

        if (resolutionDropdown != null)
        {
            if (PlayerPrefs.HasKey("ResolutionPreference"))
                resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
            else
                resolutionDropdown.value = currentResolutionIndex;
        }
        else
        {
            Debug.LogError("resolutionDropdown is not assigned in the inspector.");
        }

        if (PlayerPrefs.HasKey("FullscreenPreference"))
        {
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        }
        else
        {
            Screen.fullScreen = true;
        }

        if (fullscreenToggle != null)
        {
            if (PlayerPrefs.HasKey("FullscreenTogglePreference"))
                fullscreenToggle.isOn = PlayerPrefs.GetInt("FullscreenTogglePreference") == 1;
            else
                fullscreenToggle.isOn = true; // Значение по умолчанию
        }
        else
        {
            Debug.LogError("fullscreenToggle is not assigned in the inspector.");
        }
        Debug.Log("Настройки загружены");
    }
}
