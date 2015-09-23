using UnityEngine;
using System.Collections;

//public class ScoreBoardControl : MonoBehaviour 
//{
//
//
//	// Use this for initialization
//	void Start () 
//	{
//		if(GameObject.FindGameObjectWithTag("Player1")!=null)
//		{
//		if(GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerControl>().enabled)
//		{
//			transform.FindChild("Name1").GetComponent<UnityEngine.UI.Text>().text = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerControl>().playerName;
//		}
//		else 
//		{
//			transform.FindChild("Name1").GetComponent<UnityEngine.UI.Text>().text = GameObject.FindGameObjectWithTag("Player1").GetComponent<RemotePlayerControl>().playerName;
//		}
//		}
//
//		if(GameObject.FindGameObjectWithTag("Player2")!=null)
//		{
//		if(GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerControl>().enabled)
//		{
//			transform.FindChild("Name2").GetComponent<UnityEngine.UI.Text>().text = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerControl>().playerName;
//		}
//		else
//		{
//			transform.FindChild("Name2").GetComponent<UnityEngine.UI.Text>().text = GameObject.FindGameObjectWithTag("Player2").GetComponent<RemotePlayerControl>().playerName;
//		}
//		}
//
//		if(GameObject.FindGameObjectWithTag("Player3")!=null)
//		{
//		if(GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerControl>().enabled)
//		{
//			transform.FindChild("Name3").GetComponent<UnityEngine.UI.Text>().text = GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerControl>().playerName;
//		}
//		else
//		{
//			transform.FindChild("Name3").GetComponent<UnityEngine.UI.Text>().text = GameObject.FindGameObjectWithTag("Player3").GetComponent<RemotePlayerControl>().playerName;
//		}
//		}
//
//		if(GameObject.FindGameObjectWithTag("Player4")!=null)
//		{
//		if(GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerControl>().enabled)
//		{
//			transform.FindChild("Name4").GetComponent<UnityEngine.UI.Text>().text = GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerControl>().playerName;
//		}
//		else
//		{
//			transform.FindChild("Name4").GetComponent<UnityEngine.UI.Text>().text = GameObject.FindGameObjectWithTag("Player4").GetComponent<RemotePlayerControl>().playerName;
//		}
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () 
//	{
//	
//	}
//
//
//	public void UpdateScore()
//	{
//		if(GameObject.FindGameObjectWithTag("Player1")!=null)
//		{
//		if(GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerControl>().enabled)
//		{
//			switch(GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerControl>().stock)
//			{
//			case 0:
//				transform.FindChild("Lives1").GetComponent<UnityEngine.UI.Text>().text = "";
//				break;
//			case 1:
//				transform.FindChild("Lives1").GetComponent<UnityEngine.UI.Text>().text = "o";
//				break;
//			case 2:
//				transform.FindChild("Lives1").GetComponent<UnityEngine.UI.Text>().text = "oo";
//				break;
//			case 3:
//				transform.FindChild("Lives1").GetComponent<UnityEngine.UI.Text>().text = "ooo";
//				break;
//			}
//		}
//		else
//		{
//			switch(GameObject.FindGameObjectWithTag("Player1").GetComponent<RemotePlayerControl>().stock)
//			{
//			case 0:
//				transform.FindChild("Lives1").GetComponent<UnityEngine.UI.Text>().text = "";
//				break;
//			case 1:
//				transform.FindChild("Lives1").GetComponent<UnityEngine.UI.Text>().text = "o";
//				break;
//			case 2:
//				transform.FindChild("Lives1").GetComponent<UnityEngine.UI.Text>().text = "oo";
//				break;
//			case 3:
//				transform.FindChild("Lives1").GetComponent<UnityEngine.UI.Text>().text = "ooo";
//				break;
//			}		
//		}
//		}
//
//		if(GameObject.FindGameObjectWithTag("Player2")!=null)
//		{
//		if(GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerControl>().enabled)
//		{
//			switch(GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerControl>().stock)
//			{
//			case 0:
//				transform.FindChild("Lives2").GetComponent<UnityEngine.UI.Text>().text = "";
//				break;
//			case 1:
//				transform.FindChild("Lives2").GetComponent<UnityEngine.UI.Text>().text = "o";
//				break;
//			case 2:
//				transform.FindChild("Lives2").GetComponent<UnityEngine.UI.Text>().text = "oo";
//				break;
//			case 3:
//				transform.FindChild("Lives2").GetComponent<UnityEngine.UI.Text>().text = "ooo";
//				break;
//			}
//		}
//		else
//		{
//			switch(GameObject.FindGameObjectWithTag("Player2").GetComponent<RemotePlayerControl>().stock)
//			{
//			case 0:
//				transform.FindChild("Lives2").GetComponent<UnityEngine.UI.Text>().text = "";
//				break;
//			case 1:
//				transform.FindChild("Lives2").GetComponent<UnityEngine.UI.Text>().text = "o";
//				break;
//			case 2:
//				transform.FindChild("Lives2").GetComponent<UnityEngine.UI.Text>().text = "oo";
//				break;
//			case 3:
//				transform.FindChild("Lives2").GetComponent<UnityEngine.UI.Text>().text = "ooo";
//				break;
//			}		
//		}
//		}
//
//		if(GameObject.FindGameObjectWithTag("Player3")!=null)
//		{
//		if(GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerControl>().enabled)
//		{
//			switch(GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerControl>().stock)
//			{
//			case 0:
//				transform.FindChild("Lives3").GetComponent<UnityEngine.UI.Text>().text = "";
//				break;
//			case 1:
//				transform.FindChild("Lives3").GetComponent<UnityEngine.UI.Text>().text = "o";
//				break;
//			case 2:
//				transform.FindChild("Lives3").GetComponent<UnityEngine.UI.Text>().text = "oo";
//				break;
//			case 3:
//				transform.FindChild("Lives3").GetComponent<UnityEngine.UI.Text>().text = "ooo";
//				break;
//			}
//		}
//		else
//		{
//			switch(GameObject.FindGameObjectWithTag("Player3").GetComponent<RemotePlayerControl>().stock)
//			{
//			case 0:
//				transform.FindChild("Lives3").GetComponent<UnityEngine.UI.Text>().text = "";
//				break;
//			case 1:
//				transform.FindChild("Lives3").GetComponent<UnityEngine.UI.Text>().text = "o";
//				break;
//			case 2:
//				transform.FindChild("Lives3").GetComponent<UnityEngine.UI.Text>().text = "oo";
//				break;
//			case 3:
//				transform.FindChild("Lives3").GetComponent<UnityEngine.UI.Text>().text = "ooo";
//				break;
//			}		
//		}
//		}
//
//		if(GameObject.FindGameObjectWithTag("Player4")!=null)
//		{
//		if(GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerControl>().enabled)
//		{
//			switch(GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerControl>().stock)
//			{
//			case 0:
//				transform.FindChild("Lives4").GetComponent<UnityEngine.UI.Text>().text = "";
//				break;
//			case 1:
//				transform.FindChild("Lives4").GetComponent<UnityEngine.UI.Text>().text = "o";
//				break;
//			case 2:
//				transform.FindChild("Lives4").GetComponent<UnityEngine.UI.Text>().text = "oo";
//				break;
//			case 3:
//				transform.FindChild("Lives4").GetComponent<UnityEngine.UI.Text>().text = "ooo";
//				break;
//			}
//		}
//		else
//		{
//			switch(GameObject.FindGameObjectWithTag("Player4").GetComponent<RemotePlayerControl>().stock)
//			{
//			case 0:
//				transform.FindChild("Lives4").GetComponent<UnityEngine.UI.Text>().text = "";
//				break;
//			case 1:
//				transform.FindChild("Lives4").GetComponent<UnityEngine.UI.Text>().text = "o";
//				break;
//			case 2:
//				transform.FindChild("Lives4").GetComponent<UnityEngine.UI.Text>().text = "oo";
//				break;
//			case 3:
//				transform.FindChild("Lives4").GetComponent<UnityEngine.UI.Text>().text = "ooo";
//				break;
//			}		
//		}
//		}
//
//	}
//
//
//}
