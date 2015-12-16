using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretControl : MonoBehaviour 
{
	[System.Serializable]
	public class HardPoint
	{
		// Clockwise positive, 0-359, 0 = straight up
		public float Angle;
		public Vector2 Location;
		
//		public Vector2 Location
//		{
//			get
//			{
//				return location;
//			}
//		}
//		
//		public float Angle
//		{
//			get
//			{
//				return angle;
//			}
//		}
		
		public HardPoint(Vector2 inputLocation, float inputAngle)
		{
			Location = inputLocation;
			Angle = inputAngle % 360f;
		}
	}


	public List<HardPoint> hardPoints = new List<HardPoint>();
	public float rotationSpeed = 1;

	// Variables for MouseLook
	private Vector3 targetPosition;
	private Vector3 directionToMouse;
	private float angleToMouse;
	private Quaternion targetRotation;

	// Delegates for firing/reloading weapons and sounds
	delegate void FireOrReload();
	FireOrReload primaryFire;
	FireOrReload secondaryFire;
	FireOrReload secondaryReload;
	delegate void FireSound();
	FireSound primaryFireSound;
	FireSound secondaryFireSound;

	private WeaponControl tempScript;

	public bool canFire = true;

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
	

	public void DelegateFireAndReload() 
	{		
		// Clear delegates
		primaryFire = null;
		secondaryFire = null;
		secondaryReload = null;
		primaryFireSound = null;
		secondaryFireSound = null;

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

					int ii = 0;
					bool matched = false;
					while(ii<child.GetSiblingIndex())
					{
						if(transform.GetChild(ii).name == child.name)
						{
							matched = true;
						}
						ii++;
					}
					if(!matched)
					{
						primaryFireSound += tempScript.FiringSoundCall;
					}
				}
				// Check if child is a Secondary Weapon
				else if(tempScript.weaponType==2)
				{
					// Store FireCall() in a delegate
					secondaryFire += tempScript.FireCall;

					int ii = 0;
					bool matched = false;
					while(ii<child.GetSiblingIndex())
					{
						if(transform.GetChild(ii).name == child.name)
						{
							matched = true;
						}
						ii++;
					}
					if(!matched)
					{
						secondaryFireSound += tempScript.FiringSoundCall;
					}

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
			if(Input.GetButton("Fire1") && canFire)
			{
				if(primaryFire != null)
				{
					if(primaryFireSound != null)
					{
						primaryFireSound();
					}
					primaryFire();
				}
			}

			// If secondary fire button is pressed
			if(Input.GetButton("Fire2") && canFire)
			{

				if(secondaryFire != null)
				{
					if(secondaryFireSound != null)
					{
						secondaryFireSound();
					}
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
