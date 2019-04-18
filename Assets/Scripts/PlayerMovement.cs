using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour {


	//public bool gameIsRunning = true;
    float initialOrientationY = 0.0f;
    float currentDeviceAngle = 0.0f;
    public float delay = 0f;
    public GM gameManager;
    public Animator animator;
    public TextMeshProUGUI GyroY;


    void Start () {
        Input.gyro.enabled = true;
        transform.position = new Vector2 (0, -20);
        
		//gameManager = GameObject.FindWithTag ("GM");
	}


	// Update is called once per frame
	void FixedUpdate() {
		if (GM.gameIsRunning) {

#if UNITY_STANDALONE || UNITY_WEBPLAYER

			var move = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			transform.position += move * speed * Time.deltaTime;
			transform.eulerAngles = new Vector3 (0, 0, -30.0f * move.x);

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

            if (true)
            {
                initialOrientationY = Input.gyro.rotationRate.y;
                currentDeviceAngle += initialOrientationY;
            }
            else
            {
                initialOrientationY = 0;
                currentDeviceAngle += initialOrientationY;
            }
            
            GyroY.text = "GyroY = "+ currentDeviceAngle.ToString();
			transform.position += new Vector3(currentDeviceAngle / 100 ,0,0);
			transform.eulerAngles = new Vector3 (0, 0, -30.0f * initialOrientationY);

			#endif
		}

	}

	//stop game if player collide with wall
	void OnTriggerEnter2D(Collider2D other) 
	{
        animator.SetBool("Death", true);
        Destroy(this.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        GM.gameIsRunning = false;
    }
}
