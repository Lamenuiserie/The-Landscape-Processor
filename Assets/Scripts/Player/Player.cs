using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    /// <summary>
    /// The only instance of the player.
    /// </summary>
    private static Player _instance;

    private SelectableManager selectableManager;
    private GroundManager groundManager;

    private int numberOfSelectablesSelected;


    /// <summary>
    /// Retrieve the instance of the audio manager.
    /// </summary>
    /// <value>The audio manager.</value>
    public static Player instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Player").GetComponent<Player>();
            }
            return _instance;
        }
    }

	// Use this for initialization
	void Start ()
    {
        selectableManager = GameObject.Find("SelectableManager").GetComponent<SelectableManager>();
        groundManager = GameObject.Find("GroundManager").GetComponent<GroundManager>();

        numberOfSelectablesSelected = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*if (32f - numberOfSelectablesSelected / 2f >= 0f)
        {
            selectableManager.groundSpeed = 32f - numberOfSelectablesSelected / 2f;
            selectableManager.mountainSpeed = 32f - numberOfSelectablesSelected / 2f;
            selectableManager.maxCloudSpeed = 10f - numberOfSelectablesSelected / 2f;
            groundManager.speed = 32f - numberOfSelectablesSelected / 2f;
        }*/
	}

    public void incSelectable ()
    {
        numberOfSelectablesSelected++;
    }

    public void decSelectable()
    {
        numberOfSelectablesSelected--;
    }
}
