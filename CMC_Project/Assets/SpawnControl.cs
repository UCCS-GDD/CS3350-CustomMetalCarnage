using UnityEngine;
using System.Collections;

public class SpawnControl : MonoBehaviour 
{
	public GameObject objectPrefab;
	private GameObject lastObject;
	public float spawnPerSecond;
	private float lastSpawnTime;

	// Use this for initialization
	void Start () 
	{
		lastSpawnTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if((spawnPerSecond > 0f) && (Time.time > (lastSpawnTime+1f/spawnPerSecond)))
		{
			SpawnObject();
		}
	}

	void SpawnObject()
	{
		lastObject = Instantiate(objectPrefab, transform.position, transform.rotation) as GameObject;
		lastSpawnTime = Time.time;
	}
}
