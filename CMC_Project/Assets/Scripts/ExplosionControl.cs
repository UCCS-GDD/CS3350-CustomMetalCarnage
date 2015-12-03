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
			if(coll.GetComponent<BomberControl>()!=null)
			{
				coll.GetComponent<BomberControl>().TakeDamage(damage);
			}
			else if(coll.GetComponent<BasicEnemyControl>()!=null)
			{
				coll.GetComponent<BasicEnemyControl>().TakeDamage(damage);
			}
		}
	}
}
