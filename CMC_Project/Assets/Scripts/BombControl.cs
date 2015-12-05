using UnityEngine;
using System.Collections;

public class BombControl : MonoBehaviour 
{
	public float preWarningTime;
	public float rotationSpeed;
	public float flashSpeed;
	public GameObject explosionPrefab;

	private SpriteRenderer thisRenderer;
	private float startTime;

	// Use this for initialization
	void Start() 
	{
		thisRenderer = GetComponent<SpriteRenderer>();
		startTime = Time.time;
		StartCoroutine("FlashAndRotate");
	}
	
	// Update is called once per frame
	void Update() 
	{
		if(Time.time > (startTime+preWarningTime))
		{
			StopCoroutine("FlashAndRotate");
			thisRenderer.enabled = false;
			Instantiate(explosionPrefab, transform.position, transform.rotation);
			Destroy(this);
		}
	}

	IEnumerator FlashAndRotate()
	{
		while(true)
		{
			transform.Rotate(new Vector3(0f, 0f, rotationSpeed));
			thisRenderer.color = new Color((Mathf.Sin(Time.time*(flashSpeed+(Time.time-(startTime+preWarningTime))))+1f)/2f, 0f, 0f, 1f);
			yield return null;
		}
	}

}
