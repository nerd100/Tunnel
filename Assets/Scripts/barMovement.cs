using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barMovement : MonoBehaviour
{


    private float velocity;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GM.gameIsRunning)
        {
            velocity = GM.speed;
            var move = new Vector3(0, -1, 0);
            this.gameObject.transform.position += move * velocity * Time.deltaTime;
        }
        
        if(this.gameObject.transform.position.y < -50)
        {
            Destroy(this.gameObject);
        }
    }
}
