using UnityEngine;
using System.Collections;

public class SpawnManagerControl : MonoBehaviour 
{
	public int numWaves = 0;
	public int[] wavePoints;
	private int currentWave = 0;
	public float waveDelay = 0f;
	public float[] spawnerDelay;

	public GameObject[][] spawnerRatios = new GameObject[5][]; 
	public GameObject[] wave1Ratios = new GameObject[5];
	public GameObject[] wave2Ratios = new GameObject[5];
	public GameObject[] wave3Ratios = new GameObject[5];
	public GameObject[] wave4Ratios = new GameObject[5];
	public GameObject[] wave5Ratios = new GameObject[5];
	public GameObject topWall;
	public GameObject leftWall;
	public GameObject rightWall;
	public GameObject bottomWall;

	public GameObject BossPrefab;

	private int randInt;

	// Use this for initialization
	void Start() 
	{
		int ii = 0;
		spawnerRatios[ii] = new GameObject[5];
		for(int jj=0; jj<wave1Ratios.Length; jj++)
		{
			spawnerRatios[ii][jj] = wave1Ratios[jj];
		}
		ii++;
		spawnerRatios[ii] = new GameObject[5];
		for(int jj=0; jj<wave2Ratios.Length; jj++)
		{
			spawnerRatios[ii][jj] = wave2Ratios[jj];
		}
		ii++;
		spawnerRatios[ii] = new GameObject[5];
		for(int jj=0; jj<wave3Ratios.Length; jj++)
		{
			spawnerRatios[ii][jj] = wave3Ratios[jj];
		}
		ii++;
		spawnerRatios[ii] = new GameObject[5];
		for(int jj=0; jj<wave4Ratios.Length; jj++)
		{
			spawnerRatios[ii][jj] = wave4Ratios[jj];
		}
		ii++;
		spawnerRatios[ii] = new GameObject[5];
		for(int jj=0; jj<wave5Ratios.Length; jj++)
		{
			spawnerRatios[ii][jj] = wave5Ratios[jj];
		}




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
					randInt = Random.Range(0, spawnerRatios[currentWave].Length);
					Instantiate(spawnerRatios[currentWave][randInt], new Vector3(Random.Range(leftWall.transform.position.x, rightWall.transform.position.x), Random.Range(bottomWall.transform.position.y, topWall.transform.position.y), 0f), Quaternion.identity);
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
