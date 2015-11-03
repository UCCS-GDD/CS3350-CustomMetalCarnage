using UnityEngine;
using System.Collections;

public class BasicEnemyControl : MonoBehaviour 
{
	public int health;
	public int points;
	public float stopDistance;
	public float shootDistance;
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
//	private GameObject tempObject;

	delegate void Shoot();
	Shoot shoot;

	// Use this for initialization
	void Start () 
	{
		playerObject = GameObject.FindGameObjectWithTag("Player");
		thisRigidbody = this.GetComponent<Rigidbody2D>();
		audioManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

		foreach(Transform child in transform)
		{
			shoot += child.GetComponent<WeaponControl>().FireCall;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		tempVector = (playerObject.transform.position - transform.position).normalized;
		forceDirection = new Vector2(tempVector.x, tempVector.y);
		angleToPlayer = Mathf.Atan2(tempVector.x, tempVector.y) * Mathf.Rad2Deg;
		targetRotation = Quaternion.Euler(new Vector3(0f, 0f, -angleToPlayer));
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
		//Debug.Log ("Distance = " + Vector3.Distance (playerObject.transform.position, transform.position));
		if(Vector3.Distance(playerObject.transform.position, transform.position) > stopDistance)
		{
			thisRigidbody.AddForce(forceDirection);
		}
		else
		{
			thisRigidbody.velocity = Vector2.zero;
		}
		if(Vector3.Distance(playerObject.transform.position, transform.position) < shootDistance)
		{
			shoot();
		}
		if(thisRigidbody.velocity.magnitude > moveSpeed)
		{
			thisRigidbody.velocity = thisRigidbody.velocity.normalized * moveSpeed;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{

	}


	public void TakeDamage(int incomingDamage)
	{
		health -= incomingDamage;
		if(health <= 0)
		{
			DestroyEnemy();
		}
	}

	
	public void DestroyEnemy()
	{
		GameManagerControl.playerScore += points;
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		audioManager.playSound(explosionSound, .3f);
		Destroy(this.gameObject);
	}
}
