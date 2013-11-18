using UnityEngine;
using System.Collections;

public class CharacterSpeedController : MonoBehaviour {

	public Vector3 charSpeed = new Vector3(0, 0, 0);
	public GameObject actualChar;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		charSpeed = actualChar.transform.rotation * charSpeed; // Use rotation to transform speed vector to match orientation
		
		Vector3 newPos = new Vector3(transform.position.x + charSpeed.x, transform.position.y + charSpeed.y, transform.position.z + charSpeed.z);
		actualChar.transform.position = newPos;

	}
	
	void setSpeed(Vector3 newSpeed) {
		charSpeed = newSpeed;
	}
	
	void setXSpeed(float xSpeed) {
		charSpeed.x = xSpeed;
	}
	
	void setZSpeed(float zSpeed) {
		charSpeed.z = zSpeed;
	}
}
