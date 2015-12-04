using UnityEngine;
using System.Collections;

public class BananaPeelControl : MonoBehaviour {

	public GameObject statusEffectPrefab;
	private GameObject statusObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.CompareTag("Enemy"))
		{
			statusObject = Instantiate(statusEffectPrefab, transform.position, transform.rotation) as GameObject;
			statusObject.transform.parent = coll.transform;
		}
	}


}
