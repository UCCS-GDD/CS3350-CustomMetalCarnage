using UnityEngine;
using System.Collections;

public class ProjectileControl : MonoBehaviour 
{
	public float travelSpeed = 1f;
	public int damage = 1;
	public GameObject explosionPrefab;

	private GameObject tempObject;
	private Rigidbody2D thisRigidbody;
	private Vector2 travelVelocity;

	// Use this for initialization
	void Start () 
	{
		thisRigidbody = this.GetComponent<Rigidbody2D>();
		RefreshVelocity();
	}

	void FixedUpdate () 
	{
		thisRigidbody.velocity = travelVelocity;
	}

	public void RefreshVelocity()
	{
		travelVelocity = new Vector2(-Mathf.Sin(Mathf.Deg2Rad*transform.rotation.eulerAngles.z)*travelSpeed, Mathf.Cos(Mathf.Deg2Rad*transform.rotation.eulerAngles.z)*travelSpeed);
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log ("Hit " + coll.gameObject.tag);
		switch(coll.gameObject.tag)
		{
		case "Wall":
		case "Enemy":
			this.gameObject.SetActive(false);
			OnHitEffects(coll.gameObject);
			break;
		}
	}


	void OnHitEffects(GameObject other)
	{
//		if(other.tag == "Enemy")
//		{
//			//other.GetComponent<EnemyControl>().TakeDamage(damage);
//		}

		// Spawn explosion particles
		//tempObject = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;

	}

}
