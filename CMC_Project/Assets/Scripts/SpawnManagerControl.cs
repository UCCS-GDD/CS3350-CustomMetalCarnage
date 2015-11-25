using UnityEngine;
using System.Collections;

public class SpawnManagerControl : MonoBehaviour 
{
	public int numWaves = 0;
	public int[] wavePoints;
	private int currentWave = 0;
	public float waveDelay = 0f;
	public float[] spawnerDelay;

	public GameObject[] spawners;
	public float[] spawnerRatios;

	public GameObject topWall;
	public GameObject leftWall;
	public GameObject rightWall;
	public GameObject bottomWall;

	private int randInt;

	// Use this for initialization
	void Start() 
	{
		StartCoroutine("SpawnWave");
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}


	IEnumerator SpawnWave()
	{
		while(true)
		{	//Debug.Log(SpawnControl.pointsUsed);
			if(SpawnControl.pointsUsed < wavePoints[currentWave])
			{
				randInt = Random.Range(0, spawners.Length);
				Instantiate(spawners[randInt], new Vector3(Random.Range(leftWall.transform.position.x, rightWall.transform.position.x), Random.Range(bottomWall.transform.position.y, topWall.transform.position.y), 0f), Quaternion.identity);
			}
			else
			{
				currentWave++;
				if(currentWave >= numWaves)
				{
					OnLevelComplete();
					StopCoroutine("SpawnWave");
				}
				else
				{
					OnWaveComplete();
					yield return new WaitForSeconds(waveDelay);
				}
			}
			yield return new WaitForSeconds(spawnerDelay[currentWave]);
		}
	}


	void OnWaveComplete()
	{
		Debug.Log("Wave "+(currentWave-1)+" complete");
	}


	void OnLevelComplete()
	{
		Debug.Log("Level '"+Application.loadedLevelName+"' complete");
	}


}
