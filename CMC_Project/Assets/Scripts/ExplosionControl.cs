using UnityEngine;
using System.Collections;

public class ExplosionControl : MonoBehaviour 
{
	public int team; // 0 for enemy projectile, 1 for player
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
		if((team==1) && coll.CompareTag("Enemy"))
		{
			if(coll.GetComponent<BomberControl>()!=null)
			{
				coll.GetComponent<BomberControl>().TakeDamage(damage);
			}
			else if(coll.GetComponent<BasicEnemyControl>()!=null)
			{
				coll.GetComponent<BasicEnemyControl>().TakeDamage(damage);
			}
			else if(coll.GetComponent<ArtilleryControl>()!=null)
			{
				coll.GetComponent<ArtilleryControl>().TakeDamage(damage);
			}
			else if(coll.GetComponent<BossControl>()!=null)
			{
				coll.GetComponent<BossControl>().TakeDamage(damage);
			}
		}
		if((team==0) && (coll.CompareTag("Player")))
		{
			coll.GetComponent<PlayerControl>().TakeDamage(damage);
		}
	}
}
