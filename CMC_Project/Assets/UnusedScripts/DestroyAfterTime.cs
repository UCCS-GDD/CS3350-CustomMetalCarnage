using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour 
{
	public float timeUntilDestroyed;
	private float startTime;

	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if((Time.time-startTime)>timeUntilDestroyed)
		{
			GameObject.Destroy(this.gameObject);
		}
	}

}
