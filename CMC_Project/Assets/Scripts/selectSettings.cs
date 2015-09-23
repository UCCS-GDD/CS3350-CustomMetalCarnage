using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class selectSettings : MonoBehaviour {
	public Sprite onSprite;
	public Sprite offSprite;
	public string save_key;

	Button[] buttons;
	// Use this for initialization
	void Start () {
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
			Image i = selected.GetComponent<Image>();
			i.sprite = onSprite;

		}
	}

	public void addToPreferences (int i){
		try{
			if(save_key != null)
				PlayerPrefs.SetInt(save_key, i);
		}catch(PlayerPrefsException e){
			Debug.LogException(e);
		}
	}
}
