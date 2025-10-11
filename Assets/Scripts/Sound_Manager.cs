using UnityEngine;
using UnityEngine.UI;
public class Sound_Manager : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (audioSource != null && volumeSlider != null)
        {
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
        }
    }
    public void OpenSettingsPanel()
    {

    }

    public void CloseSeetingsPanel()
    {

    }
}
