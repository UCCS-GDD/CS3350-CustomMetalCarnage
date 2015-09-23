//using UnityEngine;
//using System.Collections;
//
//public class HealthUIControl : MonoBehaviour 
//{
//	public string playerTag;
//	private GameObject playerObject;
//	private bool local;
//	private PlayerControl lPlayerScript;
//	private RemotePlayerControl rPlayerScript;
//	private Canvas canvas;
//	private Camera cam;
//
//	// Use this for initialization
//	void Start () 
//	{
//		playerObject = GameObject.FindGameObjectWithTag (playerTag);
//		if(playerObject!=null)
//		{
//		if(playerObject.GetComponent<PlayerControl>().enabled==true)
//		{
//			local = true;
//			lPlayerScript = playerObject.GetComponent<PlayerControl>();
//		}
//		else if(playerObject.GetComponent<RemotePlayerControl>().enabled==true)
//		{
//			local = false;
//			rPlayerScript = playerObject.GetComponent<RemotePlayerControl>();
//		}
//		canvas = transform.parent.GetComponent<Canvas> ();
//		cam = playerObject.transform.FindChild ("CameraParent/Main Camera").GetComponent<Camera> ();
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () 
//	{
//		if(playerObject!=null)
//		{
//		if(local)
//		{
//			transform.FindChild("Fill").GetComponent<RectTransform>().localScale = new Vector3((lPlayerScript.currentHealth/lPlayerScript.maxHealth), 1, 1);
//		}
//		else
//		{
//			transform.FindChild("Fill").GetComponent<RectTransform>().localScale = new Vector3((rPlayerScript.currentHealth/rPlayerScript.maxHealth), 1, 1);
//		}
//		}
//	}
//
//
//	void LateUpdate()
//	{
//		if(playerObject!=null)
//		{
//		UpdatePosition ();
//		}
//	}
//
//
//	void UpdatePosition()
//	{
//		Vector3 pos;
//		float width = canvas.GetComponent<RectTransform>().sizeDelta.x;
//		float height = canvas.GetComponent<RectTransform>().sizeDelta.y;
//		float x = Camera.main.WorldToScreenPoint(playerObject.transform.position).x / Screen.width;
//		float y = Camera.main.WorldToScreenPoint(playerObject.transform.position).y / Screen.height;
//		pos = new Vector3(width * x - width / 2, (y * height - height / 2)-50);    
//		transform.GetComponent<RectTransform>().anchoredPosition = pos;    
//	}
//
//
//
//}
