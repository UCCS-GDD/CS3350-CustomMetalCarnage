using UnityEngine;
using System.Collections;

public class ForceOnStart : MonoBehaviour 
{
	public float xForceMin;
	public float xForceMax;
	public float yForceMin;
	public float yForceMax;
	private Rigidbody2D thisRigidbody;

	// Use this for initialization
	void Start () 
	{
		thisRigidbody = GetComponent<Rigidbody2D>();
		thisRigidbody.AddForce(new Vector2(Random.Range(xForceMin, xForceMax), Random.Range(yForceMin, yForceMax)));
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
