using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat(PreferenceConstants.PREFS_VOLUME_KEY, 1f);
        AudioListener.volume = volume;
        volumeSlider.value = volume;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(PreferenceConstants.PREFS_VOLUME_KEY, volume);
    }

}
