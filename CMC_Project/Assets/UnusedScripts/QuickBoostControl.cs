using UnityEngine;
using System.Collections;

public class QuickBoostControl : MonoBehaviour 
{
	public float coolDownTime;
	private float lastUseTime;
	public Sprite HUDIcon;
	private UnityEngine.UI.Image HUDIconImage;
	public GameObject boostTrailEmitter;
	private GameObject boostTrailObject;
	public float boostStrength;
	public float boostDecay;
	public float lift;
	public float energyUsed;
	private float inputX;
	private float inputY;
	private CharacterController controller;
	private Vector3 moveDirection;
	private float inputModifyFactor;

	private RectTransform TechHUDTrans;
	private UnityEngine.UI.Text TechCDText;
	private float HUDScale;

	// Use this for initialization
	void Start () 
	{
		controller = transform.parent.GetComponent<CharacterController> ();
		TechHUDTrans = GameObject.FindGameObjectWithTag ("UIObject").transform.FindChild("HUD/Canvas/TechAbility/TechBackground/Fill").GetComponent<RectTransform>();
		TechCDText = GameObject.FindGameObjectWithTag ("UIObject").transform.FindChild("HUD/Canvas/TechAbility/Text").GetComponent<UnityEngine.UI.Text>();
		lastUseTime = -coolDownTime;
		HUDIconImage = GameObject.FindGameObjectWithTag ("UIObject").transform.FindChild ("HUD/Canvas/TechAbility/Icon").GetComponent<UnityEngine.UI.Image> ();
		HUDIconImage.sprite = HUDIcon;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(Input.GetButtonDown("TechAbility")&&((Time.time - lastUseTime)>coolDownTime))
		{
			inputX = Input.GetAxis("HorizontalTech");
			inputY = Input.GetAxis("VerticalTech");
			//Debug.Log("X: "+inputX);
			//Debug.Log("Y: "+inputY);
			if((energyUsed <= transform.parent.GetComponent<PlayerControl>().currentEnergy) && ((inputX != 0)||(inputY != 0)))
			{
				transform.parent.GetComponent<PlayerControl>().currentEnergy -= energyUsed;
				boostTrailObject = Instantiate(boostTrailEmitter, transform.position + new Vector3(0, -.2f, 0), transform.rotation) as GameObject;
				boostTrailObject.transform.parent = this.transform;
				inputModifyFactor = ((inputX != 0.0f) && (inputY != 0.0f))? .7071f : 1.0f;
				moveDirection = new Vector3(inputX * inputModifyFactor, lift, inputY * inputModifyFactor) * boostStrength;

				// Adjust HUD
				HUDIconImage.color = new Color(HUDIconImage.color.r, HUDIconImage.color.g, HUDIconImage.color.b, 0.1f);
				TechHUDTrans.localPosition = new Vector3(0, 0, 0);
				if(coolDownTime < 9.9f)
				{
					// Truncate to one decimal place
					TechCDText.text = HUDScale.ToString("f1");
				}
				else
				{
					TechCDText.text = "" + Mathf.CeilToInt(coolDownTime);
				}


				lastUseTime = Time.time;
			}
		}
		if(moveDirection.magnitude > 0.2f)
		{
			controller.Move(moveDirection * Time.deltaTime);
			moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, boostDecay*Time.deltaTime);
		}

		UpdateHUD();

	}


	void UpdateHUD()
	{
		if((Time.time - lastUseTime)<coolDownTime)
		{
			HUDScale = coolDownTime - (Time.time - lastUseTime);
			if(HUDScale < .05f)
			{
				TechCDText.text = "";
				HUDIconImage.color = new Color(HUDIconImage.color.r, HUDIconImage.color.g, HUDIconImage.color.b, 1);
			}
			else if(HUDScale > 9.9f)
			{
				TechCDText.text = "" + Mathf.CeilToInt(HUDScale);
			}
			else
			{
				// Truncate to one decimal place
				TechCDText.text = HUDScale.ToString("f1");
			}
			HUDScale /= coolDownTime;
			TechHUDTrans.localPosition = new Vector3(0, ((1-HUDScale)*(-71.5f)), 0);
		}
	}



}
