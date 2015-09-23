using UnityEngine;
using System.Collections;

public class CoreRotation : MonoBehaviour 
{

	public float xRotationSpeed;
	public float yRotationSpeed;
	public float zRotationSpeed;

	public float xInitialRotation;
	public float yInitialRotation;
	public float zInitialRotation;

	private Vector3 rotationPerFrame;

	// Use this for initialization
	void Start () 
	{
		xInitialRotation = xRotationSpeed;
		yInitialRotation = yRotationSpeed;
		zInitialRotation = zRotationSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		rotationPerFrame.x = xRotationSpeed * Time.deltaTime;
		rotationPerFrame.y = yRotationSpeed * Time.deltaTime;
		rotationPerFrame.z = zRotationSpeed * Time.deltaTime;

		this.transform.localRotation = this.transform.localRotation * Quaternion.Euler (rotationPerFrame);
	}
}
