using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioParameter : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;


    public void SetMasterLevel()
    {
        float level = masterSlider.value;
        mixer.SetFloat("masterVol", level);
    }

    public void SetMusicLevel()
    {
        float level = musicSlider.value;
        mixer.SetFloat("musicVol", level);
    }

    public void SetSFXLevel()
    {
        float level = sfxSlider.value;
        mixer.SetFloat("sfxVol", level);
    }

}
