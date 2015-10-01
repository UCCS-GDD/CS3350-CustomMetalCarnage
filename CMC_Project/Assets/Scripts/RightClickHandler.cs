using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RightClickHandler : MonoBehaviour, IPointerClickHandler 
{
	public GameObject uiManager;
	private UIManager uiScript;

	public GameObject ssObject;
	private selectSettings ssScript;

	// Use this for initialization
	void Start () 
	{
		uiScript = uiManager.GetComponent<UIManager>();
		ssScript = ssObject.GetComponent<selectSettings>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(eventData.button == PointerEventData.InputButton.Right)
		{
			Debug.Log("Right "+this.gameObject.name);
			uiScript.RemoveWeapon(this.gameObject.name);
			ssScript.onSelection();
		}
	}
}
