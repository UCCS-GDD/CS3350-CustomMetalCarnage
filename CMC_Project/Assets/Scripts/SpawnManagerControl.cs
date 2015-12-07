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

	public GameObject BossPrefab;

	private int randInt;

	// Use this for initialization
	void Start() 
	{
		StartCoroutine("SpawnWave");
		SpawnControl.pointsUsed = 0;
		currentWave = 0;
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}


	IEnumerator SpawnWave()
	{
		while(true)
		{	//Debug.Log(currentWave);
			if(currentWave < numWaves)
			{
				if(SpawnControl.pointsUsed < wavePoints[currentWave])
				{
					randInt = Random.Range(0, spawners.Length);
					Instantiate(spawners[randInt], new Vector3(Random.Range(leftWall.transform.position.x, rightWall.transform.position.x), Random.Range(bottomWall.transform.position.y, topWall.transform.position.y), 0f), Quaternion.identity);
				}
				else if(GameManagerControl.playerScore >= wavePoints[currentWave])
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
				//Debug.Log(currentWave);
				if(currentWave < numWaves)
				{
					yield return new WaitForSeconds(spawnerDelay[currentWave]);
				}
				else
				{
					yield return null;
				}
			}
		}
	}


	void OnWaveComplete()
	{
		//Debug.Log("Wave "+(currentWave-1)+" complete");
		GameManagerControl.ShowMessage("Wave "+(currentWave)+" Complete!");

	}


	void OnLevelComplete()
	{
		//Debug.Log("Level '"+Application.loadedLevelName+"' complete");
		StopCoroutine("SpawnWave");
		GameManagerControl.ShowMessage("The 'Opressor' has entered the Arena!");
		BossPrefab.SetActive(true);
	}


}
