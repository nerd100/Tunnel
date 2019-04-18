using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {


	public GameObject player;

    public GameObject backgroundParalax;

    private float smoothSpeed = 5f;
    // Use this for initialization
    void Start () {
		transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z); 
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            //transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);

            transform.position += new Vector3(player.transform.position.x - transform.position.x, 0, 0) * Time.deltaTime * smoothSpeed;
            backgroundParalax.transform.position += new Vector3(player.transform.position.x - backgroundParalax.transform.position.x, 0, 0) * Time.deltaTime * (smoothSpeed - 2);
        }
    }
}
