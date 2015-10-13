using UnityEngine;
using System.Collections;

public class ProjectileControl : MonoBehaviour 
{
	public float travelSpeed = 1f;
	public float timeAfterColl = 0f;
	public int damage = 1;
	public GameObject explosionPrefab;

	private GameObject tempObject;
	private Rigidbody2D thisRigidbody;
	private Vector2 travelVelocity;

	private float collisionTime = 0f;
	private bool exploded = false;

	// Use this for initialization
	void Start () 
	{
		thisRigidbody = this.GetComponent<Rigidbody2D>();
		RefreshVelocity();
	}

	void FixedUpdate () 
	{
		if(!exploded)
		{
			thisRigidbody.velocity = travelVelocity;
		}
		else
		{
			thisRigidbody.velocity = Vector2.zero;
			if(Time.time > (collisionTime+timeAfterColl))
			{
				this.gameObject.SetActive(false);
			}
		}
	}

	public void RefreshVelocity()
	{
		exploded = false;
		travelVelocity = new Vector2(-Mathf.Sin(Mathf.Deg2Rad*transform.rotation.eulerAngles.z)*travelSpeed, Mathf.Cos(Mathf.Deg2Rad*transform.rotation.eulerAngles.z)*travelSpeed);
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log ("Hit " + coll.gameObject.tag);
		switch(coll.gameObject.tag)
		{
		case "Wall":
		case "Enemy":
			exploded = true;
			this.GetComponent<Collider2D>().enabled = false;
			collisionTime = Time.time;
			OnHitEffects(coll.gameObject);
			break;
		}
	}


	void OnHitEffects(GameObject other)
	{
		if(other.tag == "Enemy")
		{
			//other.GetComponent<EnemyControl>().TakeDamage(damage);
			Destroy(other.gameObject);
		}

		// Spawn explosion particles
		tempObject = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;

	}

}
