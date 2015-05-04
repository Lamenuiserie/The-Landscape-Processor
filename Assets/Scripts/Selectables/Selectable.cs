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
    public bool selected { get; private set; }
    /// <summary>
    /// Whether the selectable is the result of a merge or not.
    /// </summary>
    public bool haveBeenMerged { get; set; }
    /// <summary>
    /// Whether the selectable is already merging with another.
    /// </summary>
    private bool beingMerged;


	// Use this for initialization
	void Start ()
    {
        // 
        selectableManager = GetComponentInParent<SelectableManager>();

        // Init
        if (haveBeenMerged)
        {
            selected = true;
        }
        else
        {
            selected = false;
        }
        beingMerged = false;
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
            Player.instance.incSelectable();
            selected = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f);
            gameObject.layer = LayerMask.NameToLayer("Selected");
        }
        else
        {
            Player.instance.decSelectable();
            selected = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);
            gameObject.layer = LayerMask.NameToLayer("Selectable");
        }
        
    }

    void OnTriggerEnter (Collider collider)
    {
        Selectable other = collider.GetComponent<Selectable>();
        if ((collider.gameObject.layer == LayerMask.NameToLayer("Selected") ^ gameObject.layer == LayerMask.NameToLayer("Selected")) && (!beingMerged && !other.beingMerged) && other.type == type && other.size == size && size != SelectableManager.Sizes.Big)
        {
            other.beingMerged = true;
            beingMerged = true;
            AudioManager.instance.playMerging();
            selectableManager.mergeSelectables(gameObject, collider.gameObject, type, size);
        }
    }
}
