using UnityEngine;
using System.Collections;

public class EndArea : MonoBehaviour
{
    /// <summary>
    /// The selectable manager;
    /// </summary>
    private SelectableManager selectableManager;


	// Use this for initialization
	void Start ()
    {
        selectableManager = GetComponentInParent<SelectableManager>();
	}

    void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Selectable"))
        {
            //selectableManager.warp(collider.transform);
            Destroy(collider.gameObject);
        }
    }
}
