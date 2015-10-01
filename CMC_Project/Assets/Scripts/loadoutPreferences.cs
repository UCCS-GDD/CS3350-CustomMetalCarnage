using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class loadoutPreferences : MonoBehaviour {

	public GameObject uiManager;
	public GameObject panel1;
	public GameObject panel2;
	public GameObject panel3;
	public GameObject panel4;
	public Sprite onSprite;

	private Button weap;

	public int gameType = 1; 
	public int chassis = 1;  
	public int turret = 1;
	public List<int> weapons = new List<int>();

	Button gt;
	Button chassisB;
	Button ta;

	private int ii = 1;


	// Use this for initialization
	void Start () {
		try{
			if(PlayerPrefs.HasKey("GameType"))
				gameType = PlayerPrefs.GetInt("GameType");
			if(PlayerPrefs.HasKey("Chassis"))
				chassis = PlayerPrefs.GetInt("Chassis");
			if(PlayerPrefs.HasKey("Turret"))
				turret = PlayerPrefs.GetInt("Turret");

			while(true)
			{
				if(PlayerPrefs.HasKey("Weapon_"+ii))
				{
					weapons.Add(PlayerPrefs.GetInt("Weapon_"+ii));
					Debug.Log("Weapon_"+ii);
					ii++;
				}
				else
				{
					break;
				}
			}

		}catch(PlayerPrefsException e){
				Debug.LogException(e);
		}

		HighlightParts();

		uiManager.GetComponent<UIManager>().InitialLoad(this);



	}
	
	public void HighlightParts()
	{
		if (panel1 != null && panel2 != null &&
		    panel3 != null && panel4 != null && onSprite != null) 
		{
			gt = panel1.transform.GetChild(gameType-1).GetComponentInChildren<Button>();
			gt.image.sprite = onSprite;
			
			chassisB = panel2.transform.GetChild(chassis-1).GetComponentInChildren<Button>();
			chassisB.image.sprite = onSprite;
			
			ta = panel3.transform.GetChild(turret-1).GetComponentInChildren<Button>();
			ta.image.sprite = onSprite;
			
//			foreach(int weapon in weapons)
//			{
//				if(weapon!=0)
//				{
//					weap = panel4.transform.GetChild(weapon-1).GetComponentInChildren<Button>();
//					weap.image.sprite = onSprite;
//				}
//			}
		}
	}


}
