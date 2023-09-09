using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _uiAudioSource;

    [SerializeField] 
    private Slider _volumeSlider;

    [SerializeField]
    private AudioClip _controlPressed;
    [SerializeField]
    private AudioClip _controlError;

    public void PlayControlPressed()
    {
        _uiAudioSource.PlayOneShot(_controlPressed);
    }

    public void PlayControlError() 
    {
        _uiAudioSource.PlayOneShot(_controlError);
    }
}
