using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Vector2 vehicleLocation;

	public GameObject start_button;
	public GameObject play_button;
	public GameObject build_button;
	public GameObject options_button;
	public GameObject name_screen;
	public InputField player_name;
	public GameObject title_screen;
	public GameObject rotator_menu;

	// Use this for initialization
	void Start () {
		if (title_screen != null) {
			title_screen.SetActive(true);
		}
		showStart ();
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
			}
		}
	}


	public void showNameScreen(){
		if (name_screen != null && !name_screen.activeInHierarchy) {
			play_button.SetActive(false);
			build_button.SetActive(false);
			options_button.SetActive(false);
			name_screen.SetActive(true);
			if(PlayerPrefs.HasKey("PlayerName")){
				player_name.text = getScreenName ();
			}

		}
	}


	public void submitName(){
		if (player_name != null) {
			string name = player_name.text;
			if(saveScreenName(name)){
			//TODO:continue
				Debug.Log("Success in saving name!" + name);
				if(title_screen != null && rotator_menu != null){
					title_screen.SetActive(false);
					name_screen.SetActive(false);
					rotator_menu.SetActive(true);
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
	

}
