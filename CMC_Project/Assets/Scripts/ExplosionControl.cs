using UnityEngine;
using System.Collections;

public class ExplosionControl : MonoBehaviour 
{
	public int damage;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.CompareTag("Enemy"))
		{
			//coll.GetComponent<EnemyControl>().TakeDamage(damage);
			Destroy(coll.gameObject);
		}
	}
}
