using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    /// <summary>
    /// Time before the target disapears.
    /// </summary>
    public float timeToLive;

    /// <summary>
    /// The timer tracking the time a target lived.
    /// </summary>
    private float timer;


	// Use this for initialization
	void Start ()
    {
        timer = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
	    if (timer >= timeToLive)
        {
            Destroy(gameObject);
        }
	}
}
