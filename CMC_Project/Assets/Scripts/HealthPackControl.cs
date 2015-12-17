using UnityEngine;
using System.Collections;

public class HealthPackControl : MonoBehaviour 
{
	public int amountHealed;
	public GameObject usePrefab;

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
		switch(coll.gameObject.tag)
		{
		case "Player":
			Instantiate(usePrefab, transform.position, Quaternion.identity);
			coll.GetComponent<PlayerControl>().TakeDamage(-amountHealed);
			Destroy(this.gameObject);
			break;
		}
	}

}
