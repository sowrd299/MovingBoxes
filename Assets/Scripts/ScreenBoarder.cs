using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoarder : MonoBehaviour
{
	private readonly static Vector2 TOP_RIGHT = new Vector2(1,1);

	[SerializeField]
	private List<Vector3> directions = new List<Vector3>(){
		Vector3.left,
		Vector3.up,
		Vector3.right,
		Vector3.down
	};
	[SerializeField]
	private float distance = 0;
	[SerializeField]
	private GameObject boarderObject;
	[SerializeField]
	private Camera targetCamera; // defaults to main if none set

	private float width = 10f;

    // Start is called before the first frame update
    void Start()
    {
		if(targetCamera == null){
			targetCamera = Camera.main;
		}
		generate();
    }

	// automatically adds an edge on all sides of the given camera
	private void generate(){
		// based on code from: https://answers.unity.com/questions/640325/get-screen-height-in-real-world-coordinates.html
		Vector2 screenEdge = targetCamera.ViewportToWorldPoint(TOP_RIGHT);
		float xDist = screenEdge.x + distance + (width / 2);
		float yDist = screenEdge.y + distance + (width / 2);
		foreach(var d in directions){
			GameObject boarder = Instantiate(boarderObject);
			boarder.transform.position = targetCamera.transform.position + 
				new Vector3(d.x * xDist, d.y * yDist, d.z);
			boarder.transform.localScale = new Vector3(
				d.x == 0 ? xDist * 2 : width, 
				d.y == 0 ? yDist * 2 : width,
				1
			);  
			boarder.transform.parent = transform;
		}
	}

}
