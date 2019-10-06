using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown graphicsDropdown;
    public TMPro.TMP_Dropdown languagesDropdown;
    public LanguageManager languageManager;
    Resolution[] resolutions;

    private void Start()
    {
        #region Resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (!options.Contains(option))
            {
                options.Add(option);
            }

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        #endregion
        #region Languages
        languagesDropdown.ClearOptions();

        List<string> languagesOptions = new List<string>();

        languagesOptions.Add("English");
        languagesOptions.Add("Portuguese");

        languagesDropdown.AddOptions(languagesOptions);
        languagesDropdown.value = languageManager.activeLanguage == Languages.ENGLISH ? 0 : 1;
        languagesDropdown.RefreshShownValue();
        #endregion
        #region Graphics
        graphicsDropdown.ClearOptions();

        List<string> graphicsOptions = new List<string>();

        graphicsOptions.Add("Low");
        graphicsOptions.Add("Medium");
        graphicsOptions.Add("High");
        graphicsOptions.Add("Ultra");

        graphicsDropdown.AddOptions(graphicsOptions);
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();
        #endregion
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetLanguage()
    {
        languageManager.ChangeLanguage(languagesDropdown.value == 0 ? Languages.ENGLISH : Languages.PORTUGUESE);
    }
}
