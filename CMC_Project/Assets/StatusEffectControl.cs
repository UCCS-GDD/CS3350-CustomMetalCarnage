using UnityEngine;
using System.Collections;

public class StatusEffectControl : MonoBehaviour {

	private float originalSpeed;

	// Use this for initialization
	void Start () 
	{
		if(transform.parent.GetComponent<BomberControl>()!=null)
		{
			originalSpeed = transform.parent.GetComponent<BomberControl>().moveSpeed;
		}
		if(transform.parent.GetComponent<BasicEnemyControl>()!=null)
		{
			originalSpeed =  transform.parent.GetComponent<BasicEnemyControl>().moveSpeed;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.parent!=null)
		{
			transform.parent.Rotate(new Vector3(0f, 0f, 4f));
			if(transform.parent.GetComponent<BomberControl>()!=null)
			{
				transform.parent.GetComponent<BomberControl>().moveSpeed -= 1f * Time.deltaTime;
				if(transform.parent.GetComponent<BomberControl>().moveSpeed < 0f)
				{
					transform.parent.GetComponent<BomberControl>().moveSpeed = 0f;
				}
			}
			if(transform.parent.GetComponent<BasicEnemyControl>()!=null)
			{
				transform.parent.GetComponent<BasicEnemyControl>().moveSpeed -= 1f * Time.deltaTime;
				if(transform.parent.GetComponent<BasicEnemyControl>().moveSpeed < 0f)
				{
					transform.parent.GetComponent<BasicEnemyControl>().moveSpeed = 0f;
				}
			}

		}
	}


	void OnDestroy()
	{
		if(transform.parent.GetComponent<BomberControl>()!=null)
		{
			transform.parent.GetComponent<BomberControl>().moveSpeed = originalSpeed;
		}
		if(transform.parent.GetComponent<BasicEnemyControl>()!=null)
		{
			transform.parent.GetComponent<BasicEnemyControl>().moveSpeed = originalSpeed;
		}
	}


}
