using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RayWeaponControl : MonoBehaviour 
{
	public int weaponType;
	public int damage;
	public int team; // 0 for enemy projectile, 1 for player
	public Vector2 firingTip;
	public float rateOfFire;
	public float duration;
	public GameObject particlePrefab;
	public AudioClip firingSound;
	//public SoundManager audioManager;
	public GameObject readyPrefab;
	public GameObject targetPrefab;
	public GameObject explosionPrefab;
	
	private GameObject readyObject;
	private GameObject particleObject;
	private EffectSettings particleScript;
	private GameObject targetObject;
	private float lastFireTime = 0f;
	private RaycastHit2D hit;
	private LineRenderer line;
	private LineRenderer line2;

	public GameObject damageNumber;
	
	// Use this for initialization
	void Awake () 
	{
		//audioManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		if(readyPrefab!=null)
		{
			readyObject = Instantiate(readyPrefab, transform.position, transform.rotation) as GameObject;
			readyObject.transform.parent = transform;
			readyObject.transform.localPosition = new Vector3(firingTip.x, firingTip.y, 0f);
		}

		targetObject = Instantiate(targetPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		particleObject = Instantiate(particlePrefab, transform.position, Quaternion.identity) as GameObject;

		particleScript = particleObject.GetComponent<EffectSettings>();
		particleScript.Target = targetObject;
		particleObject.transform.parent = transform;
		particleObject.transform.localRotation = Quaternion.identity;
		particleObject.transform.localPosition = new Vector3(firingTip.x, firingTip.y, 0f);
		line = particleObject.transform.FindChild("Trail").GetComponent<LineRenderer>();
		line2 = particleObject.transform.FindChild("GlowTrail").GetComponent<LineRenderer>();
		particleObject.SetActive(false);

		lastFireTime = -(1f/rateOfFire);

	}
	
	// Update is called once per frame
	void Update () 
	{


		if((this!=null) && (readyObject!=null) && (Time.time > ((1f/rateOfFire)+lastFireTime)))
		{
			readyObject.SetActive(true);
		}
		if(Time.time > (lastFireTime+duration))
		{
			particleObject.SetActive(false);
		}

		if(particleObject.activeSelf && (line!=null) && (line2!=null))
		{
			line.SetPosition(0, particleObject.transform.position);
			line2.SetPosition(0, particleObject.transform.position);
		}
		

		if(Input.GetButton("Fire1"))
		{
			FireCall();
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
				SoundManager.singleton.playModulatedSound(firingSound, .5f);
			}

			hit = Physics2D.Raycast(new Vector2(transform.position.x+firingTip.x, transform.position.y+firingTip.y), new Vector2((-Mathf.Sin(transform.rotation.eulerAngles.z*Mathf.Deg2Rad)), (Mathf.Cos(transform.rotation.eulerAngles.z*Mathf.Deg2Rad))), Mathf.Infinity, -1, 0f, 0f);

			// Show particle effect
			targetObject.transform.position = new Vector3(hit.point.x, hit.point.y, transform.position.z);
			particleObject.SetActive(true);

			//Debug.Log(hit.point);

			if(explosionPrefab!=null)
			{
				Instantiate(explosionPrefab, hit.point, transform.rotation);
			}

			switch(hit.collider.tag)
			{
			case "Enemy":
				if(team==1)
				{
					OnHitEffects(hit.collider.gameObject);
				}
				break;
			case "Player":
				if(team==0)
				{
					OnHitEffects(hit.collider.gameObject);
				}
				break;
			}
		}
	}
	
	
	public void ReloadCall()
	{
		
	}


	void OnHitEffects(GameObject other)
	{
		if((team==1) && (other.tag == "Enemy"))
		{
			if(other.GetComponent<BomberControl>()!=null)
			{
				other.GetComponent<BomberControl>().TakeDamage(damage);
			}
			else if(other.GetComponent<BasicEnemyControl>()!=null)
			{
				other.GetComponent<BasicEnemyControl>().TakeDamage(damage);
			}
			else if(other.GetComponent<ArtilleryControl>()!=null)
			{
				other.GetComponent<ArtilleryControl>().TakeDamage(damage);
			}
			else if(other.GetComponent<BossControl>()!=null)
			{
				other.GetComponent<BossControl>().TakeDamage(damage);
			}
			if(damageNumber!=null)
			{
				Instantiate(damageNumber, other.transform.position, Quaternion.identity);
			}

		}
		if((team==0) && (other.tag == "Player"))
		{
			other.GetComponent<PlayerControl>().TakeDamage(damage);
		}
	}


}
