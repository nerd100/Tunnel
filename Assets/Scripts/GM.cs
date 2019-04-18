using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GM : MonoBehaviour {


	public int scoreTimer = 0;
	public static bool gameIsRunning = true;
    public static float speed = 0.1f;
    public static float gap = 0.01f;

    public TextMeshProUGUI score;

    public GameObject panel;
	public TextMeshProUGUI panelScore;


    public GameObject startBar1;
    public GameObject startBar2;

    public void setGameStatePause() {
        PauseGame();
    }
    public void setGameStateStart()
    {
        ContinueGame();
    }

    void Start () {
		score.text = "Score: 0";
        Instantiate(startBar1, new Vector3(-20, -20.8f, 0.0f), Quaternion.identity);
        Instantiate(startBar2, new Vector3(20, -20.8f, 0.0f), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		if (!gameIsRunning) {
            panelScore.text = "Your " + score.text; 
			panel.SetActive (true); 
            //PauseGame();
            
        }

     


        else {

            if (Input.touchCount > 0 )
            {
                Touch touch = Input.GetTouch(0);

                // Move the cube if the screen has the finger moving.
                if (touch.phase == TouchPhase.Began)
                {
                    Time.timeScale = 0.5f;

                }
                else if (touch.phase == TouchPhase.Canceled)
                {
                    Time.timeScale = 1.0f;
                }
            }
            if (Input.GetMouseButton(0)){
                if (Input.GetMouseButtonDown(0))
                {
                    Time.timeScale = 0.1f;

                }
                else if (Input.GetMouseButtonUp(0))
                {
                    Time.timeScale = 1.0f;
                }

            }
        
            scoreTimer++;
            score.text = "Score: " + scoreTimer;
            if (scoreTimer % 10 == 0)
            {
                speed += 0.05f;
            }
        }
	}

	public void exitGame(){
		if (scoreTimer > PlayerPrefs.GetInt ("Score", 0)) {
			PlayerPrefs.SetInt ("Score", scoreTimer);
		}
        speed = 0.1f;
        gap = 0.01f;
        gameIsRunning = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
	}

    public void retryGame()
    {
        if (scoreTimer > PlayerPrefs.GetInt("Score", 0))
        {
            PlayerPrefs.SetInt("Score", scoreTimer);
        }
        gameIsRunning = true;
        speed = 0.1f;
        gap = 0.01f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
    }

}
