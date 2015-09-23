using UnityEngine;
using System.Collections;

public class EnergyBarControl : MonoBehaviour 
{
	private float scale;
	private RectTransform rectTrans;
	private PlayerControl playerScript;
	private UnityEngine.UI.Text energyText;

	// Use this for initialization
	void Start () 
	{
		// Will have to set this after the camera has been moved to the player's CameraParent object
		// Player is found through the Canvas to allow splitscreen
		//playerScript = transform.parent.GetComponent<Canvas>().worldCamera.transform.parent.parent.GetComponent<PlayerControl> ();
		playerScript = GameObject.FindGameObjectWithTag ("NetworkController").GetComponent<NetworkControl> ().myPlayer.GetComponent<PlayerControl> ();
		rectTrans = transform.FindChild("BarBackground/Fill").GetComponent<RectTransform> ();
		energyText = transform.FindChild ("StatusBackground/Text").GetComponent<UnityEngine.UI.Text> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(playerScript.currentEnergy < 0)
		{
			scale = 0;
			energyText.text = "Energy  0";
		}
		else
		{
			scale = playerScript.currentEnergy / playerScript.maxEnergy;
			energyText.text = "Energy " + Mathf.Floor(playerScript.currentEnergy);
		}
		rectTrans.localScale = new Vector3(scale, rectTrans.localScale.y, rectTrans.localScale.y);

	}
}
