using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class loadoutPreferences : MonoBehaviour {

	public GameObject panel1;
	public GameObject panel2;
	public GameObject panel3;
	public GameObject panel4;
	public Sprite onSprite;


	// Use this for initialization
	void Start () {
		int gameType = 1; 
		int chassis = 1; 
		int weapon = 1; 
		int turret = 1;
		try{
			if(PlayerPrefs.HasKey("GameType"))
				gameType = PlayerPrefs.GetInt("GameType");
			if(PlayerPrefs.HasKey("Chassis"))
				chassis = PlayerPrefs.GetInt("Chassis");
			if(PlayerPrefs.HasKey("Turret"))
				turret = PlayerPrefs.GetInt("Turret");
			if(PlayerPrefs.HasKey("Weapon"))
				weapon = PlayerPrefs.GetInt("Weapon");

		}catch(PlayerPrefsException e){
				Debug.LogException(e);
		}
		if (panel1 != null && panel2 != null &&
			panel3 != null && panel4 != null && onSprite != null) {
			Button gt = panel1.transform.GetChild(gameType-1).GetComponentInChildren<Button>();
			gt.image.sprite = onSprite;
			Button chassisB = panel2.transform.GetChild(chassis-1).GetComponentInChildren<Button>();
			chassisB.image.sprite = onSprite;
			Button ta = panel3.transform.GetChild(turret-1).GetComponentInChildren<Button>();
			ta.image.sprite = onSprite;
			Button weap = panel4.transform.GetChild(weapon-1).GetComponentInChildren<Button>();
			weap.image.sprite = onSprite;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
