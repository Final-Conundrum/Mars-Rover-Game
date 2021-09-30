using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Setting_Audio : MonoBehaviour
{
    public AudioMixer mixer_SFX;
    public AudioMixer mixer_Background;

    // Attach to slider to set audio
    public void SetAudioLevel_SFX(float sliderValue)
    {   
        mixer_SFX.SetFloat("ObjectMasterVol", Mathf.Log10(sliderValue) * 20);       
    }

    public void SetAudioLevel_PlayerSFX(float sliderValue)
    {
        mixer_SFX.SetFloat("PlayerVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetAudioLevel_Music(float sliderValue)
    {
        mixer_Background.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetAudioLevel_Ambience(float sliderValue)
    {
        mixer_Background.SetFloat("AmbienceVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetAudioLevel_TTS(float sliderValue)
    {
        mixer_Background.SetFloat("TTSVol", Mathf.Log10(sliderValue) * 20);
    }
}
