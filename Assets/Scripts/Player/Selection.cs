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

    private Material titleMaterial;
    private Material authorMaterial;

    // UI
    private GameObject title;
    private GameObject author;
    private GameObject instructions;

    //
    private bool uiFade;
    private float uiTimer;
    private float uiFadeTimer;


	// Use this for initialization
	void Start ()
    {
        mainCamera = GetComponentInChildren<Camera>();
        title = GameObject.Find("Title");
        author = GameObject.Find("Author");
        instructions = GameObject.Find("Instructions");

        titleMaterial = title.GetComponent<MeshRenderer>().material;
        authorMaterial = author.GetComponent<MeshRenderer>().material;

        uiFade = false;
        uiTimer = 0f;
        uiFadeTimer = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (title.activeSelf && !uiFade)
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
                title.SetActive(false);
                author.SetActive(false);
                uiFade = false;
            }
            else
            {
                titleMaterial.color = new Color(titleMaterial.color.r, titleMaterial.color.g, titleMaterial.color.b, 1f - (uiFadeTimer / 3f));
                authorMaterial.color = new Color(authorMaterial.color.r, authorMaterial.color.g, authorMaterial.color.b, 1f - (uiFadeTimer / 3f));
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("Select"))
        {
            Ray mouseRay = mainCamera.ScreenPointToRay(CrossPlatformInputManager.mousePosition);
            if (!Physics.Raycast(mouseRay, Mathf.Infinity, LayerMask.GetMask("Window")))
            {
                RaycastHit hitInfo;
                if (Physics.SphereCast(mouseRay, 0.5f, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Selectable", "Selected")))
                {
                    hitInfo.collider.GetComponent<Selectable>().select();
                    AudioManager.instance.playSelection();
                    if (instructions.activeSelf)
                    {
                        instructions.SetActive(false);
                    }
                }
            }
        }
	}



    void FixedUpdate()
    {

    }
}
