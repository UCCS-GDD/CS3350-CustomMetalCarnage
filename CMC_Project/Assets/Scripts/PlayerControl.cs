using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	public int health = 1;
	private SpriteRenderer damageFlash;
	public float damageFlashDecay;

	// Use this for initialization
	void Start() 
	{
		damageFlash = GameObject.FindGameObjectWithTag("DamageFlash").GetComponent<SpriteRenderer>();
	}

	
	// Update is called once per frame
	void Update() 
	{
	
	}


	public void TakeDamage(int incomingDamage)
	{
		health -= incomingDamage;
		damageFlash.color = new Color(damageFlash.color.r, damageFlash.color.g, damageFlash.color.b, 1f);
		StopCoroutine("DamageFlash");
		StartCoroutine("DamageFlash");

		if(health <= 0)
		{
			// Player dies
			//Debug.Log("Player died.");

			GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerControl>().PlayerDeath();
		}
	}


	IEnumerator DamageFlash()
	{
		Debug.Log(damageFlash.color.a);
		while(damageFlash.color.a > 0f)
		{
			if((damageFlash.color.a-damageFlashDecay) > 0)
			{
				damageFlash.color = new Color(damageFlash.color.r, damageFlash.color.g, damageFlash.color.b, damageFlash.color.a-damageFlashDecay);
			}
			else
			{
				damageFlash.color = new Color(damageFlash.color.r, damageFlash.color.g, damageFlash.color.b, 0f);
			}
			yield return null;
		}
	}





}
