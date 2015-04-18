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
        Ray mouseRay = mainCamera.ScreenPointToRay(CrossPlatformInputManager.mousePosition);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * 6f, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Selectable")))
        {
            Debug.Log("lksjdf");
            select(hitInfo.collider.gameObject);
        }
    }

    private void select (GameObject selectable)
    {
        Destroy(selectable);
    }
}
