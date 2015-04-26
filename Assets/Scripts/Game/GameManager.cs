using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{
    /// <summary>
	/// The instance of the GameManager.
	/// </summary>
	private static GameManager _instance;

	/// <summary>
	/// Whether to show or hide the cursor during gameplay.
	/// </summary>
	public bool showCursorMode;
	/// <summary>
	/// Whether to lock the cursor during gameplay.
	/// </summary>
	public CursorLockMode lockCursorMode;


	/// <summary>
	/// Retrieve the instance of the game manager.
	/// </summary>
	/// <value>The game manager.</value>
	public static GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.Find("GameManager").GetComponent<GameManager>();
			}
			return _instance;
		}
	}


	// Use this for initialization
	void Start ()
	{
        // Init the cursor behaviour
        Cursor.lockState = lockCursorMode;
        Cursor.visible = showCursorMode;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }


#if UNITY_WEBPLAYER || UNITY_WEBGL
		// Go fullscreen
		if (Input.GetKeyDown(KeyCode.F))
		{
			Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
		}
#endif
	}
}
