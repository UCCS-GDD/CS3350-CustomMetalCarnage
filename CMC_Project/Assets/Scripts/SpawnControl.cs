using UnityEngine;
using System.Collections;

public class SpawnControl : MonoBehaviour 
{
	public GameObject objectPrefab;
	private GameObject lastObject;
	public float spawnPerSecond;
	private float lastSpawnTime;

	public GameObject prespawnPrefab;
	public float prespawnTimeLead;
	private bool prespawnActive = false;

	// Use this for initialization
	void Start () 
	{
		lastSpawnTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if((!prespawnActive) && (Time.time > ((lastSpawnTime+1f/spawnPerSecond)-prespawnTimeLead)))
		{
			lastObject = Instantiate(prespawnPrefab, transform.position, transform.rotation) as GameObject;
			prespawnActive = true;
		}

		if((spawnPerSecond > 0f) && (Time.time > (lastSpawnTime+1f/spawnPerSecond)))
		{
			SpawnObject();
		}

	}

	void SpawnObject()
	{
		if(prespawnActive)
		{
			Destroy(lastObject);
			prespawnActive = false;
		}
		lastObject = Instantiate(objectPrefab, transform.position, transform.rotation) as GameObject;
		lastSpawnTime = Time.time;
	}
}
