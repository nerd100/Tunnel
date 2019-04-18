using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class meshBehavior : MonoBehaviour {

	Mesh mesh;

	Vector3[] vertices;
	int[] triangles;
	//Color[] colors;

    public Material mMaterial;

    //lists for points, vertices, triangles and uv
	List<Vector2> points = new List<Vector2>();
	public List<Vector3> verticeList  = new List<Vector3>();
    public List<int> triangleList = new List<int>();
    List<Vector2> uvList = new List<Vector2>();

    //uv for mapping an material to the mesh
    Vector2[] uv = new Vector2[4];
    Vector3 lastLeftVertice = new Vector3();
    Vector3 lastRightVertice = new Vector3();

    EdgeCollider2D edgeCollider;


    //triangle counter is necessary to extand the triangleList
    int triangleCounter;


    private Vector2 startCollisionPoint = new Vector2(20,0);
    private Vector3 intializeMesh = new Vector3();

    private float velocity;

    public void setVelocity(float value)
    {
        velocity = value;
    }

    private int verticeNumber = 0;

    public int getVerticeNumber()
    {
        return verticeNumber;
    }

    private float lastVerticePosition = 0;
    public float getLastVerticePosition()
    {
        return lastVerticePosition;
    }

    public bool left;


	void Awake(){
		mesh = GetComponent<MeshFilter> ().mesh;
        gameObject.GetComponent<MeshRenderer>().material = mMaterial;
    }

    void Start() {
		GM.gameIsRunning = true;
		triangleCounter = 2;

        if (left) {	
			edgeCollider = GetComponent<EdgeCollider2D>();
			//points.Add(-startCollisionPoint);
			MakeMeshData (-startCollisionPoint.x, 1.0f);
			CreateMesh ();
		} else {
			edgeCollider = GetComponent<EdgeCollider2D>();
			//points.Add(startCollisionPoint);
			MakeMeshData (startCollisionPoint.x, 1.0f);
			CreateMesh ();
		}
	}

	void FixedUpdate(){
		if (GM.gameIsRunning) {
            //fake movement of the player, mesh moves down
            velocity = GM.speed;
            var move = new Vector3 (0, -1, 0);
			this.gameObject.transform.position += move * velocity * Time.deltaTime;


            //lastVerticePosition = vertices[0].y + this.gameObject.transform.position.y;
            lastVerticePosition = transform.TransformPoint(vertices[0]).y;
        }
	}
    //initilize mesh, call one time
	void MakeMeshData(float offsetX, float offsetY){
        //set points of a plaine mesh
        if (left)
        {
            intializeMesh = new Vector3(0.0f, 0.0f, 0.0f);

            verticeList.Add(intializeMesh);
            verticeList.Add(new Vector3(intializeMesh.x + offsetX, intializeMesh.y, 0.0f));
            verticeList.Add(new Vector3(intializeMesh.x, intializeMesh.y + offsetY, 0.0f));
            verticeList.Add(new Vector3(intializeMesh.x + offsetX, intializeMesh.y + offsetY, 0.0f));
        }
        else
        {
            intializeMesh = new Vector3(0.0f, 0.0f, 0.0f);

            verticeList.Add(new Vector3(intializeMesh.x + offsetX, intializeMesh.y, 0.0f));
            verticeList.Add(intializeMesh);
            verticeList.Add(new Vector3(intializeMesh.x + offsetX, intializeMesh.y + offsetY, 0.0f));
            verticeList.Add(new Vector3(intializeMesh.x, intializeMesh.y + offsetY, 0.0f));
            

        }

        //save the last two points for the next mesh
        lastRightVertice = new Vector3(intializeMesh.x, intializeMesh.y + offsetY, 0.0f);
        lastLeftVertice = new Vector3(intializeMesh.x + offsetX, intializeMesh.y + offsetY, 0.0f);
        
        //uvList.Add(new Vector2(1, 0));
        //uvList.Add(new Vector2(0, 0));
       // uvList.Add(new Vector2(1, 1));
        //uvList.Add(new Vector2(0, 1));

        uvList.Add(new Vector2(1, 0));
        uvList.Add(new Vector2(0, 0));
        uvList.Add(new Vector2(1, 1));
        uvList.Add(new Vector2(0, 1));

        triangleList.Add(0);
        triangleList.Add(1);
        triangleList.Add(2);
        triangleList.Add(2);
        triangleList.Add(1);
        triangleList.Add(3);

        points.Add(new Vector2(intializeMesh.x, intializeMesh.y));
		points.Add(new Vector2(intializeMesh.x, intializeMesh .y + offsetY));

        uv = uvList.ToArray();
        vertices = verticeList.ToArray();
        triangles = triangleList.ToArray();

        edgeCollider.points = points.ToArray();

    }


	public void CreateMesh(){

       
        mesh.Clear ();
        mesh.vertices = vertices;
        mesh.uv = uv;
		mesh.triangles = triangles;
        //colors = new Color[newVertices.Length];
        //for (int i = 0; i < newVertices.Length; i++) {
        //colors [i] = Random.ColorHSV (0, 1);
        //colors [i] = Color.black;
        //}
        //mesh1.colors = colors;
        //mesh1.uv = UV_MaterialDisplay;
        mesh.RecalculateBounds();
        verticeNumber = vertices.Length;
        edgeCollider.points = points.ToArray();

    }


    public void addVertice(float X, float Y, float offsetX, string direction)
    {
   

        if (direction.Equals("left"))
        {
            verticeList.Add(lastRightVertice);
            verticeList.Add(lastLeftVertice);
            verticeList.Add(new Vector3(X, Y, 0.0f));
            verticeList.Add(new Vector3(X + offsetX, Y, 0.0f));
        }
        else
        {
            verticeList.Add(lastLeftVertice);
            verticeList.Add(lastRightVertice);
            verticeList.Add(new Vector3(X + offsetX, Y, 0.0f));
            verticeList.Add(new Vector3(X, Y, 0.0f));
        }

        lastRightVertice = new Vector3(X, Y, 0.0f);
        lastLeftVertice = new Vector3(X + offsetX, Y, 0.0f);

        uvList.Add(new Vector2(1, 0));
        uvList.Add(new Vector2(0, 0));
        uvList.Add(new Vector2(1, 1));
        uvList.Add(new Vector2(0, 1));

        triangleList.Add(2 + triangleCounter);
        triangleList.Add(3 + triangleCounter);
        triangleList.Add(4 + triangleCounter);
        triangleList.Add(4 + triangleCounter);
        triangleList.Add(3 + triangleCounter);
        triangleList.Add(5 + triangleCounter);


        uv = uvList.ToArray();
        vertices = verticeList.ToArray();
        triangles = triangleList.ToArray();
        
        points.Add(new Vector2(X, Y));
        
        
        
        triangleCounter+=4;

        CreateMesh();
    }

    public void deleteVertice(string position) {

        verticeList.RemoveAt(0);
        verticeList.RemoveAt(0);
        verticeList.RemoveAt(0);
        verticeList.RemoveAt(0);


        triangleList.RemoveAt(0);
        triangleList.RemoveAt(0);
        triangleList.RemoveAt(0);
        triangleList.RemoveAt(0);
        triangleList.RemoveAt(0);
        triangleList.RemoveAt(0);


        uvList.RemoveAt(0);
        uvList.RemoveAt(0);
        uvList.RemoveAt(0);
        uvList.RemoveAt(0);



        points.RemoveAt(0);
        triangleList = triangleList.Select(c => { c = c-4; return c; }).ToList();
        triangleCounter -= 4;
        vertices = verticeList.ToArray ();
        triangles = triangleList.ToArray();
        uv = uvList.ToArray();

        verticeNumber = uv.Length;
        CreateMesh();
    }


}
