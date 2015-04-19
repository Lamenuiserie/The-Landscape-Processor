using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    /// <summary>
    /// The main camera.
    /// </summary>
    private Camera mainCamera;

    // UI
    private Text title;
    private Text author;
    private Text instructions;

    //
    private bool uiFade;
    private float uiTimer;
    private float uiFadeTimer;


	// Use this for initialization
	void Start ()
    {
        mainCamera = GetComponentInChildren<Camera>();
        title = GameObject.Find("Title").GetComponent<Text>();
        author = GameObject.Find("Author").GetComponent<Text>();
        instructions = GameObject.Find("Instructions").GetComponent<Text>();

        uiFade = false;
        uiTimer = 0f;
        uiFadeTimer = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (title.enabled && !uiFade)
        {
            if (uiTimer < 6f)
            {
                uiTimer += Time.deltaTime;
            }
            else
            {
                uiFade = true;
                uiFadeTimer = 0f;
            }
        }

        if (uiFade)
        {
            uiFadeTimer += Time.deltaTime;
            if (uiFadeTimer > 3f)
            {
                title.enabled = false;
                author.enabled = false;
                uiFade = false;
            }
            else
            {
                title.color = new Color(title.color.r, title.color.g, title.color.b, 1f - (uiFadeTimer / 3f));
                author.color = new Color(author.color.r, author.color.g, author.color.b, 1f - (uiFadeTimer / 3f));
            }
        }
	}

    void FixedUpdate ()
    {
        if (CrossPlatformInputManager.GetButton("Select"))
        {
            Ray mouseRay = mainCamera.ScreenPointToRay(CrossPlatformInputManager.mousePosition);
            if (!Physics.Raycast(mouseRay, Mathf.Infinity, LayerMask.GetMask("Window", "Default")))
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Selectable")))
                {
                    hitInfo.collider.GetComponent<Selectable>().select();
                    if (instructions.enabled)
                    {
                        instructions.enabled = false;
                    }
                }
            }
        }
    }
}
