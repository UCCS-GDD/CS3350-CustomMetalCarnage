using UnityEngine;
using System.Collections;

public class MouseAim : MonoBehaviour 
{
	public Vector3 targetPosition;
	private Camera myCamera;
	private Ray ray;
	RaycastHit hit;

	// Use this for initialization
	void Start () 
	{
		myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		ray = myCamera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
			transform.LookAt(targetPosition);
		}
			
	}
}
