using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretControl : MonoBehaviour 
{
	public List<HardPoint> hardPoints = new List<HardPoint>();
	public float rotationSpeed = 1;

	// Variables for MouseLook
	private Vector3 targetPosition;
	private Vector3 directionToMouse;
	private float angleToMouse;
	private Quaternion targetRotation;

	// Delegates for firing/reloading weapons
	delegate void FireOrReload();
	FireOrReload primaryFire;
	FireOrReload secondaryFire;
	FireOrReload secondaryReload;

	private WeaponControl tempScript;

	// Use this for initialization
	void Start () 
	{
		// Use this format to add new HardPoints to the List, with the X/Y positions of the HardPoint and its angle in degrees (0 = up/forward)
		// hardPoints.Add(new HardPoint(new Vector2("xPos", "yPos"), "angle"));

		// Store child weapon FireCall() and ReloadCall() functions in delegates
		DelegateFireAndReload();

		StartCoroutine("MouseLook");
		StartCoroutine("FireAndReloadListener");
	}
	

	void DelegateFireAndReload() 
	{		
		// Loop through children
		foreach(Transform child in transform)
		{
			// Get child's WeaponControl script
			tempScript = child.GetComponent<WeaponControl>();

			// Check if child has a WeaponControl script
			if(tempScript!=null)
			{
				// Check if child is a Primary Weapon
				if(tempScript.weaponType==1)
				{
					// Store FireCall() in a delegate
					primaryFire += tempScript.FireCall;
				}
				// Check if child is a Secondary Weapon
				else if(tempScript.weaponType==2)
				{
					// Store FireCall() in a delegate
					secondaryFire += tempScript.FireCall;

					// Store ReloadCall() in a delegate
					secondaryReload += tempScript.ReloadCall;
				}
			}
		}		
	}


	IEnumerator MouseLook()
	{
		while(true)
		{
			targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
			directionToMouse = targetPosition - new Vector3(transform.position.x, transform.position.y, 0f);
			angleToMouse = Mathf.Atan2(directionToMouse.x, directionToMouse.y) * Mathf.Rad2Deg;
			targetRotation = Quaternion.Euler(new Vector3(0f, 0f, -angleToMouse));
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
			yield return null;
		}
	}


	IEnumerator FireAndReloadListener()
	{
		while(true)
		{
			// If primary fire button is pressed
			if(Input.GetButton("Fire1"))
			{
				if(primaryFire != null)
				{
					primaryFire();
				}
			}

			// If secondary fire button is pressed
			if(Input.GetButton("Fire2"))
			{
				if(secondaryFire != null)
				{
					secondaryFire();
				}
			}

			// If secondary reload button is pressed
			if(Input.GetButton("Reload2"))
			{
				if(secondaryReload != null)
				{
					secondaryReload();
				}
			}

			yield return null;
		}				
	}


}
