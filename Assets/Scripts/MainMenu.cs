using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject audioManager;

    public Text highscore;
	int m_Score;

	void Start (){
		SetText();
        
    }

	void SetText()
	{
		m_Score = PlayerPrefs.GetInt("Score", 0);
		highscore.text = "HIGHSCORE: " + m_Score;
	}

	public void playGame () {
        audioManager = GameObject.FindGameObjectWithTag("audio");
        DontDestroyOnLoad(audioManager);
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);

	}
	
	public void quitGame () {
		Debug.Log ("QUIT");
	}
		
}
