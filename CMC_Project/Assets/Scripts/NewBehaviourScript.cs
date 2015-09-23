using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Vector3 ePosition = new Vector3(Random.Range(-3.0F, 3.0F), 0, Random.Range(-3.0F, 3.0F));

		this.GetComponent<Rigidbody>().AddExplosionForce (400, ePosition, 1000, 3);
		Debug.Log("" + ePosition.x + " " + ePosition.z);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
