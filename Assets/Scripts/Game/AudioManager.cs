using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // SFX
    public AudioClip[] selections;

	/// <summary>
	/// The only instance of the audio manager.
	/// </summary>
	private static AudioManager _instance;

	/// <summary>
	/// The audio source.
	/// </summary>
	private AudioSource thisAudio;


	/// <summary>
	/// Retrieve the instance of the audio manager.
	/// </summary>
	/// <value>The audio manager.</value>
	public static AudioManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.Find("AudioManager").GetComponent<AudioManager>();
			}
			return _instance;
		}
	}

	// Use this for initialization
	void Start ()
	{
		// Components
		thisAudio = GetComponents<AudioSource>()[0];
	}

    public void playSelection ()
    {
        if (!thisAudio.isPlaying)
        {
            thisAudio.PlayOneShot(selections[Random.Range(0, selections.Length)], 0.7f);
        }
    }
}