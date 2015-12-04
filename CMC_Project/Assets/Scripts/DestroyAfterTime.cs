using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour 
{
	public float timeUntilDestroyed;
    public AudioClip effectSound;
	private float startTime;

	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
        if (effectSound != null)
        {
            SoundManager.singleton.playModulatedSound(effectSound, .5f);
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		if((Time.time-startTime)>timeUntilDestroyed)
		{
			GameObject.Destroy(this.gameObject);
		}
	}

}
