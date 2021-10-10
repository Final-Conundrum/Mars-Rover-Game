using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    private bool isFullscreen = false;
    public static bool postProcessingActive = true;

    public GameObject fullscreenONButton;
    public GameObject fullscreenOFFButton;

    public GameObject postProcessingONButton;
    public GameObject postProcessingOFFButton;

    public void Start()
    {
        fullscreenONButton.SetActive(false);

    }

    // Screen resolution settings
    public void ScreenSize1620()
    {
        Screen.SetResolution(2880, 1620, isFullscreen);
    }

    public void ScreenSize1080()
    {
        Screen.SetResolution(1920, 1080, isFullscreen);
    }

    public void ScreenSize540()
    {
        Screen.SetResolution(960, 540, isFullscreen);
    }

    public void ScreenSizeFullScreen(bool enabled)
    {
        Screen.fullScreen = enabled;
        isFullscreen = enabled;

        if(enabled == true)
        {
            fullscreenONButton.SetActive(false);
            fullscreenOFFButton.SetActive(true);
        }
        else
        {
            fullscreenONButton.SetActive(true);
            fullscreenOFFButton.SetActive(false);
        }
    }

    // Graphics settings
    public void PostProcessing(bool enabled)
    {
        postProcessingActive = enabled;

        if (enabled == true)
        {
            postProcessingONButton.SetActive(false);
            postProcessingOFFButton.SetActive(true);
        }
        else
        {
            postProcessingONButton.SetActive(true);
            postProcessingOFFButton.SetActive(false);
        }
    }

    // Audio settings
}
