using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {


    public AudioClip otherClip;
    public GameObject audioPrefab;
    public static bool playSong = false;
    public AudioMixer audioMixer;
    public Slider MusicSlider;


    void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("volume", 0.0f);//wird erst aufgerufen wenn das Optionsmenu geffnet wird und dann nur einmal
        if (playSong == false)
        { //falls der Song nicht gespielt wird, dann starte ihn

            audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("volume", 0.0f));
            //MusicSlider.value = PlayerPrefs.GetFloat ("volume", 0.0f);
            GameObject audioGameObject = Instantiate(audioPrefab, Vector3.zero, Quaternion.identity);
            audioGameObject.GetComponentInChildren<AudioSource>().clip = otherClip;
            audioGameObject.GetComponentInChildren<AudioSource>().Play();
            playSong = true;
        }
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
        //PlayerPrefs.Save ();
    }


}
