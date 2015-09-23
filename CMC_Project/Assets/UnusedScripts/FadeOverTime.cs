using UnityEngine;
using System.Collections;

public class FadeOverTime : MonoBehaviour 
{
	public float timeUntilDestroyed;
	public float fadeAmount;
	private float startTime;
	private TrailRenderer trail;


	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
		trail = this.GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		trail.time -= fadeAmount;

		if((Time.time-startTime)>timeUntilDestroyed)
		{
			GameObject.Destroy(this.gameObject);
		}
	}
	
}
