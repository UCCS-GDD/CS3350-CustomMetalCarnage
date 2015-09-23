//using UnityEngine;
//using System.Collections;
//
//public class WeaponTemplateControl : MonoBehaviour 
//{
////	private NetworkControl networkScript;
//	private PlayerControl playerScript;
//
//	public bool damageTypeKinetic;
//	public bool damageTypeEnergy;
//	public bool projectileTypeRaycast;
//	public bool tracer;
//	public bool projectileTypeCollision;
//	public GameObject collisionProjectilePrefab;
//	public float collisionProjectileSpeed;
//	public bool tracking;
//	public float trackingStrength;
//	private GameObject trackingTarget;
//
//	public float rateOfFire;
//	public float baseDamage;
//	private float damageValue;
//	public float attackRatio;
//	public float accuracy;
//	public float range;
//	public float energyUsage;
//	public int ammoPerMagazine;
//	private int currentAmmoInMag;
//	public int totalAmmo;
//	public float reloadTime;
//	private float currentTimeInReload;
//
//	public Texture2D bulletHole;
//	public Texture muzzleFlash;
//	private Vector3 startingPosition;
//	public Vector3 recoilPosition;
//	public float recoilSpeed;
//
//	public bool canFire = true;
//	private float lastFireTime = 0;
//	private float timeBetweenFire;
//	private bool reloading = false;
//	private Quaternion targetLocalRotation;
//
//	private GameObject mainCamera;
//	private Camera mainCameraComponent;
//	private Vector3 lookTargetPoint;
//	private float workingFloat;
//	private Vector2 aimOffset;
//	private Vector3 rayDirection;
//
//	private RaycastHit hit;
//	public Vector3 firingTip;
//
//	private LineRenderer tracerRenderer;
//	public float tracerLengthPercentage;
//	public float tracerDuration;
//	private float tracerStartTime;
//	private float rayRandX;
//	private float rayRandY;
//
//	private MouseAim playerAimScript;
//
//	private UnityEngine.UI.Text HUDAmmoText;
//	public GameObject bulletIconPrefab;
//	private GameObject[] bulletIconObjects;
//	private GameObject bulletIconParent;
//	public float bulletIconsTotalWidth;
//	public float bulletIconsTotalheight;
//	private int currentIndex;
//
//	private float twoDimDistance;
//	private float threeDimDistance;
//	private float heightAngle;
//
////	private VerticalAimControl vAimScript;
//
//	// Use this for initialization
//	void Start () 
//	{
//		//vAimScript = transform.parent.FindChild ("VerticalAim").GetComponent<VerticalAimControl> ();
//		timeBetweenFire = 1 / rateOfFire;
//		currentAmmoInMag = ammoPerMagazine;
//		currentTimeInReload = 0;
//		startingPosition = transform.localPosition;
//		playerScript = transform.parent.gameObject.GetComponent<PlayerControl> ();
//		playerAimScript = transform.parent.gameObject.GetComponent<MouseAim> ();
//		//networkScript = GameObject.FindGameObjectWithTag ("NetworkController").GetComponent<NetworkControl>();
//
//		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
//
//		if(tracer)
//		{
//			if(tracerLengthPercentage < 0)
//			{
//				tracerLengthPercentage = 0;
//			}
//			else if(tracerLengthPercentage > 100)
//			{
//				tracerLengthPercentage = 100;
//			}
//			Debug.Log ("Setting Tracer");
//			tracerRenderer = transform.FindChild("Tracer").gameObject.GetComponent<LineRenderer>();
//			tracerRenderer.useWorldSpace = false;
//			tracerRenderer.SetPosition(0, firingTip);
//		}
//
//
//		// Move PrimaryWeaponUI to UIObject
//		GameObject UIObject = GameObject.FindGameObjectWithTag ("UIObject");
//		UIObject.transform.position = Vector3.zero;//mainCamera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(mainCamera.GetComponent<Camera> ().pixelWidth/2, mainCamera.GetComponent<Camera> ().pixelHeight/2, (Mathf.Abs(mainCamera.transform.localPosition.z)+range)));
//
//
//		//Debug.Log (Mathf.Sin(Mathf.Deg2Rad*(1f/accuracy)) * range);
//		transform.FindChild("PrimaryWeaponUI").parent = UIObject.transform;
//		RectTransform crosshairRect = UIObject.transform.FindChild ("PrimaryWeaponUI/Canvas/Crosshair").GetComponent<RectTransform> ();
//
//		// Change Crosshair size based on Accuracy
//		crosshairRect.sizeDelta = new Vector2(Mathf.Sin(Mathf.Deg2Rad*(1f/accuracy)) * Mathf.Rad2Deg  * 22f, Mathf.Sin(Mathf.Deg2Rad*(1f/accuracy)) * Mathf.Rad2Deg * 22f);
//		//Debug.Log (UIObject.transform.FindChild ("PrimaryWeaponUI/Canvas/Crosshair").GetComponent<RectTransform> ().sizeDelta);
//
//		UIObject.transform.FindChild ("PrimaryWeaponUI").localPosition = Vector3.zero;
//		UIObject.transform.FindChild ("PrimaryWeaponUI/Canvas").localPosition = Vector3.zero;
//		aimOffset.x = 0;//GameObject.FindGameObjectWithTag ("PrimaryWeaponUI").transform.FindChild ("Canvas/Crosshair").gameObject.GetComponent<RectTransform>().anchoredPosition.x;
//		aimOffset.y = 0;//GameObject.FindGameObjectWithTag ("PrimaryWeaponUI").transform.FindChild ("Canvas/Crosshair").gameObject.GetComponent<RectTransform>().anchoredPosition.y;
//		//Debug.Log (aimOffset);
//
//		// HUD Initialization
//		Debug.Log ("Trying to access weapon HUD");
//		HUDAmmoText = GameObject.FindGameObjectWithTag ("UIObject").transform.FindChild("HUD/Canvas/PrimaryWeapon/Text").GetComponent<UnityEngine.UI.Text>();
//		HUDAmmoText.text = "" + totalAmmo;
//		bulletIconParent = GameObject.FindGameObjectWithTag ("UIObject").transform.FindChild ("HUD/Canvas/PrimaryWeapon/Bullets").gameObject;
//		float bulletWidth = ((bulletIconsTotalWidth / ammoPerMagazine) - .5f);
//		bulletIconObjects = new GameObject[ammoPerMagazine];
//		currentIndex = ammoPerMagazine - 1;
//		if(bulletWidth < 3)
//		{
//			// 2 Layers of bullet icons
//			bulletWidth *=2;
//			for(int ii = 0; ii < ammoPerMagazine; ii++)
//			{
//				bulletIconObjects[ii] = Instantiate(bulletIconPrefab, Vector3.zero, Quaternion.identity) as GameObject;
//				bulletIconObjects[ii].transform.SetParent(bulletIconParent.transform, false);
//				bulletIconObjects[ii].GetComponent<RectTransform>().sizeDelta = new Vector2(bulletWidth, (bulletIconsTotalheight/2));
//				if(ii < (ammoPerMagazine/2))
//				{
//					bulletIconObjects[ii].GetComponent<RectTransform>().anchoredPosition = new Vector2((((bulletWidth+0.5f)*ii)+45), (bulletIconsTotalheight/4));
//				}
//				else
//				{
//					bulletIconObjects[ii].GetComponent<RectTransform>().anchoredPosition = new Vector2(((bulletWidth+0.5f)*(ii-Mathf.Floor(ammoPerMagazine/2)))+45, (-bulletIconsTotalheight/4));
//				}
//				bulletIconObjects[ii].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0,0,0);
//				bulletIconObjects[ii].GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
//			}
//		}
//		else
//		{
//			for(int ii = 0; ii < ammoPerMagazine; ii++)
//			{
//				bulletIconObjects[ii] = Instantiate(bulletIconPrefab, Vector3.zero, Quaternion.identity) as GameObject;
//				bulletIconObjects[ii].transform.SetParent(bulletIconParent.transform, false);
//				bulletIconObjects[ii].GetComponent<RectTransform>().sizeDelta = new Vector2(bulletWidth, bulletIconsTotalheight);
//				bulletIconObjects[ii].GetComponent<RectTransform>().anchoredPosition = new Vector2(((bulletWidth+0.5f)*ii)+45, 0);
//				bulletIconObjects[ii].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0,0,0);
//				bulletIconObjects[ii].GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
//			}
//		}
//
//
//
//	}
//	
//	// Update is called once per frame
//	void Update () 
//	{
//		if((Time.time - lastFireTime) > timeBetweenFire)
//		{
//			if(ammoPerMagazine > 0)
//			{
//				// Check if there's ammo in the magazine
//				if(currentAmmoInMag > 0)
//				{
//					canFire = true;
//				}
//				else if(!reloading)
//				{
//					Reload();
//				}
//			}
//			else if(energyUsage != 0)
//			{
//				// Check if current energy is more than energyUsage
//				if(playerScript.currentEnergy > energyUsage)
//				{
//					canFire = true;
//				}
//			}
//			else
//			{
//				canFire = true;
//			}
//		}
//
//		if(Input.GetButton("Fire1")&&canFire)
//		{
//			if(reloading)
//			{
//
//
//				// Cancel reload
//				currentTimeInReload = -1;
//			}
//
//			// Fire weapon
//			Fire();
//
//
//		}
//
//		if(Input.GetButton("Reload")&&!reloading)
//		{
//			Reload();
//		}
//
//
//		if(reloading)
//		{
//			if(currentTimeInReload < 0)
//			{
//				reloading = false;
//			}
//			else
//			{
//				currentTimeInReload += Time.deltaTime;
//			}
//			if(currentTimeInReload > reloadTime)
//			{
//				reloading = false;
//				currentTimeInReload = -1;
//				// Rotate weapon back to normal
//				//targetLocalRotation.eulerAngles = Vector3.zero;
//
//				if(totalAmmo >= ammoPerMagazine)
//				{
//					currentAmmoInMag = ammoPerMagazine;
//					for(int ii=0; ii<ammoPerMagazine; ii++)
//					{
//						bulletIconObjects[ii].SetActive(true);
//					}
//				}
//				else
//				{
//					currentAmmoInMag = totalAmmo;
//					for(int ii=0; ii<currentAmmoInMag; ii++)
//					{
//						bulletIconObjects[ii].SetActive(true);
//					}
//				}
//				currentIndex = ammoPerMagazine - 1;
//			}
//		}
//
//
//
//		transform.localPosition = Vector3.MoveTowards (transform.localPosition, startingPosition, recoilSpeed*Time.deltaTime);
//
//		if(!reloading)
//		{
//			transform.LookAt (playerAimScript.targetPosition);
//		}
//		else
//		{
//			transform.localRotation = Quaternion.RotateTowards (transform.localRotation, targetLocalRotation, (200*(1/reloadTime)*Time.deltaTime));
//		}
//
//
//		if(tracer&&tracerRenderer.enabled)
//		{
//			if((Time.time-tracerStartTime)>tracerDuration)
//			{
//				tracerRenderer.enabled = false;
//			}
//		}
//
//	}
//
//
//	private void Reload()
//	{
//		if(totalAmmo > 0)
//		{
//			//Debug.Log ("reloading");
//			reloading = true;
//			currentTimeInReload = 0;
//			targetLocalRotation.eulerAngles = new Vector3 (90, 0, 0);
//		}
//		else
//		{
//			// Flash no ammo warning
//		}
//	}
//
//	private void Fire()
//	{
//		//Debug.Log("Fire");
//		lastFireTime = Time.time;
//		canFire = false;
//		transform.position = transform.TransformPoint(recoilPosition);
//		if(ammoPerMagazine > 0)
//		{
//			if(currentAmmoInMag > 0)
//			{
//				currentAmmoInMag -= 1;
//				totalAmmo -= 1;
//				HUDAmmoText.text = "" + totalAmmo;
//				bulletIconObjects[currentIndex].SetActive(false);
//				currentIndex--;
//			}
//			else if(!reloading)
//			{
//				Reload();
//			}
//		}
//		playerScript.currentEnergy -= energyUsage;
//
//		// Rotate weapon to fire position
//		targetLocalRotation = Quaternion.identity;
//		//twoDimDistance = Vector2.Distance(new Vector2(transform.parent.position.x, transform.parent.position.z), new Vector2(vAimScript.targetPosition.x, vAimScript.targetPosition.z));
//		Debug.Log("2D dist: "+twoDimDistance);
//		//threeDimDistance = Vector3.Distance(transform.parent.position, vAimScript.targetPosition);
//		Debug.Log("3D dist: "+threeDimDistance);
//		heightAngle = Mathf.Acos(twoDimDistance/threeDimDistance);
//		Debug.Log ((Mathf.Tan (heightAngle) * range));
//		rayDirection = new Vector3(transform.parent.GetComponent<MouseAim>().targetPosition.x, ((Mathf.Tan(heightAngle)*range)+transform.parent.position.y), transform.parent.GetComponent<MouseAim>().targetPosition.z) - (transform.parent.position);
//		rayDirection = Quaternion.Euler ((Random.Range(1.0f-(1/accuracy), -1.0f+(1/accuracy))), 0, 0) * rayDirection;
//		Debug.Log ("RayDir "+ rayDirection.x+" "+rayDirection.y+" "+rayDirection.z);
//		if(Physics.Raycast (transform.parent.position, rayDirection, out hit, range))
//		{
//
//			//Debug.DrawRay (transform.TransformPoint(firingTip), transform.forward, Color.white, 2);
//			//Debug.Log (hit.collider.gameObject.name);
//
//			if(tracer)
//			{
//				tracerRenderer.SetPosition(1, tracerRenderer.transform.InverseTransformPoint(hit.point));
//			}
//
//
//			// Calculate damageValue
//			Debug.Log("attackMod: "+playerScript.attackModifier );
//			damageValue = (baseDamage + (attackRatio * playerScript.attack)) * (1 + playerScript.attackModifier);
//
////			switch(hit.collider.gameObject.tag)
////			{
////			case "Player1":
////
////				networkScript.DealDamage("Player1", damageValue, damageTypeEnergy, damageTypeKinetic);
////				break;
////			case "Player2":
////				
////				networkScript.DealDamage("Player2", damageValue, damageTypeEnergy, damageTypeKinetic);
////				break;
////			case "Player3":
////				
////				networkScript.DealDamage("Player3", damageValue, damageTypeEnergy, damageTypeKinetic);
////				break;
////			case "Player4":
////				
////				networkScript.DealDamage("Player4", damageValue, damageTypeEnergy, damageTypeKinetic);
////				break;
////
////			}
//
//
//		}
//		else
//		{
//			if(tracer)
//			{
//				tracerRenderer.SetPosition(1, new Vector3((range*Mathf.Sin(Mathf.Deg2Rad*Random.Range(1.0f-(1/accuracy), -1.0f+(1/accuracy)))), 0, range));
//			}
//		}
//
//		// Tracer code
//		if(tracer)
//		{
//			tracerStartTime = Time.time;
//			tracerRenderer.enabled = true;
//		}
//	}
//
//
//
//}
