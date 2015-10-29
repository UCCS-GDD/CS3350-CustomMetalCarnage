using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerControl : MonoBehaviour 
{
	public int playerScore = 0;

	private int chassisNum;
	private int turretNum;
	private int numHardPoints;
	private List<int> weaponsNum = new List<int>();

	private string chassisName;
	private string turretName;
	private string weaponName;

	private Vector3 vehicleLocation;

	private GameObject tempChassis;
	private GameObject tempTurret;
	private GameObject compareWeapon;
	private List<GameObject> tempWeapons = new List<GameObject>();
	private int tempInt;
	private TurretControl.HardPoint currentHardPoint;

	private UnityEngine.UI.Text scoreText;


	// Use this for initialization
	void Awake() 
	{
		playerScore = 0;

		vehicleLocation = new Vector3(5f, -5f, 0f);

		if(PlayerPrefs.HasKey("Chassis"))
			chassisNum = PlayerPrefs.GetInt("Chassis");
		if(PlayerPrefs.HasKey("Turret"))
			turretNum = PlayerPrefs.GetInt("Turret");

		int ii = 1;
		while(true)
		{
			if(PlayerPrefs.HasKey("Weapon_"+ii))
			{
				weaponsNum.Add(PlayerPrefs.GetInt("Weapon_"+ii));
				Debug.Log("Weapon_"+ii);
				ii++;
			}
			else
			{
				break;
			}
		}

		InitializePlayer();
	}


	void Start()
	{
		scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<UnityEngine.UI.Text>();
	}


	// Update is called once per frame
	void Update() 
	{
		scoreText.text = playerScore.ToString();
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
			//Debug.Log("Current Hardpoint "+tempInt);
			if(tempInt<numHardPoints)
				currentHardPoint = tempTurret.GetComponent<TurretControl>().hardPoints[tempInt];
			
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

}
