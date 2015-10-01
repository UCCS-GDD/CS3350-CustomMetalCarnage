using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class selectSettings : MonoBehaviour {
	public Sprite onSprite;
	public Sprite offSprite;
	public string save_key;

	public GameObject uiManager;
	private UIManager uiScript;

	public GameObject loadoutObject;
	private loadoutPreferences lopScript;

	Button[] buttons;
	// Use this for initialization
	void Start () {

		uiScript = uiManager.GetComponent<UIManager>();
		lopScript = loadoutObject.GetComponent<loadoutPreferences>();

		if (onSprite == null || offSprite == null)
			Debug.Log ("Missing Sprite");

		buttons = gameObject.GetComponentsInChildren<Button>();
		if (buttons != null) {
			Debug.Log ("Found " + buttons.Length + " buttons ");
		} else {
			Debug.Log("NO BUTTONS FOUND!!!!");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onSelection(GameObject selected){
		Debug.Log (selected.ToString());
		if (buttons != null) {
			foreach(Button b in buttons){
				b.image.sprite = offSprite;
				//Debug.Log(b.ToString);
			}
			if(!save_key.Equals("Weapon"))
			{
				Image i = selected.GetComponent<Image>();
				i.sprite = onSprite;
			}

			uiScript.ChangePart(save_key, selected.name);
		}
	}


	public void onSelection()
	{
		if (buttons != null) 
		{
			foreach(Button b in buttons)
			{
				b.image.sprite = offSprite;
				//Debug.Log(b.ToString);
			}
			//lopScript.HighlightParts();			
		}
	}


	public void addToPreferences (int i){
		try{
			if(save_key != null)
			{
				if(save_key.Equals("Weapon"))
				{
					uiScript.AddWeaponToPrefs(i);
				}
				else
				{
					PlayerPrefs.SetInt(save_key, i);
				}
			}
		}catch(PlayerPrefsException e){
			Debug.LogException(e);
		}
	}
}
