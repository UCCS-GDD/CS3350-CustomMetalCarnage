using UnityEngine;
using System.Collections;

public class BuildPanelControl : MonoBehaviour 
{
	public int numberOfCycles;
	public int cyclePosition;
	int leftCap;
	int rightCap;
	public float cycleDelay = 0.3f;
	float nextCycle = 0.0f;
	public float speed  = 30.0f;
	Vector3 position_8 = new Vector3(0.0f,20.0f,40.0f);
	Vector3 position_7 = new Vector3(-8.0f,20.0f,35.0f);
	Vector3 position_6 = new Vector3(-15.0f,20.0f,30.0f);
	Vector3 position_5 = new Vector3(-22.0f,20.0f,25.0f);
	Vector3 position_4 = new Vector3(-28.5f,16.1f,20.0f);
	Vector3 position_3 = new Vector3(-22.0f,14.7f,15.0f);
	Vector3 position_2 = new Vector3(-15.0f,13.2f,10.0f);
	Vector3 position_1 = new Vector3(-8.0f,11.8f,5.0f);
	Vector3 position0 = new Vector3(0.0f,9.0f,0.0f);
	Vector3 position1 = new Vector3(8.0f,11.8f,5.0f);
	Vector3 position2 = new Vector3(15.0f,13.2f,10.0f);
	Vector3 position3 = new Vector3(22.0f,14.7f,15.0f);
	Vector3 position4 = new Vector3(28.5f,16.1f,20.0f);
	Vector3 position5 = new Vector3(22.0f,20.0f,25.0f);
	Vector3 position6 = new Vector3(15.0f,20.0f,30.0f);
	Vector3 position7 = new Vector3(8.0f,20.0f,35.0f);
	Vector3 position8 = new Vector3(0.0f,20.0f,40.0f);

	//private GameObject myCamera;

	// Use this for initialization
	void Start () 
	{ 
		//myCamera = GameObject.FindGameObjectWithTag("MainCamera");

		leftCap = cyclePosition - (numberOfCycles-1);
		rightCap = cyclePosition;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		float step = speed * Time.deltaTime;
				
			switch (cyclePosition)
			{
			case 0:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position0, step);
   				break;
			case 1:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position1, step);
   				break;
			case 2:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position2, step);
   				break;
			case 3:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position3, step);
   				break;
			case 4:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position4, step);
   				break;
			case 5:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position5, step);
   				break;
			case 6:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position6, step);
   				break;
			case 7:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position7, step);
   				break;
			case 8:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position8, step);
   				break;
			case -1:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position_1, step);
   				break;
			case -2:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position_2, step);
   				break;
			case -3:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position_3, step);
   				break;
			case -4:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position_4, step);
   				break;
			case -5:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position_5, step);
   				break;
			case -6:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position_6, step);
   				break;
			case -7:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position_7, step);
   				break;
			case -8:
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, position_8, step);
   				break;
			}
		
		
		if(Input.GetButtonDown("Horizontal"))
		{
			nextCycle = Time.time + cycleDelay;
			if(Input.GetAxis("Horizontal") < 0)
			{
				// Move to the left
				cyclePosition++;
				if(cyclePosition > rightCap)
				{
					cyclePosition = rightCap;
				}		
			}
			else
			{
				// Move to the right
				cyclePosition--;
				if(cyclePosition < leftCap)
				{
					cyclePosition = leftCap;
				}
			}
		}
			
		
		
		if(Input.GetButton("Horizontal") && Time.time > nextCycle)
		{
			nextCycle = Time.time + cycleDelay; 
			
			// Move towards new position
			if(Input.GetAxis("Horizontal") < 0)
			{
				// Move to the left
				cyclePosition++;
				if(cyclePosition > rightCap)
				{
					cyclePosition = rightCap;
				}		
			}
			else
			{
				// Move to the right
				cyclePosition--;
				if(cyclePosition < leftCap)
				{
					cyclePosition = leftCap;
				}
			}
			
		}
	}
}
