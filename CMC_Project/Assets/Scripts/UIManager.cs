using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Vector2 vehicleLocation;

	public GameObject start_button;   
	public GameObject play_button;
	public GameObject build_button;
	public GameObject options_button;
    public GameObject back_button;
	public GameObject name_screen;
	public InputField player_name;
	public GameObject title_screen;
    public GameObject title_text;
    public GameObject help_screen;
	public GameObject rotator_menu;
    public AudioClip startClip;

    private AudioSource audioSource;

//
//	private GameObject tempChassis;
//	private GameObject tempTurret;
//	private GameObject compareWeapon;
//	private List<GameObject> tempWeapons = new List<GameObject>();
//	private int tempInt;
//
//	private int numHardPoints;
//	private TurretControl.HardPoint currentHardPoint;
//
//	private Vector3 oldPosition;
//	private Quaternion oldRotation;
//
//	private GameObject playerObject;

	// Use this for initialization
	void Start () {
		if (title_screen != null) {
			title_screen.SetActive(true);
            //audioManager = n.GetComponent<AudioSource>();
            //audio.Play();
		}
		showStart ();
	}
	

	void Update()
	{
		if(Input.GetButton("Cancel"))
		{
			//Debug.Log("Exiting");
			Application.Quit();
		}
	}


	void showStart (){
		if (start_button != null && !start_button.activeInHierarchy) {
			start_button.SetActive(true);
		}
	}


	public void showSelectionMenu(){
		if (play_button != null && build_button != null && options_button != null) {
			if(start_button.activeInHierarchy){
				//TODO: add a fade out 
				start_button.SetActive(false);
				play_button.SetActive(true);
				build_button.SetActive(true);
				options_button.SetActive(true);
                back_button.SetActive(false);
                //audioManager.playSound(startClip);)
                SoundManager.singleton.playSoundAtVolume(startClip, .4f);
                //AudioSource.PlayClipAtPoint(startClip, Camera.main.transform.position);
			}
		}
	}


	public void showNameScreen(){
		if (name_screen != null && !name_screen.activeInHierarchy) {
			play_button.SetActive(false);
			build_button.SetActive(false);
			options_button.SetActive(false);
            back_button.SetActive(true);
			name_screen.SetActive(true);
			if(PlayerPrefs.HasKey("PlayerName")){
				player_name.text = getScreenName ();
			}

		}
	}
    public void showHelpScreen()
    {
        play_button.SetActive(false);
        build_button.SetActive(false);
        options_button.SetActive(false);
        title_text.SetActive(false);
        back_button.SetActive(true);
        help_screen.SetActive(true);
    }

    public void backToPreviousMenu()
    {
        if(name_screen.activeInHierarchy && name_screen != null)
        {
            name_screen.SetActive(false);
            play_button.SetActive(true);
            build_button.SetActive(true);
            options_button.SetActive(true);
            back_button.SetActive(false);
        }
        else if(help_screen.activeInHierarchy && help_screen != null)
        {
            help_screen.SetActive(false);
            play_button.SetActive(true);
            build_button.SetActive(true);
            options_button.SetActive(true);
            title_text.SetActive(true);
            back_button.SetActive(false);
        }
        else if(Application.loadedLevel == 1)
        {
            Application.LoadLevel(0);
        }
    }


	public void submitName(){
		if (player_name != null) {
			string name = player_name.text;
			if(saveScreenName(name)){
			//TODO:continue
				//Debug.Log("Success in saving name!" + name);
				if(title_screen != null && rotator_menu != null){
					title_screen.SetActive(false);
					name_screen.SetActive(false);
					//rotator_menu.SetActive(true);
					// Change scenes here
					Application.LoadLevel(1);
				}
			} else{
				//TODO: Did not work
				Debug.LogError("Failed to save");
			}
		}
	}



	bool saveScreenName(string name){
		try{
			PlayerPrefs.SetString("PlayerName", name);
		}catch(PlayerPrefsException e){
			Debug.LogException(e);
			return false;
		}
		return true;
	}


	string getScreenName (){
		string name = "";
		try{
			name = PlayerPrefs.GetString("PlayerName");
		}catch(PlayerPrefsException e){
			Debug.LogException(e);
		}
		return name;
		 
	}


//	public void InitialLoad(loadoutPreferences lopScript)
//	{
//		// Show Chassis object
//		tempChassis = Instantiate(Resources.Load("Chassis/"+lopScript.panel2.transform.GetChild(lopScript.chassis-1).name), vehicleLocation, Quaternion.identity) as GameObject;
//
//		// Show Turret
//		tempTurret = Instantiate(Resources.Load("Turret/"+lopScript.panel3.transform.GetChild(lopScript.turret-1).name), vehicleLocation, Quaternion.identity) as GameObject;
//		tempTurret.transform.parent = tempChassis.transform;
//		numHardPoints = tempTurret.GetComponent<TurretControl>().hardPoints.Count;
//
//		// Show Weapons
//		foreach(int weapon in lopScript.weapons)
//		{
//			////Debug.Log("tempWeapons.Count="+tempWeapons.Count);
//			tempInt = 0;
//			foreach(GameObject weap in tempWeapons)
//			{
//				if(weap!=null)
//				{
//					tempInt++;
//				}
//				else
//				{
//					break;
//				}
//			}
//			////Debug.Log("Current Hardpoint "+tempInt);
//			if(tempInt<numHardPoints)
//			currentHardPoint = tempTurret.GetComponent<TurretControl>().hardPoints[tempInt];
//
//			if(weapon>0)
//			{
//				////Debug.Log("Added with weapon id "+weapon);
//				tempWeapons.Add(Instantiate(Resources.Load("Weapon/"+lopScript.panel4.transform.GetChild(weapon-1).name), vehicleLocation, Quaternion.identity) as GameObject);
//				// Attach weapon to Turret
//				tempWeapons[tempWeapons.Count-1].transform.parent = tempTurret.transform;
//				// move weapon to hardpoint position
//				tempWeapons[tempWeapons.Count-1].transform.localPosition = new Vector3(currentHardPoint.Location.x, currentHardPoint.Location.y, 0f);
//				tempWeapons[tempWeapons.Count-1].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentHardPoint.Angle));
//			}
//			else
//			{
//				////Debug.Log("Added null");
//				tempWeapons.Add(null);
//			}
//		}
//	}


//	public void ChangePart(string area, string partName)
//	{
//		switch(area)
//		{
//		case "Chassis":
//			if(tempChassis!=null)
//			{
//				// Un-parent Turret
//				tempTurret.transform.parent = null;
//				// Store old transform info
//				oldPosition = tempChassis.transform.position;
//				oldRotation = tempChassis.transform.rotation;
//				// Destroy old chassis object
//				Destroy(tempChassis);
//			}
//
//			// Load new chassis object
//			// Instantiate new chassis object
//			tempChassis = Instantiate(Resources.Load("Chassis/"+partName), oldPosition, oldRotation) as GameObject;
//			if(partName=="Drone")
//			{
//				tempChassis.transform.rotation = Quaternion.identity;
//			}
//
//			if(tempTurret!=null)
//			{
//				// Parent Turret to new chassis
//				tempTurret.transform.parent = tempChassis.transform;
//				tempTurret.transform.localPosition = Vector3.zero;
//			}
//			break;
//		case "Turret":
//			if(tempTurret!=null)
//			{
//				if((tempWeapons!=null) && (tempWeapons.Count>0))
//				{
//					// Detach Weapons
//					foreach(GameObject weapon in tempWeapons)
//					{
//						if(weapon!=null)
//						weapon.transform.parent = null;
//					}
//				}
//				// Store old transform info
//				oldPosition = tempTurret.transform.position;
//				oldRotation = tempTurret.transform.rotation;
//				// Destroy old turret object
//				Destroy(tempTurret);
//			}
//			
//			// Load new turret object
//			// Instantiate new turret object
//			tempTurret = Instantiate(Resources.Load("Turret/"+partName), oldPosition, oldRotation) as GameObject;
//
//			if((tempWeapons!=null) && (tempWeapons.Count>0))
//			{
//				// Attach weapons to new Turret
//				foreach(GameObject weapon in tempWeapons)
//				{
//					if(weapon!=null)
//					{
//						weapon.transform.parent = tempTurret.transform;
//						
//					}
//				}
//			}
//
//			// Get number of hardpoints on new turret
//			numHardPoints = tempTurret.GetComponent<TurretControl>().hardPoints.Count;
//
//			// Remove weapons beyond number of hardpoints and update positions
//			tempInt = 0;
//			while(tempInt<tempWeapons.Count)
//			{
//				if(tempInt >= numHardPoints)
//				{
//					Destroy(tempWeapons[tempInt]);
//					tempWeapons[tempInt] = null;
//				}
//				else
//				{
//					currentHardPoint = tempTurret.GetComponent<TurretControl>().hardPoints[tempInt];
//					if(tempWeapons[tempInt]!=null)
//					{
//						tempWeapons[tempInt].transform.localPosition = new Vector3(currentHardPoint.Location.x, currentHardPoint.Location.y, 0f);
//						tempWeapons[tempInt].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentHardPoint.Angle));
//					}
//				}
//				tempInt++;
//			}
//
//			// Update TurretControl delegates
//			tempTurret.GetComponent<TurretControl>().DelegateFireAndReload();
//
//			// Sync playerprefs
//			tempInt = 1;
//			while(true)
//			{
//				if(PlayerPrefs.HasKey("Weapon_"+tempInt))
//				{
//					if((tempWeapons.Count>=tempInt) && (tempWeapons[tempInt-1]!=null))
//					{
//						//Debug.Log(tempWeapons[tempInt-1].name);
//						switch(tempWeapons[tempInt-1].name)
//						{
//						case "MachineGun(Clone)":
//							PlayerPrefs.SetInt("Weapon_"+tempInt, 1);
//							break;
//						case "Cannon(Clone)":
//							PlayerPrefs.SetInt("Weapon_"+tempInt, 2);
//							break;
//						}
//					}
//					else
//					{
//						PlayerPrefs.SetInt("Weapon_"+tempInt, 0);
//					}
//					tempInt++;
//				}
//				else
//				{
//					break;
//				}
//			}
//
//			if(tempChassis!=null)
//			{
//				// Attach new turret to chassis
//				tempTurret.transform.parent = tempChassis.transform;
//				tempTurret.transform.localPosition = Vector3.zero;
//
//			}
//
//			numHardPoints = tempTurret.GetComponent<TurretControl>().hardPoints.Count;
//			break;
//		case "Weapon":
//			tempInt = 0;
//			foreach(GameObject weap in tempWeapons)
//			{
//				if(weap!=null)
//				{
//					tempInt++;
//				}
//			}
//			if(tempInt<numHardPoints)
//			{
//				// Get weapon prefab reference
//				compareWeapon = Resources.Load("Weapon/"+partName) as GameObject;
//
//				// Instantiate and add new weapon to list of weapons
//				tempInt = 0;
//				foreach(GameObject weap in tempWeapons)
//				{
//					if(weap!=null)
//					{
//						tempInt++;
//					}
//					else
//					{
//						break;
//					}
//				}
//				currentHardPoint = tempTurret.GetComponent<TurretControl>().hardPoints[tempInt];
//				//Debug.Log("Current Hard point: "+tempInt);
//
//				tempWeapons.Insert(tempInt, Instantiate(compareWeapon, vehicleLocation, Quaternion.identity) as GameObject);
//
//				if(tempWeapons.Count>=(tempInt+2))
//					tempWeapons.RemoveAt(tempInt+1);
//
//				//Debug.Log("Added "+partName);
//
//				// Sync with PlayerPrefs
//				int ii=1;
//				while(true)
//				{
//					if(PlayerPrefs.HasKey("Weapon_"+ii))
//					{
//						if((tempWeapons.Count>=ii) && (tempWeapons[ii-1]!=null))
//						{
//							//Debug.Log(tempWeapons[ii-1].name);
//							switch(tempWeapons[ii-1].name)
//							{
//							case "MachineGun(Clone)":
//								PlayerPrefs.SetInt("Weapon_"+ii, 1);
//								break;
//							case "Cannon(Clone)":
//								PlayerPrefs.SetInt("Weapon_"+ii, 2);
//								break;
//							}
//						}
//						else
//						{
//							PlayerPrefs.SetInt("Weapon_"+ii, 0);
//						}
//						ii++;
//					}
//					else
//					{
//						break;
//					}
//				}
//
//				// Attach weapon to Turret
//				tempWeapons[tempInt].transform.parent = tempTurret.transform;
//				// Update TurretControl Delegates
//				tempTurret.GetComponent<TurretControl>().DelegateFireAndReload();
//				// move weapon to hardpoint position
//				tempWeapons[tempInt].transform.localPosition = new Vector3(currentHardPoint.Location.x, currentHardPoint.Location.y, 0f);
//				tempWeapons[tempInt].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentHardPoint.Angle));
//			}
//			else
//			{
//				// Maybe play sound for out of hardpoints
//			}
//			break;
//		}
//	}

//
//	public void AddWeaponToPrefs(int i)
//	{
//		tempInt = 0;
//		foreach(GameObject weap in tempWeapons)
//		{
//			if(weap!=null)
//			{
//				tempInt++;
//			}
//			else
//			{
//				break;
//			}
//		}
//		PlayerPrefs.SetInt("Weapon_"+tempInt, i);
//	}

//
//	public void RemoveWeapon(string weaponName)
//	{
//		//compareWeapon = Resources.Load("Weapon/"+weaponName) as GameObject;
//		tempInt = -1;
//		for(int i=0; i<tempWeapons.Count; i++)
//		{
//			if((tempWeapons[i]!=null) && (tempWeapons[i].name==(weaponName+"(Clone)")))
//			{
//				tempInt = i;
//			}
//		}
//		//Debug.Log("tempInt="+tempInt);
//		if(tempInt>=0)
//		{
//			// PUT REMOVE WEAPON SOUND HERE
//            audioManager.playSound(audioManager.backSound, 1);
//
//			//Debug.Log("Removed weapon");
//			Destroy(tempWeapons[tempInt]);
//			tempWeapons[tempInt] = null;
//			// Update TurretControl Delegates
//			tempTurret.GetComponent<TurretControl>().DelegateFireAndReload();
//		}
//		else
//		{
//			// PUT CANNOT REMOVE WEAPON SOUND HERE
//		}
//		// Sync with PlayerPrefs
//		int ii=1;
//		while(true)
//		{
//			if(PlayerPrefs.HasKey("Weapon_"+ii))
//			{
//				if((tempWeapons.Count>=ii) && (tempWeapons[ii-1]!=null))
//				{
//					switch(tempWeapons[ii-1].name)
//					{
//					case "MachineGun(Clone)":
//						PlayerPrefs.SetInt("Weapon_"+ii, 1);
//						break;
//					case "Cannon(Clone)":
//						PlayerPrefs.SetInt("Weapon_"+ii, 2);
//						break;
//					}
//				}
//				else
//				{
//					PlayerPrefs.SetInt("Weapon_"+ii, 0);
//				}
//				ii++;
//			}
//			else
//			{
//				break;
//			}
//		}
//
//
//	}

}
