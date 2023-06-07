using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEvents : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer mixer;
    private float value;

    private void Start()
    {
        Time.timeScale = 1;
        mixer.GetFloat("volume", out value);
        volumeSlider.value = value;
    }
    public void SetVolume()
    {
        mixer.SetFloat("volume", volumeSlider.value);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
