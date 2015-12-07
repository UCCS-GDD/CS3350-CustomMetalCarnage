using UnityEngine;
using System.Collections;

public class SpawnControl : MonoBehaviour 
{
	public GameObject objectPrefab;
	private GameObject lastObject;
	public bool continuous;
	public float spawnPerSecond;
	private float lastSpawnTime;

	public GameObject prespawnPrefab;
	public float prespawnTimeLead;
	private bool prespawnActive = false;

	public static int pointsUsed;

	// Use this for initialization
	void Awake ()
	{
		if(Physics2D.CircleCast(transform.position, 2, Vector2.zero))
		{
			Destroy(this.gameObject);
		}
	}

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
		if(objectPrefab.GetComponent<BasicEnemyControl>()!=null)
		{
			pointsUsed += objectPrefab.GetComponent<BasicEnemyControl>().points;
		}
		else if(objectPrefab.GetComponent<BomberControl>()!=null)
		{
			pointsUsed += objectPrefab.GetComponent<BomberControl>().points;
		}
//		else if(objectPrefab.GetComponent<>()!=null)
//		{
//			pointsUsed += objectPrefab.GetComponent<>().points;
//		}

		if(!continuous)
		{
			Destroy(this.gameObject);
		}
	}
}
