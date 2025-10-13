using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class Sound_Manager : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;
    private const string volumeKey = "VolumeLevel";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (audioSource != null && volumeSlider != null)
        {
            audioSource.volume = PlayerPrefs.GetFloat(volumeKey, 0.7f);
            volumeSlider.value = audioSource.volume;
            // Add a listener to the slider's OnValueChanged event
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            PlayerPrefs.SetFloat(volumeKey, volume);
            PlayerPrefs.Save();
        }
    }
    public void OpenSettingsPanel()
    {

    }

    public void CloseSeetingsPanel()
    {

    }
}
