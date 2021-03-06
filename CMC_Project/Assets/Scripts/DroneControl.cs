﻿using UnityEngine;
using System.Collections;

public class DroneControl : MonoBehaviour 
{
	public float movementSpeed = 1f;
	private Rigidbody2D thisRigidbody;
	private Vector2 inputs;
	private Vector2 inputsSnap;
	private float dragVal;
    Animator anim;

	// Use this for initialization
	void Start () 
	{
		thisRigidbody = gameObject.GetComponent<Rigidbody2D>();
		dragVal = thisRigidbody.drag;
        anim = GetComponent<Animator>();

		StartCoroutine("MoveCoroutine");
	}

	IEnumerator MoveCoroutine()
	{
		while(true)
		{
			//Debug.Log("X:"+Input.GetAxis("Horizontal")+"  Y:"+ Input.GetAxis("Vertical"));
			inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			inputsSnap = new Vector2(Input.GetAxis("HorizontalSnap"), Input.GetAxis("VerticalSnap"));

			if(inputsSnap.magnitude==0)
			{
				thisRigidbody.drag = 100f;
			}
			else
			{
				thisRigidbody.drag = dragVal; 
				thisRigidbody.AddForce(inputs, ForceMode2D.Impulse);
			}
			//Debug.Log(thisRigidbody.drag);

			// Clamp max velocity
			if(thisRigidbody.velocity.magnitude > movementSpeed)
			{
				thisRigidbody.velocity = thisRigidbody.velocity.normalized * movementSpeed;
			}

            // Animate based on speed
            anim.SetFloat("Speed", thisRigidbody.velocity.magnitude);

			yield return null;
		}
	}
}
