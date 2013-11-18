using UnityEngine;
using System.Collections;

public class CubeCamera : MonoBehaviour {
	
	public Transform target;
	
	public float sensitivityX = 15F;
    public float sensitivityY = 15F;
	public float minimumY = -60F;
    public float maximumY = 60F;
	public float minimumX = -360F;
    public float maximumX = 360F;
	
	float rotationY = 0F;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
	}
}
