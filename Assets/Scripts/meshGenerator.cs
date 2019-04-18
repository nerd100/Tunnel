using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshGenerator : MonoBehaviour {

	// Use this for initialization
	public GameObject mesh1;
	public GameObject mesh2;

    //left and right mesh
	GameObject tempMesh1;
	GameObject tempMesh2;

    //parameter for updating and deleting the mesh
	bool updateMesh = true;
	bool deleteVertice = true;

    //random numbers for the next X and Y Coordinate of the next mesh point
	float randNumberX;
	float randNumberY;

    //start Position
	float startXValue;
	float startYValue;

    float nextVerticePositionX;
    float nextVerticePositionY;

    float lastVerticePosition = 0.0f; 
    //delete mesh after counter
    int verticeCounter = 0;

    bool del = false;
    //initialze mesh
    void Start () {

        nextVerticePositionX = 0.0f;
        nextVerticePositionY = 1.0f;


        //GM.gameIsRunning = true;
        startXValue = 10.0f;
		startYValue = 0.0f;
		//left site
	    tempMesh1 = Instantiate (mesh1, new Vector3(-startXValue, startYValue , 0.0f), Quaternion.identity);
		tempMesh1.GetComponent<meshBehavior> ().left = true;
		//right site
		tempMesh2 = Instantiate (mesh2, new Vector3(startXValue, startYValue, 0.0f), Quaternion.identity);
		tempMesh2.GetComponent<meshBehavior> ().left = false;

        
        //StartCoroutine(extendMesh());
        

	}
		
	void FixedUpdate () {

        verticeCounter = tempMesh1.GetComponent<meshBehavior>().getVerticeNumber();
        lastVerticePosition = tempMesh1.GetComponent<meshBehavior>().getLastVerticePosition();
        //Debug.Log(lastVerticePosition);
        if (updateMesh && GM.gameIsRunning && verticeCounter < 100)
        {
            StartCoroutine(extendMesh());
        }
        if (deleteVertice && lastVerticePosition < -40 && verticeCounter >= 100)//deleteVertice && lastVerticePosition < -10 && verticeCounter >= 100
        {   //delete every 5 sec the first vertices; TODO: delete if vertice under position and not after time
            StartCoroutine (deleteMesh ());
        }
	}

	//general function to extend the mesh upwards
	IEnumerator extendMesh(){

		randNumberX = Random.Range (-5.0f, 5.0f);
		randNumberY = Random.Range (3.0f , 5.0f);
        nextVerticePositionX += randNumberX;
        nextVerticePositionY += randNumberY;

		updateMesh = false;
                
        yield return new WaitForSeconds(0.0f);//wait 3 sec befor extend
        tempMesh1.GetComponent<meshBehavior>().addVertice(nextVerticePositionX, nextVerticePositionY, -20.0f, "left"); //call function addVertice() in meshBehavior
        //tempMesh1.GetComponent<meshBehavior>().CreateMesh();
        tempMesh2.GetComponent<meshBehavior> ().addVertice (nextVerticePositionX -10*GM.gap, nextVerticePositionY,  20.0f, "right"); //call function addVertice() in meshBehavior
        updateMesh = true;
        //del = true;


	}

	IEnumerator deleteMesh(){

        //deleteVertice = false; 
        deleteVertice = false;
        yield return new WaitForSeconds (0.0f);
        
		tempMesh1.GetComponent<meshBehavior> ().deleteVertice ("left"); //call function deleteVertice() in meshBehavior
		tempMesh2.GetComponent<meshBehavior> ().deleteVertice ("right"); //call function deleteVertice() in meshBehavior

		deleteVertice = true;


	}

}
