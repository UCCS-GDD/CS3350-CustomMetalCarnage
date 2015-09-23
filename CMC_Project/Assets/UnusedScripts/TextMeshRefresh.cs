using UnityEngine;
using System.Collections;

public class TextMeshRefresh : MonoBehaviour {

	// FOR SOME CRAZY REASON, THE ORBITRON FONT MAKES YOU 
	// ADJUST SOME PIECE OF THE FONT AT RUNTIME, OR ELSE
	// IT FREAKS OUT...ALOT. HENCE THIS SCRIPT THAT JUST
	// CHANGES THE FONTSTYLE TO BOLD AND BACK. WHATEVS...

	private TextMesh thisTextMesh;

	// Use this for initialization
	void Start () 
	{
		thisTextMesh = this.GetComponent<TextMesh> ();
		thisTextMesh.fontStyle = FontStyle.Bold;
		thisTextMesh.fontStyle = FontStyle.Normal;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
