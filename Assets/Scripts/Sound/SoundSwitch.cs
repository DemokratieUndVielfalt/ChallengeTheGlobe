using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSwitch : MonoBehaviour
{
    bool off = false;
    [SerializeField] GameObject offImage;
    [SerializeField] GameObject onImage;

    [SerializeField] AudioMixer mixer;

    private void Start()
    {
        offImage.SetActive(false);
    }
    public void OnClickSoundSwitch()
    {
        if (off)
        {
            mixer.SetFloat("MasterVolume", 0);
            offImage.SetActive(false);
            onImage.SetActive(true);
            off = false;
        }
        else
        {
            mixer.SetFloat("MasterVolume", -80);

            offImage.SetActive(true);
            onImage.SetActive(false);
            off = true;

        }
    }
}
