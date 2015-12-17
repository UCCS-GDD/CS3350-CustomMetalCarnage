using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EMPCircleControl : MonoBehaviour 
{
	public GameObject debuffPrefab;
	private GameObject debuffObject;

	private List<Transform> affectedEnemies = new List<Transform>();

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
		case "Enemy":
			if(coll.transform.FindChild("EMPDebuff(Clone)")==null)
			{
				debuffObject = Instantiate(debuffPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				debuffObject.transform.parent = coll.transform;
				debuffObject.transform.localPosition = Vector3.zero;
				affectedEnemies.Add(coll.transform);
			}
			break;
		}
	}


	void OnDestroy()
	{
		foreach(Transform obj in affectedEnemies)
		{
			if(obj!=null)
			{
				Destroy(obj.Find("EMPDebuff(Clone)").gameObject);
			}
		}
	}

}
