using UnityEngine;
using System.Collections;

public class plane : MonoBehaviour {
	
	public GameObject charMotor;
	
	private Vector3 screenPoint;
	private Vector3 offset;
	
	public enum Axis {X, Y, Z};
	public Axis axisOfMovement = Axis.X;
	
	private Vector3 initPos;
	
	private float minDisplace = 1;
	private float maxDisplace = 10;
	
	private static float displaceX = 0;
	private static float displaceZ = 0;
	
	private float maxSpeed = 5;
	
	public Camera cam;
	
	// Use this for initialization
	void Start () {
		initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButton(2)) {
			// middle button pressed! Reset positions to initial
			transform.position = initPos;
			displaceX = 0;
			displaceZ = 0;
		}

		// Get the character's script, set the new speed
		CharacterSpeedController charSpeedCont = charMotor.GetComponent<CharacterSpeedController>();
		charSpeedCont.charSpeed = new Vector3((displaceX/maxDisplace) * maxSpeed, 0, (displaceZ/maxDisplace) * maxSpeed);
	}
	
	void OnMouseDown() {
		// set up offset
		screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
		
		switch (axisOfMovement) {
			case Axis.X:
				offset = this.transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, screenPoint.y, screenPoint.z));
				break;
			case Axis.Y:
				offset = this.transform.position - cam.ScreenToWorldPoint(new Vector3(screenPoint.x, Input.mousePosition.y, screenPoint.z));
				break;
			case Axis.Z:
				offset = this.transform.position - cam.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, Input.mousePosition.y));	
				break;
		}
	}
	
	void OnMouseDrag() {
		// move face to match mouse position... mostly.

		Vector3 curScreenPoint = new Vector3(); 
		Vector3 curPosition = new Vector3();

		switch (axisOfMovement) {
			case Axis.X:
				curScreenPoint = new Vector3(Input.mousePosition.x, screenPoint.y, screenPoint.z);
				curPosition = cam.ScreenToWorldPoint(curScreenPoint) + offset;
				curPosition = new Vector3(curPosition.x, transform.position.y, transform.position.z);

				// if we've reached the limit, stop there
				if (Vector3.Distance(new Vector3(curPosition.x, 0, 0), new Vector3(initPos.x, 0, 0)) > maxDisplace) {
					return;
				}

				// set displacement
				if (initPos.x >= curPosition.x) {
					displaceX = -Vector3.Distance(new Vector3(curPosition.x, 0, 0), new Vector3(initPos.x, 0, 0));
				} else {
					displaceX = Vector3.Distance(new Vector3(curPosition.x, 0, 0), new Vector3(initPos.x, 0, 0));	
				}				

    			break;
			
			case Axis.Y:
				curScreenPoint = new Vector3(screenPoint.x, Input.mousePosition.y, screenPoint.z);
				curPosition = cam.ScreenToWorldPoint(curScreenPoint) + offset;
				break;

			case Axis.Z:
				curScreenPoint = new Vector3(screenPoint.x, screenPoint.y, Input.mousePosition.y);
				curPosition = cam.ScreenToWorldPoint(curScreenPoint) + offset;
				curPosition = new Vector3(transform.position.x, transform.position.y, curPosition.z);

				// if we've reached the limit, stop there
				if (Vector3.Distance(new Vector3(curPosition.z, 0, 0), new Vector3(initPos.z, 0, 0)) > maxDisplace + 10) {
					return;
				}

				// set displacement
				if (initPos.z >= curPosition.z) {
					displaceZ = -Vector3.Distance(new Vector3(curPosition.z, 0, 0), new Vector3(initPos.z, 0, 0));
				} else {
					displaceZ = Vector3.Distance(new Vector3(curPosition.z, 0, 0), new Vector3(initPos.z, 0, 0));	
				}

				break;
		}
		
		
		transform.position = curPosition;
		
	}
}
