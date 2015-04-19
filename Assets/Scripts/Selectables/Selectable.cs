using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{
    /// <summary>
    /// Speed of the selectable.
    /// </summary>
    public float speed { get; set; }


    /// <summary>
    /// Whether the selectable was selected.
    /// </summary>
    private bool selected;


	// Use this for initialization
	void Start ()
    {
        selected = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!selected)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f), Space.World);
        }
	}

    public void select ()
    {
        selected = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
