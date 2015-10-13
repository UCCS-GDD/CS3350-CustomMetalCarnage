using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Vector3 offset;
	public float size;

	// Use this for initialization
	void Start () 
	{
		transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
		transform.localPosition = offset;
		transform.GetComponent<Camera> ().orthographicSize = size;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.rotation = Quaternion.identity;
	}
}
