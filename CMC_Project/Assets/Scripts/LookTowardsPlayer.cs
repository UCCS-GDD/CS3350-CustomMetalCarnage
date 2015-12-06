using UnityEngine;
using System.Collections;

public class LookTowardsPlayer : MonoBehaviour 
{
	public float rotationSpeed = 1;

	private Vector3 targetPosition;
	private Vector3 directionToMouse;
	private float angleToMouse;
	private Quaternion targetRotation;
	private GameObject playerObject;

	// Use this for initialization
	void Start () 
	{
		playerObject = GameObject.FindGameObjectWithTag("Player");
		StartCoroutine("Look");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


	IEnumerator Look()
	{
		while(true)
		{
			targetPosition = playerObject.transform.position;
			directionToMouse = targetPosition - new Vector3(transform.position.x, transform.position.y, 0f);
			angleToMouse = Mathf.Atan2(directionToMouse.x, directionToMouse.y) * Mathf.Rad2Deg;
			targetRotation = Quaternion.Euler(new Vector3(0f, 0f, -angleToMouse));
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
			yield return null;
		}
	}
}
