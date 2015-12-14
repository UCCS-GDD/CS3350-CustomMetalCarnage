using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour 
{
	public Color targetColor;
	public float colorRate;

	private UnityEngine.UI.Text textRenderer;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () 
	{
		textRenderer = GetComponent<UnityEngine.UI.Text>();
		if(textRenderer==null)
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}
		StartCoroutine("TurnRed");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


	IEnumerator TurnRed()
	{
		while(true)
		{
			if(textRenderer!=null)
			{
				textRenderer.color = Color.Lerp(textRenderer.color, targetColor, colorRate*Time.deltaTime);
			}
			else if(spriteRenderer!=null)
			{
				spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, colorRate*Time.deltaTime);
			}
//			if(textRenderer.color.Equals(targetColor))
//			{
//				StopCoroutine("TurnRed");
//				Debug.Log("Stopped");
//			}
			yield return null;
		}
	}


}
