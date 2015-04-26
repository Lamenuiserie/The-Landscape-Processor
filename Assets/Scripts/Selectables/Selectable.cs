using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{
    /// <summary>
    /// The selectable manager.
    /// </summary>
    private SelectableManager selectableManager;

    /// <summary>
    /// Speed of the selectable.
    /// </summary>
    public float speed;
    /// <summary>
    /// The type of the selectable;
    /// </summary>
    public SelectableManager.Types type;
    /// <summary>
    /// The size of the selectable;
    /// </summary>
    public SelectableManager.Sizes size;

    /// <summary>
    /// Whether the selectable was selected.
    /// </summary>
    private bool selected;


	// Use this for initialization
	void Start ()
    {
        // 
        selectableManager = GetComponentInParent<SelectableManager>();

        // Init
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
        if (!selected)
        {
            selected = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f);
            gameObject.layer = LayerMask.NameToLayer("Selected");
        }
        else
        {
            selected = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);
            gameObject.layer = LayerMask.NameToLayer("Selectable");
        }
        
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Selected") && collider.GetComponent<Selectable>().type == type && size == collider.GetComponent<Selectable>().size && size != SelectableManager.Sizes.Big)
        {
            // TODO stop from spawning two versions
            selectableManager.spawnBiggerSelectable(gameObject, collider.gameObject, type, size);
        }
    }
}
