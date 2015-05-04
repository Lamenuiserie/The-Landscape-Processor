using UnityEngine;
using System.Collections;

public class EndArea : MonoBehaviour
{



	// Use this for initialization
	void Start ()
    {
        
	}

    void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Selectable"))
        {
            Destroy(collider.gameObject);
        }
    }
}
