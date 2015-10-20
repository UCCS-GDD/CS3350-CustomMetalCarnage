﻿using UnityEngine;
using System.Collections;

public class BomberControl : MonoBehaviour 
{
	public int damage;
	private GameObject playerObject;
	public float moveSpeed;
	private Rigidbody2D thisRigidbody;
	private Vector2 forceDirection;
	private Vector3 tempVector;
	private float angleToPlayer;
	private Quaternion targetRotation;
	public float rotationSpeed;
    public SoundManager audioManager;
    public AudioClip explosionSound;

	public GameObject explosionPrefab;
	private GameObject tempObject;

	// Use this for initialization
	void Start () 
	{
		playerObject = GameObject.FindGameObjectWithTag("Player");
		thisRigidbody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		tempVector = (playerObject.transform.position - transform.position).normalized;
		forceDirection = new Vector2(tempVector.x, tempVector.y);
		thisRigidbody.AddForce(forceDirection);
		angleToPlayer = Mathf.Atan2(thisRigidbody.velocity.x, thisRigidbody.velocity.y) * Mathf.Rad2Deg;
		targetRotation = Quaternion.Euler(new Vector3(0f, 0f, -angleToPlayer));
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
		if(thisRigidbody.velocity.magnitude > moveSpeed)
		{
			thisRigidbody.velocity = thisRigidbody.velocity.normalized * moveSpeed;
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.CompareTag("Player"))
		{
			tempObject = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
			//coll.gameObject.GetComponent<PlayerControl>().TakeDamage(damage);
            audioManager.playSound(explosionSound);
			Destroy(this.gameObject);
		}
	}
}
