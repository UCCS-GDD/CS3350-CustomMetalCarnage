using UnityEngine;
using System.Collections;

public class ArtilleryBombControl : MonoBehaviour 
{
	public GameObject bombPrefab;
	public int numberOfBombs;
	public float delayBetweenBombs;
	public float spread;

	private Vector2 targetDirection;
	private float targetDistance;

	// Use this for initialization
	void Start() 
	{
		targetDirection = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
		targetDistance = targetDirection.magnitude;

		StartCoroutine("SpawnBombs");

	}
	
	IEnumerator SpawnBombs()
	{
		// Place bombs near Player
		for(int ii=0; ii<numberOfBombs; ii++)
		{
			//Debug.Log(((1f-(spread/2))+(spread/numberOfBombs*ii)));
			Instantiate(bombPrefab, (((1f-(spread/2))+(spread/numberOfBombs*ii))*targetDirection.normalized*targetDistance)+(Vector2)transform.position, transform.rotation);
			yield return new WaitForSeconds(delayBetweenBombs);
		}
	}
}
