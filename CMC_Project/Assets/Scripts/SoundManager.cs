using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	// Use this for initialization
    public AudioClip startSound;
    public AudioClip hoverSound;
    public AudioClip smallTick;
    public AudioClip largeTick;
    public AudioClip backSound;
    public AudioClip menuMusic;
    AudioSource loopSource;
    AudioSource persistentSource;
	void Start () {
        DontDestroyOnLoad(this);
        //loopSource = new AudioSource();
        loopSource = this.GetComponents<AudioSource>()[0];
        persistentSource = this.GetComponents<AudioSource>()[1];
        //DontDestroyOnLoad(persistentSource);
        Debug.Log(loopSource);
        Debug.Log(loopSource.clip);
        playSustainedSound(menuMusic);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playSound(AudioClip sound)
    {
        AudioSource.PlayClipAtPoint(sound, this.transform.position);
        Debug.Log(sound + " played.");
    }

    public void playSound(AudioClip sound, float volume)
    {
        AudioSource.PlayClipAtPoint(sound, this.transform.position, volume);
        Debug.Log(sound + " played.");
    }

    public void playSustainedSound(AudioClip sound)
    {
        //loopSource.transform.position = this.transform.position;
        loopSource.clip = sound;
        loopSource.loop = true;
        loopSource.Play();
        Debug.Log(sound + " played.");
    }

    public void playPersistentSound(AudioClip sound)
    {
        //persistentSource.transform.position = this.transform.position;
        persistentSource.clip = sound;
        persistentSource.Play();
        Debug.Log(sound + " played.");
    }
}
