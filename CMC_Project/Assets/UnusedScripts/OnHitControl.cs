using UnityEngine;
using System.Collections;

public class OnHitControl : MonoBehaviour 
{

	public float lerpSpeed;
	public int color;
	public float orbiterPower;


	// Use this for initialization
	void Start () 
	{
	
	}


	// Update is called once per frame
	void Update () 
	{
		if(Vector3.Distance(transform.localPosition, Vector3.zero)>0.1f)
		{
			// Lerp to parent position
			transform.localPosition = Vector3.Lerp (transform.localPosition, Vector3.zero, lerpSpeed);
		}
	}





}
