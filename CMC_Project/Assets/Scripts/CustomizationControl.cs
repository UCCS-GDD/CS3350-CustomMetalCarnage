using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CustomizationControl : MonoBehaviour {
	
	public Vector3 vehicleLocation;
	
	public GameObject start_button;
	public SoundManager audioManager;
	public AudioClip startClip;
	
	private AudioSource audioSource;
	
	private int chassisNum;
	private int turretNum;
	private int numHardPoints;
	private List<int> weaponsNum = new List<int>();
	
	private string chassisName;
	private string turretName;
	private string weaponName;

	private GameObject tempChassis;
	private GameObject tempTurret;
	private GameObject compareWeapon;
	private List<GameObject> tempWeapons = new List<GameObject>();
	private int tempInt;

	private TurretControl.HardPoint currentHardPoint;
	
	private Vector3 oldPosition;
	private Quaternion oldRotation;
	
	private GameObject playerObject;


	// Use this for initialization
	void Awake() 
	{				
		if(PlayerPrefs.HasKey("Chassis"))
		{
			chassisNum = PlayerPrefs.GetInt("Chassis");
		}
		else 
		{
			PlayerPrefs.SetInt("Chassis", 1);
			chassisNum = 1;
		}

		if(PlayerPrefs.HasKey("Turret"))
		{
			turretNum = PlayerPrefs.GetInt("Turret");
		}
		else
		{
			PlayerPrefs.SetInt("Turret", 1);
			turretNum = 1;
		}

		int ii = 1;
		while(true)
		{
			if(PlayerPrefs.HasKey("Weapon_"+ii))
			{
				weaponsNum.Add(PlayerPrefs.GetInt("Weapon_"+ii));
				//Debug.Log("Weapon_"+ii);
				ii++;
			}
			else
			{
				break;
			}
		}
		
		InitializePlayer();
	}


	// Use this for initialization
	void Start() 
	{
		audioManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
	}


	void Update()
	{
		if(Input.GetButton("Cancel"))
		{
			Application.Quit();
		}
	}

	
	void InitializePlayer()
	{
		switch(chassisNum)
		{
		case 1:
			chassisName = "Tank";
			break;
		case 2:
			chassisName = "Jeep";
			break;
		case 3:
			chassisName = "Drone";
			break;
		}
		switch(turretNum)
		{
		case 1:
			turretName = "Turret_2";
			break;
		case 2:
			turretName = "Turret_3";
			break;
		case 3:
			turretName = "Turret_4";
			break;
		}
		
		
		// Show Chassis object
		tempChassis = Instantiate(Resources.Load("Chassis/"+chassisName), vehicleLocation, Quaternion.identity) as GameObject;
		
		// Show Turret
		tempTurret = Instantiate(Resources.Load("Turret/"+turretName), vehicleLocation, Quaternion.identity) as GameObject;
		tempTurret.transform.parent = tempChassis.transform;
		numHardPoints = tempTurret.GetComponent<TurretControl>().hardPoints.Count;
		
		// Show Weapons
		foreach(int weapon in weaponsNum)
		{
			switch(weapon)
			{
			case 1:
				weaponName = "MachineGun";
				break;
			case 2:
				weaponName = "Cannon";
				break;
			case 3:
				weaponName = "ShotGun";
				break;
			case 4:
				weaponName = "RailGun";
				break;
			case 5:
				weaponName = "Laser";
				break;
			case 6:
				weaponName = "EMP";
				break;
			}
			
			tempInt = 0;
			foreach(GameObject weap in tempWeapons)
			{
				if(weap!=null)
				{
					tempInt++;
				}
				else
				{
					break;
				}
			}
			////Debug.Log("Current Hardpoint "+tempInt);
			if(tempInt<numHardPoints)
			{
				currentHardPoint = tempTurret.GetComponent<TurretControl>().hardPoints[tempInt];
			}
			
			if(weapon>0)
			{
				tempWeapons.Add(Instantiate(Resources.Load("Weapon/"+weaponName), vehicleLocation, Quaternion.identity) as GameObject);
				// Attach weapon to Turret
				tempWeapons[tempWeapons.Count-1].transform.parent = tempTurret.transform;
				// move weapon to hardpoint position
				tempWeapons[tempWeapons.Count-1].transform.localPosition = new Vector3(currentHardPoint.Location.x, currentHardPoint.Location.y, 0f);
				tempWeapons[tempWeapons.Count-1].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentHardPoint.Angle));
			}
			else
			{
				tempWeapons.Add(null);
			}
		}
	}
	
	
	public void ChangePart(string area, string partName)
	{
		switch(area)
		{
		case "Chassis":
			if(tempChassis!=null)
			{
				// Un-parent Turret
				tempTurret.transform.parent = null;
				// Store old transform info
				oldPosition = tempChassis.transform.position;
				oldRotation = tempChassis.transform.rotation;
				// Destroy old chassis object
				Destroy(tempChassis);
			}
			
			// Load new chassis object
			// Instantiate new chassis object
			tempChassis = Instantiate(Resources.Load("Chassis/"+partName), oldPosition, oldRotation) as GameObject;
			if(partName=="Drone")
			{
				tempChassis.transform.rotation = Quaternion.identity;
			}
			
			if(tempTurret!=null)
			{
				// Parent Turret to new chassis
				tempTurret.transform.parent = tempChassis.transform;
				tempTurret.transform.localPosition = Vector3.zero;
			}
			break;
		case "Turret":
			if(tempTurret!=null)
			{
				if((tempWeapons!=null) && (tempWeapons.Count>0))
				{
					// Detach Weapons
					foreach(GameObject weapon in tempWeapons)
					{
						if(weapon!=null)
							weapon.transform.parent = null;
					}
				}
				// Store old transform info
				oldPosition = tempTurret.transform.position;
				oldRotation = tempTurret.transform.rotation;
				// Destroy old turret object
				Destroy(tempTurret);
			}
			
			// Load new turret object
			// Instantiate new turret object
			tempTurret = Instantiate(Resources.Load("Turret/"+partName), oldPosition, oldRotation) as GameObject;
			
			if((tempWeapons!=null) && (tempWeapons.Count>0))
			{
				// Attach weapons to new Turret
				foreach(GameObject weapon in tempWeapons)
				{
					if(weapon!=null)
					{
						weapon.transform.parent = tempTurret.transform;
						
					}
				}
			}
			
			// Get number of hardpoints on new turret
			numHardPoints = tempTurret.GetComponent<TurretControl>().hardPoints.Count;
			
			// Remove weapons beyond number of hardpoints and update positions
			tempInt = 0;
			while(tempInt<tempWeapons.Count)
			{
				if(tempInt >= numHardPoints)
				{
					Destroy(tempWeapons[tempInt]);
					tempWeapons[tempInt] = null;
				}
				else
				{
					currentHardPoint = tempTurret.GetComponent<TurretControl>().hardPoints[tempInt];
					if(tempWeapons[tempInt]!=null)
					{
						tempWeapons[tempInt].transform.localPosition = new Vector3(currentHardPoint.Location.x, currentHardPoint.Location.y, 0f);
						tempWeapons[tempInt].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentHardPoint.Angle));
					}
				}
				tempInt++;
			}
			
			// Update TurretControl delegates
			tempTurret.GetComponent<TurretControl>().DelegateFireAndReload();
			
			// Sync playerprefs
			tempInt = 1;
			while(true)
			{
				if(PlayerPrefs.HasKey("Weapon_"+tempInt))
				{
					if((tempWeapons.Count>=tempInt) && (tempWeapons[tempInt-1]!=null))
					{
						//Debug.Log(tempWeapons[tempInt-1].name);
						switch(tempWeapons[tempInt-1].name)
						{
						case "MachineGun(Clone)":
							PlayerPrefs.SetInt("Weapon_"+tempInt, 1);
							break;
						case "Cannon(Clone)":
							PlayerPrefs.SetInt("Weapon_"+tempInt, 2);
							break;
						}
					}
					else
					{
						PlayerPrefs.SetInt("Weapon_"+tempInt, 0);
					}
					tempInt++;
				}
				else
				{
					break;
				}
			}
			
			if(tempChassis!=null)
			{
				// Attach new turret to chassis
				tempTurret.transform.parent = tempChassis.transform;
				tempTurret.transform.localPosition = Vector3.zero;
				
			}
			
			numHardPoints = tempTurret.GetComponent<TurretControl>().hardPoints.Count;
			break;
		case "Weapon":
			tempInt = 0;
			foreach(GameObject weap in tempWeapons)
			{
				if(weap!=null)
				{
					tempInt++;
				}
			}
			if(tempInt<numHardPoints)
			{
				// Get weapon prefab reference
				compareWeapon = Resources.Load("Weapon/"+partName) as GameObject;
				
				// Instantiate and add new weapon to list of weapons
				tempInt = 0;
				foreach(GameObject weap in tempWeapons)
				{
					if(weap!=null)
					{
						tempInt++;
					}
					else
					{
						break;
					}
				}
				currentHardPoint = tempTurret.GetComponent<TurretControl>().hardPoints[tempInt];
				//Debug.Log("Current Hard point: "+tempInt);
				
				tempWeapons.Insert(tempInt, Instantiate(compareWeapon, vehicleLocation, Quaternion.identity) as GameObject);
				
				if(tempWeapons.Count>=(tempInt+2))
					tempWeapons.RemoveAt(tempInt+1);
				
				//Debug.Log("Added "+partName);
				
				// Sync with PlayerPrefs
				int ii=1;
				while(true)
				{
					if(PlayerPrefs.HasKey("Weapon_"+ii))
					{
						if((tempWeapons.Count>=ii) && (tempWeapons[ii-1]!=null))
						{
							//Debug.Log(tempWeapons[ii-1].name);
							switch(tempWeapons[ii-1].name)
							{
							case "MachineGun(Clone)":
								PlayerPrefs.SetInt("Weapon_"+ii, 1);
								break;
							case "Cannon(Clone)":
								PlayerPrefs.SetInt("Weapon_"+ii, 2);
								break;
							}
						}
						else
						{
							PlayerPrefs.SetInt("Weapon_"+ii, 0);
						}
						ii++;
					}
					else
					{
						break;
					}
				}
				
				// Attach weapon to Turret
				tempWeapons[tempInt].transform.parent = tempTurret.transform;
				// Update TurretControl Delegates
				tempTurret.GetComponent<TurretControl>().DelegateFireAndReload();
				// move weapon to hardpoint position
				tempWeapons[tempInt].transform.localPosition = new Vector3(currentHardPoint.Location.x, currentHardPoint.Location.y, 0f);
				tempWeapons[tempInt].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentHardPoint.Angle));
			}
			else
			{
				// Maybe play sound for out of hardpoints
			}
			break;
		}
	}
	
	
	public void AddWeaponToPrefs(int i)
	{
		tempInt = 0;
		foreach(GameObject weap in tempWeapons)
		{
			if(weap!=null)
			{
				tempInt++;
			}
			else
			{
				break;
			}
		}
		PlayerPrefs.SetInt("Weapon_"+tempInt, i);
	}
	
	
	public void RemoveWeapon(string weaponName)
	{
		//compareWeapon = Resources.Load("Weapon/"+weaponName) as GameObject;
		tempInt = -1;
		for(int i=0; i<tempWeapons.Count; i++)
		{
			if((tempWeapons[i]!=null) && (tempWeapons[i].name==(weaponName+"(Clone)")))
			{
				tempInt = i;
			}
		}
		//Debug.Log("tempInt="+tempInt);
		if(tempInt>=0)
		{
			// PUT REMOVE WEAPON SOUND HERE
			audioManager.playStandardSound(audioManager.backSound);
			
			//Debug.Log("Removed weapon");
			Destroy(tempWeapons[tempInt]);
			tempWeapons[tempInt] = null;
			// Update TurretControl Delegates
			tempTurret.GetComponent<TurretControl>().DelegateFireAndReload();
		}
		else
		{
			// PUT CANNOT REMOVE WEAPON SOUND HERE
		}
		// Sync with PlayerPrefs
		int ii=1;
		while(true)
		{
			if(PlayerPrefs.HasKey("Weapon_"+ii))
			{
				if((tempWeapons.Count>=ii) && (tempWeapons[ii-1]!=null))
				{
					switch(tempWeapons[ii-1].name)
					{
					case "MachineGun(Clone)":
						PlayerPrefs.SetInt("Weapon_"+ii, 1);
						break;
					case "Cannon(Clone)":
						PlayerPrefs.SetInt("Weapon_"+ii, 2);
						break;
					}
				}
				else
				{
					PlayerPrefs.SetInt("Weapon_"+ii, 0);
				}
				ii++;
			}
			else
			{
				break;
			}
		}
		
		
	}
	
    public void PreviousLevel()
    {
        Application.LoadLevel(0);
    }
	
	public void NextLevel()
	{
		Application.LoadLevel(2);
	}
	
}