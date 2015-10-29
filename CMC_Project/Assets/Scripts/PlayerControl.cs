using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	public int health = 1;


	// Use this for initialization
	void Start() 
	{

	}

	
	// Update is called once per frame
	void Update() 
	{
	
	}


	public void TakeDamage(int incomingDamage)
	{
		health -= incomingDamage;
		DamageFlash();

		if(health <= 0)
		{
			// Player dies
			Debug.Log("Player died.");

			GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerControl>().PlayerDeath();
		}
	}


	void DamageFlash()
	{
		// Add a transparent damage image over camera, decrease it's transparency when hit
	}





}
