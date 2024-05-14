using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    public AudioMixer audioMixer;
    public void PlayGame()
    {
        GameObject obj = GameObject.Find("LevelSelector");

        if (obj != null)
        {
            obj.SetActive(true);
            mainMenu.SetActive(false);
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Awake()
    {
        if (mainMenu == null)
        {
            Debug.LogError("MainMenu GameObject not found in the scene.");
        }
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("mainVolume", volume);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
