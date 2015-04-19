using UnityEngine;
using System.Collections;

public class SelectableManager : MonoBehaviour
{
    /************* PUBLIC VARIABLES **************/
    // Procedural spawning of selectables
    public float minTimeBetweenBuilding;
    public float minTimeBetweenMountain;
    public float selectableSpeed;

    // Layers
    public Vector3 layer1;
    public Vector3 layer2;
    public Vector3 layer3;

    // Prefabs
    public Transform buildingSmall;
    public Transform buildingMedium;
    public Transform buildingBig;
    public Transform mountainSmall;
    public Transform mountainMedium;
    public Transform mountainBig;
    public Transform road;
    public Transform pylon;


    /************* IMPORTANT GAME OBJECTS **************/
    // Warping areas
    private Transform startArea;
    private Transform selectablesFolder;


    /************* PRIVATE VARIABLES **************/
    // Timers
    private float timerBuilding;
    private float timerMountain;


	// Use this for initialization
	void Start ()
    {
        // Important game objects
        startArea = transform.FindChild("StartArea");
        selectablesFolder = transform.FindChild("Selectables");

        // Init
        timerBuilding = 0f;
        timerMountain = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // TODO REMOVE
        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (Selectable selectable in selectablesFolder.GetComponentsInChildren<Selectable>())
            {
                selectable.speed += 20f;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (Selectable selectable in selectablesFolder.GetComponentsInChildren<Selectable>())
            {
                selectable.speed -= 20f;
            }
        }

        // Timer between spawn
        timerBuilding += Time.deltaTime;
        timerMountain += Time.deltaTime;

        // Spawn building
        if (timerBuilding >= minTimeBetweenBuilding)
        {
            Transform building = spawnSelectable(buildingSmall, buildingMedium, buildingBig);
            building.parent = selectablesFolder;
            timerBuilding = 0f;
        }

        // Spawn mountain
        if (timerMountain >= minTimeBetweenMountain)
        {
            Transform mountain = spawnSelectable(mountainSmall, mountainMedium, mountainBig);
            mountain.parent = selectablesFolder;
            timerMountain = 0f;
        }
	}

    /// <summary>
    /// Spawn a selectable and return the transform of the object.
    /// </summary>
    /// <param name="smallPrefab"></param>
    /// <param name="mediumPrefab"></param>
    /// <param name="bigPrefab"></param>
    /// <returns></returns>
    private Transform spawnSelectable (Transform smallPrefab, Transform mediumPrefab, Transform bigPrefab)
    {
        // Select size
        Transform prefab = smallPrefab;
        int size = Random.Range(0, 3);
        switch (size)
        {
            case 0:
                prefab = smallPrefab;
                break;
            case 1:
                prefab = mediumPrefab;
                break;
            case 2:
                prefab = bigPrefab;
                break;
        }

        // Select layer
        Vector3 layer = layer1;
        int depth = Random.Range(0, 3);
        switch (size)
        {
            case 0:
                layer = layer1;
                break;
            case 1:
                layer = layer2;
                break;
            case 2:
                layer = layer3;
                break;
        }

        // Spawn the selectable
        Transform selectable = Instantiate(prefab, new Vector3(startArea.position.x, layer.y, layer.z), Quaternion.identity) as Transform;

        // Set speed
        selectable.GetComponent<Selectable>().speed = selectableSpeed;

        return selectable;
    }

    /// <summary>
    /// Warp an object back to the start.
    /// </summary>
    /// <param name="objectToWarp"></param>
    public void warp (Transform objectToWarp)
    {
        objectToWarp.position = new Vector3(startArea.transform.position.x, objectToWarp.position.y, objectToWarp.position.z);
    }
}
