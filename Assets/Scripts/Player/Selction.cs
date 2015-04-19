using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Selction : MonoBehaviour
{
    /// <summary>
    /// The main camera.
    /// </summary>
    private Camera mainCamera;


	// Use this for initialization
	void Start ()
    {
        mainCamera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void FixedUpdate ()
    {
        if (CrossPlatformInputManager.GetButton("Select"))
        {
            Ray mouseRay = mainCamera.ScreenPointToRay(CrossPlatformInputManager.mousePosition);
            if (!Physics.Raycast(mouseRay, Mathf.Infinity, LayerMask.GetMask("Window")))
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Selectable")))
                {
                    hitInfo.collider.GetComponent<Selectable>().select();
                }
            }
        }
    }
}
