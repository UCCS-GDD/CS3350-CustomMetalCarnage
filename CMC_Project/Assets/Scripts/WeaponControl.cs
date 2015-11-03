using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponControl : MonoBehaviour 
{
	public int weaponType;
	public Vector2 firingTip;
	public float rateOfFire;
	public GameObject projectilePrefab;
    public AudioClip firingSound;
    public SoundManager audioManager;
	public GameObject readyPrefab;

	private GameObject readyObject;
	private float lastFireTime = 0f;
	private List<GameObject> projectiles = new List<GameObject>();
	private GameObject tempObject;

	// Use this for initialization
	void Start () 
	{
		audioManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		if(readyPrefab!=null)
		{
			readyObject = Instantiate(readyPrefab, transform.position, transform.rotation) as GameObject;
			readyObject.transform.parent = transform;
			readyObject.transform.localPosition = new Vector3(firingTip.x, firingTip.y, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if((this!=null)&& (readyObject!=null) && (Time.time > ((1f/rateOfFire)+lastFireTime)))
		{
			readyObject.SetActive(true);
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
			tempObject = GetPooledProjectile();
            audioManager.playModulatedSound(firingSound, .3f);
			if(tempObject != null)
			{
				tempObject.transform.parent = this.transform;
				tempObject.transform.localPosition = new Vector3(firingTip.x, firingTip.y, 0f);
				tempObject.transform.localRotation = Quaternion.identity;
				tempObject.transform.parent = null;
				tempObject.SetActive(true);
				tempObject.GetComponent<Collider2D>().enabled = true;
				if(tempObject.GetComponent<SpriteRenderer>()!=null)
				{
					tempObject.GetComponent<SpriteRenderer>().enabled = true;
				}
				tempObject.GetComponent<ProjectileControl>().RefreshVelocity();
			}
			else
			{
				tempObject = Instantiate(projectilePrefab, transform.position, transform.rotation) as GameObject;
				tempObject.transform.parent = this.transform;
				tempObject.transform.localPosition = new Vector3(firingTip.x, firingTip.y, 0f);
				tempObject.transform.parent = null;
				projectiles.Add(tempObject);
			}
		}
	}


	public void ReloadCall()
	{

	}

	private GameObject GetPooledProjectile()
	{
		foreach(GameObject projectile in projectiles)
		{
			if((projectile != null) && (!projectile.activeInHierarchy))
			{
				return projectile;
			}
		}

		return null;
	}


}
