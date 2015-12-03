using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerControl : MonoBehaviour 
{
	public static int playerScore = 0;
	private int highScore;

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

	private UnityEngine.UI.Text scoreText1;
	private UnityEngine.UI.Text scoreText2;
	private UnityEngine.UI.Text highScoreText1;
	private UnityEngine.UI.Text highScoreText2;


	public float shadeDimSpeed;
	
	private SpriteRenderer shadeSprite;
	private UnityEngine.UI.Image gameOverImage;

	public Color beatScoreColor;
    public GameObject retry_Button;
    public GameObject menu_Button;

	// Use this for initialization
	void Awake() 
	{
		vehicleLocation = new Vector3(5f, -5f, 1f);

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


	void Start()
	{
		scoreText1 = GameObject.FindGameObjectWithTag("Score").GetComponent<UnityEngine.UI.Text>();
		scoreText2 = GameObject.FindGameObjectWithTag("Score2").GetComponent<UnityEngine.UI.Text>();
		highScoreText1 = GameObject.FindGameObjectWithTag("HighScore").GetComponent<UnityEngine.UI.Text>();
		highScoreText2 = GameObject.FindGameObjectWithTag("HighScore2").GetComponent<UnityEngine.UI.Text>();
		shadeSprite = GameObject.FindGameObjectWithTag("Shade").GetComponent<SpriteRenderer>();
		gameOverImage = GameObject.FindGameObjectWithTag("GameOver").GetComponent<UnityEngine.UI.Image>();

		playerScore = 0;
		if(PlayerPrefs.HasKey("HighScore"))
		{
			highScore = PlayerPrefs.GetInt("HighScore");
			highScoreText1.text = highScore.ToString();
			highScoreText2.text = highScore.ToString();
		}
	}


	// Update is called once per frame
	void Update() 
	{
		scoreText1.text = playerScore.ToString();
		scoreText2.text = playerScore.ToString();

		if(playerScore > highScore)
		{
			if(highScoreText2.color != beatScoreColor)
			{
				highScoreText2.color = beatScoreColor;
				GameObject.FindGameObjectWithTag("HighScore").transform.parent.GetComponent<UnityEngine.UI.Text>().color = beatScoreColor;
				//Debug.Log("Color change");
			}
			highScore = playerScore;
			highScoreText1.text = highScore.ToString();
			highScoreText2.text = highScore.ToString();

			PlayerPrefs.SetInt("HighScore", highScore);
		}

//		if(Input.GetButton("Submit"))
//		{
//			Application.LoadLevel(1);
//		}
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


	public void PlayerDeath()
	{
		Time.timeScale = 0f;
		StartCoroutine("DeathMenu");
	}
	
	
	IEnumerator DeathMenu()
	{
		while(true)
		{
			if(shadeSprite.color.a < 1f)
			{
				shadeSprite.color = new Color(0f, 0f, 0f, shadeSprite.color.a + shadeDimSpeed); 
				gameOverImage.color = new Color(200f, 0f, 0f, gameOverImage.color.a + shadeDimSpeed);
			}

            retry_Button.SetActive(true);
			menu_Button.SetActive(true);

			if(Input.GetButton("Submit"))
			{
                Retry();
			}

			if(Input.GetButton("Cancel"))
			{
                QuitToMenu();
			}
			
			yield return null;
		}
	}

    public void Retry()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(1);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(0);
    }

}
