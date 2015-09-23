using UnityEngine;
using System.Collections;

public class KillMeshControl : MonoBehaviour 
{
	public Vector3 teleportLocation;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log (other.tag);
		if(other.CompareTag("Player1")||other.CompareTag("Player2")||other.CompareTag("Player3")||other.CompareTag("Player4"))
		{
			other.transform.position = teleportLocation;
		}
	}
}
