using UnityEngine;
using System.Collections;

public class PullerControl : MonoBehaviour 
{
	public float force;
	private Vector3 tempVector;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if(coll.CompareTag("Enemy"))
		{
			tempVector = (transform.position - coll.transform.position).normalized * force;
			coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(tempVector.x, tempVector.y));
		}
	}
}
