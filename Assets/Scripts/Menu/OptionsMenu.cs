using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;
    public AudioMixer audioMixer;
    void Awake()
    {
        if (optionsMenu == null)
        {
            Debug.LogError("OptionsMenu GameObject not found in the scene.");
        }
        if (mainMenu == null)
        {
            Debug.LogError("MainMenu GameObject not found in the scene.");
        }
    }
    public void Back()
    {
        if (optionsMenu != null && mainMenu != null)
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("OptionsMenu or MainMenu GameObject is null. Cannot perform Back action.");
        }
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("mainVolume", volume);
    }
}
