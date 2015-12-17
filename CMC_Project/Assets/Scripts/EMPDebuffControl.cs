using UnityEngine;
using System.Collections;

public class EMPDebuffControl : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		if(transform.parent.GetComponent<BasicEnemyControl>()!=null)
		{
			transform.parent.GetComponent<BasicEnemyControl>().canFire = false;
		}
		else if(transform.parent.GetComponent<ArtilleryControl>()!=null)
		{
			transform.parent.GetComponent<ArtilleryControl>().canFire = false;
		}
		//		else if(transform.parent.GetComponent<BossControl>()!=null)
		//		{
		//			transform.parent.GetComponent<BossControl>().canFire = false;
		//		}
	}
	
	void OnDestroy()
	{
		if(transform.parent.GetComponent<BasicEnemyControl>()!=null)
		{
			transform.parent.GetComponent<BasicEnemyControl>().canFire = true;
		}
		else if(transform.parent.GetComponent<ArtilleryControl>()!=null)
		{
			transform.parent.GetComponent<ArtilleryControl>().canFire = true;
		}
		//		else if(transform.parent.GetComponent<BossControl>()!=null)
		//		{
		//			transform.parent.GetComponent<BossControl>().canFire = true;
		//		}
	}

}
