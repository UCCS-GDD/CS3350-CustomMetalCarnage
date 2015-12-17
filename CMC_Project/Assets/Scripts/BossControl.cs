using UnityEngine;
using System.Collections;

public class BossControl : MonoBehaviour 
{
	public int health;
	public int points;
	public float stopDistance;
	private GameObject targetObject;
	public float moveSpeed;
	private Rigidbody2D thisRigidbody;
	private Vector2 forceDirection;
	private Vector3 tempVector;

	public AudioClip explosionSound;
    public AudioClip bossMusic;
	
	public GameObject explosionPrefab;

	public GameObject topWall;
	public GameObject leftWall;
	public GameObject rightWall;
	public GameObject bottomWall;

	public GameObject targetPrefab;
	private int targetNumber;

	private bool circuitComplete = false;
	//	private GameObject tempObject;
	
	delegate void Shoot();
	Shoot shoot;
	
	// Use this for initialization
	void Start () 
	{
		targetObject = Instantiate(targetPrefab, topWall.transform.position, Quaternion.identity) as GameObject;
		thisRigidbody = this.GetComponent<Rigidbody2D>();
        SoundManager.singleton.playSustainedSound(bossMusic);
		//audioManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		
		foreach(Transform child in transform)
		{
			shoot += child.GetComponent<WeaponControl>().FireCall;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		tempVector = (targetObject.transform.position - transform.position).normalized;
		forceDirection = new Vector2(tempVector.x, tempVector.y);
		//Debug.Log ("Distance = " + Vector3.Distance (targetObject.transform.position, transform.position));
		if((moveSpeed > 0) && (Vector3.Distance(targetObject.transform.position, transform.position) > stopDistance))
		{
			thisRigidbody.AddForce(forceDirection);
		}
		else
		{
			thisRigidbody.velocity = Vector2.zero;
			if(circuitComplete)
			{
				targetNumber = Random.Range(1, 5);
			}
			else
			{
				targetNumber++;
				if(targetNumber > 4)
				{
					circuitComplete = true;
					targetNumber = 1;
				}
			}


			switch(targetNumber)
			{
			case 1:
				targetObject.transform.position = topWall.transform.position;
				break;
			case 2:
				targetObject.transform.position = rightWall.transform.position;
				break;
			case 3:
				targetObject.transform.position = bottomWall.transform.position;
				break;
			case 4:
				targetObject.transform.position = leftWall.transform.position;
				break;
			}
		}
		if(shoot!=null)
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
		if(explosionSound!=null)
		{
			//audioManager.playModulatedSound(explosionSound, .3f);
		}
		Destroy(this.gameObject);
	}


	void OnDestroy()
	{
		foreach(ProjectileControl item in GameObject.FindObjectsOfType<ProjectileControl>())
		{
			Destroy(item.gameObject);
		}
		
		GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerControl>().retry_Button.SetActive(true);
		GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerControl>().menu_Button.SetActive(true);
		GameManagerControl.ShowMessage("Congratulations! You are the Champion of the Arena!");
	}

}
