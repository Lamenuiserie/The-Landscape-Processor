using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    /// <summary>
    /// The projectile prefab.
    /// </summary>
    public Transform targetPrefab;

    /// <summary>
    /// The main camera.
    /// </summary>
    private Camera mainCamera;

    /// <summary>
    /// The custom cursor.
    /// </summary>
    private Transform cursor;
    /// <summary>
    /// Folder in which the targets are.
    /// </summary>
    private Transform targetsFolder;


    private Material titleMaterial;
    private Material authorMaterial;

    // UI
    private GameObject title;
    private GameObject author;
    private GameObject instructions;

    /// <summary>
    /// The projectile.
    /// </summary>
    private Transform projectile;

    //
    private bool uiFade;
    private float uiTimer;
    private float uiFadeTimer;


	// Use this for initialization
	void Start ()
    {
        mainCamera = GetComponentInChildren<Camera>();
        cursor = transform.FindChild("Cursor");
        targetsFolder = transform.FindChild("Targets");

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
        // Title, author and instructions
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

        /*if (Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("CursorPlane")))
        {
            cursor.position = hitInfo.point;
        }*/
        
        // Selection of the selectables
        if (CrossPlatformInputManager.GetButtonDown("Select"))
        {
            // Custom cursor positionning
            Ray mouseRay = mainCamera.ScreenPointToRay(CrossPlatformInputManager.mousePosition);
            RaycastHit hitInfo;
            if (!Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Window")))
            {
                if (Physics.SphereCast(mouseRay, 0.5f, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Selectable", "Selected")))
                {
                    hitInfo.collider.GetComponent<Selectable>().select();
                    AudioManager.instance.playSelection();
                    if (instructions.activeSelf)
                    {
                        instructions.SetActive(false);
                    }
                }

                /*Transform target = Instantiate(targetPrefab) as Transform;
                target.position = hitInfo.point;
                target.parent = targetsFolder;*/
            }
        }

        // Check if a target overlaps a selectable
        /*for (int i = 0; i < targetsFolder.childCount; i++)
        {
            Transform target = targetsFolder.GetChild(i);
            if (Physics.Raycast(mainCamera.transform.position, target.position - mainCamera.transform.position, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Selectable")))
            {
                hitInfo.collider.GetComponent<Selectable>().select();
                Destroy(target.gameObject);
                AudioManager.instance.playSelection();
                if (instructions.activeSelf)
                {
                    instructions.SetActive(false);
                }
            }
        }*/
	}


    /// <summary>
    /// In order for the raycasts to work it should be there emppty or not I think.
    /// </summary>
    void FixedUpdate()
    {

    }
}
