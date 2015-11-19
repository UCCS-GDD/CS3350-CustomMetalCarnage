using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DirectWeaponControl : MonoBehaviour 
{
	public int weaponType;
	public Vector2 firingTip;
	public float rateOfFire;
	public GameObject effectPrefab;
	public GameObject particlePrefab;
	public AudioClip firingSound;
	public SoundManager audioManager;
	public GameObject readyPrefab;
	public GameObject targetPrefab;
	public float speedDelay;
	
	private GameObject readyObject;
	private GameObject particleObject;
	private GameObject effectObject;
	private EffectSettings particleScript;
	private GameObject targetObject;
	private float lastFireTime = 0f;
	private RaycastHit2D hit;
	private LineRenderer line;
	private LineRenderer line2;
	
	// Use this for initialization
	void Awake () 
	{
		//audioManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		if(readyPrefab!=null)
		{
			readyObject = Instantiate(readyPrefab, transform.position, transform.rotation) as GameObject;
			readyObject.transform.parent = transform;
		}
		lastFireTime = -(1f/rateOfFire);		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if((this!=null) && (readyObject!=null) && (Time.time > ((1f/rateOfFire)+lastFireTime)))
		{
			readyObject.SetActive(true);
		}		
		
		if(Input.GetButtonUp("Fire2"))
		{
			FireCall();
		}

		if((particleObject!=null)&&(Time.time>(lastFireTime+speedDelay)))
		{
			particleScript.MoveSpeed = 30;
		}
	}
	
	
	public void FireCall()
	{
		if((this!=null) && (Time.time > ((1f/rateOfFire)+lastFireTime)))
		{
			if(readyObject!=null)
			{
				readyObject.SetActive(false);
			}
			lastFireTime = Time.time;
			
			if(firingSound!=null)
			{
				audioManager.playModulatedSound(firingSound, .3f);
			}

			//Debug.Log ("Direct Fire");
			// Show particle effect
			targetObject = Instantiate(targetPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			targetObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
			particleObject = Instantiate(particlePrefab, transform.position, Quaternion.identity) as GameObject;		
			particleScript = particleObject.GetComponent<EffectSettings>();
			particleScript.Target = targetObject;
			particleScript.MoveDistance = Vector3.Distance(targetObject.transform.position, transform.position)-0.5f;
			particleScript.MoveSpeed = 4f;

		}
	}
	
	
	public void ReloadCall()
	{
		
	}
	

	
	
}
