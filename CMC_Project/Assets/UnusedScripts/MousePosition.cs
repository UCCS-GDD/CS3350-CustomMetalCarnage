using UnityEngine;
using System.Collections;

public class MousePosition : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = transform.parent.GetComponent<MouseAim> ().targetPosition;
	}
}
