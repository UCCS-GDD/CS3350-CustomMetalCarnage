using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	// Use this for initialization
    public AudioClip startSound;
    public AudioClip hoverSound;
    public AudioClip smallTick;
    public AudioClip largeTick;
    public AudioClip backSound;
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playSound(AudioClip sound)
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
        Debug.Log(sound + " played.");
    }
}
