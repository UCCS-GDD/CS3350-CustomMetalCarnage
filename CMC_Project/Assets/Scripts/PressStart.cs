using UnityEngine;
using System.Collections;

public class PressStart : MonoBehaviour 
{
	public float timeOn = 1f;
	public float timeOff = 1f;

	private float currentTime;
    
	private UIManager managerScript;
	private UnityEngine.UI.Text thisText;
	private string thisString;

	// Use this for initialization
	void Start () 
	{
		managerScript = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
		thisText = this.GetComponent<UnityEngine.UI.Text>();
		thisString = thisText.text;
        

		// Start checker coroutine
		StartCoroutine ("CheckForStart");

		// Start blinking coroutine
		StartCoroutine ("BlinkControl");
	}


	IEnumerator CheckForStart()
	{
		while(true)
		{
			if(Input.GetButton("Submit"))
			{
                Debug.Log("the sound should  be playing.");
                
				managerScript.showSelectionMenu();
                
			}
			yield return null;
		}
	}

	IEnumerator BlinkControl()
	{
		while(true)
		{
			// Activate text
			thisText.text = thisString;
			yield return new WaitForSeconds(timeOn);

			// Deactivate text
			thisText.text = "";
			yield return new WaitForSeconds(timeOff);
		}
	}

}
