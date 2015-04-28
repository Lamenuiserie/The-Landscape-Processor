using UnityEngine;
using System.Collections;

public class SelectableManager : MonoBehaviour
{
    /************* ENUMS **************/
    /// <summary>
    /// Types of selectables.
    /// </summary>
    public enum Types { Building, Tree, Rock, Mountain, Cloud };
    /// <summary>
    /// Sizes of selectables.
    /// </summary>
    public enum Sizes { Small, Medium, Big };

    /************* PUBLIC VARIABLES **************/
    // Procedural spawning of selectables
    public float minTimeBetweenSpawn;
    public float maxTimeBetweenSpawn;
    public float selectableSpeed;
    public float minTimeBetweenCloudSpawn;
    public float maxTimeBetweenCloudSpawn;
    public float minCloudSpeed;
    public float maxCloudSpeed;

    // Prefabs
    public Transform[] selectablePrefabs;
    public Transform buildingSmall;
    public Transform buildingMedium;
    public Transform buildingBig;
    public Transform mountainSmall;
    public Transform mountainMedium;
    public Transform mountainBig;
    public Transform rockSmall;
    public Transform rockMedium;
    public Transform rockBig;
    public Transform treeSmall;
    public Transform treeMedium;
    public Transform treeBig;
    public Transform cloudSmall;
    public Transform cloudMedium;
    public Transform cloudBig;


    /************* IMPORTANT GAME OBJECTS **************/
    // Warping areas
    private Transform startArea;
    private Transform selectablesFolder;
    // Layers
    private Vector3 groundLayer1;
    private Vector3 groundLayer2;
    private Vector3 groundLayer3;
    private Vector3 airLayer1;
    private Vector3 airLayer2;
    private Vector3 airLayer3;


    /************* PRIVATE VARIABLES **************/
    // Timers
    private float timerRandomSpawn;
    private float timerLayer1;
    private float timerLayer2;
    private float timerLayer3;
    private float timerLayerAir1;


	// Use this for initialization
	void Start ()
    {
        // Important game objects
        startArea = transform.FindChild("StartArea");
        selectablesFolder = transform.FindChild("Selectables");
        Transform layers = transform.FindChild("Layers");
        groundLayer1 = layers.FindChild("GroundLayer1").transform.position;
        groundLayer2 = layers.FindChild("GroundLayer2").transform.position;
        groundLayer3 = layers.FindChild("GroundLayer3").transform.position;
        airLayer1 = layers.FindChild("AirLayer1").transform.position;
        airLayer2 = layers.FindChild("AirLayer2").transform.position;
        airLayer3 = layers.FindChild("AirLayer3").transform.position;

        // Init
        timerRandomSpawn = 0f;
        timerLayer1 = 0f;
        timerLayer2 = 0f;
        timerLayer3 = 0f;
        timerLayerAir1 = 0f;
	}

	// Update is called once per frame
	void Update ()
    {
        // TODO REMOVE for build
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

        // Main timer for randomization
        timerRandomSpawn += Time.deltaTime;

        // Timer between spawn on layers
        timerLayer1 += Time.deltaTime;
        timerLayer2 += Time.deltaTime;
        timerLayer3 += Time.deltaTime;
        timerLayerAir1 += Time.deltaTime;

        // Check every second if a new spawn can happen
        if (timerRandomSpawn >= 1f)
        {
            // Spawn layer ground 1
            if (timerLayer1 >= minTimeBetweenSpawn)
            {
                if (Random.value >= (maxTimeBetweenSpawn - timerLayer1) / maxTimeBetweenSpawn)
                {
                    Transform selectable = spawnSelectable(groundLayer1);
                    selectable.parent = selectablesFolder;
                    timerLayer1 = 0f;
                }
            }
            // Spawn layer ground 2
            if (timerLayer2 >= minTimeBetweenSpawn)
            {
                if (Random.value >= (maxTimeBetweenSpawn - timerLayer2) / maxTimeBetweenSpawn)
                {
                    Transform selectable = spawnSelectable(groundLayer2);
                    selectable.parent = selectablesFolder;
                    timerLayer2 = 0f;
                }
            }
            // Spawn layer ground 3
            if (timerLayer3 >= minTimeBetweenSpawn)
            {
                if (Random.value >= (maxTimeBetweenSpawn - timerLayer3) / maxTimeBetweenSpawn)
                {
                    Transform selectable = spawnSelectable(groundLayer3);
                    selectable.parent = selectablesFolder;
                    timerLayer3 = 0f;
                }
            }

            // Spawn layer air 1
            if (timerLayerAir1 >= minTimeBetweenCloudSpawn)
            {
                if (Random.value >= (maxTimeBetweenCloudSpawn - timerLayerAir1) / maxTimeBetweenCloudSpawn)
                {
                    // Select air layer
                    Vector3 layerAir = airLayer1;
                    int layerAirIndex = Random.Range(0, 3);
                    if (layerAirIndex == 1)
                    {
                        layerAir = airLayer2;
                    }
                    else if (layerAirIndex == 2)
                    {
                        layerAir = airLayer3;
                    }

                    Transform selectable = spawnSelectableAir(layerAir);
                    selectable.parent = selectablesFolder;
                    timerLayerAir1 = 0f;
                }
            }

            timerRandomSpawn = 0f;
        }
	}

    /// <summary>
    /// Spawn a selectable and return the transform of the object.
    /// </summary>
    /// <returns></returns>
    private Transform spawnSelectable (Vector3 layer)
    {
        // Select type
        Transform smallPrefab = buildingSmall;
        Transform mediumPrefab = buildingMedium;
        Transform bigPrefab = buildingBig;
        int spawnType = Random.Range(0, 3);
        switch (spawnType)
        {
            case 0:
                smallPrefab = buildingSmall;
                mediumPrefab = buildingMedium;
                bigPrefab = buildingBig;
                break;
            case 1:
                smallPrefab = mountainSmall;
                mediumPrefab = mountainMedium;
                bigPrefab = mountainBig;
                break;
            case 2:
                smallPrefab = treeSmall;
                mediumPrefab = treeMedium;
                bigPrefab = treeBig;
                break;
        }

        // Select size
        Transform prefab = smallPrefab;
        /*int size = Random.Range(0, 6);
        if (size >= 3 && size < 5)
        {
            prefab = mediumPrefab;
        }
        else if (size == 5)
        {
            prefab = bigPrefab;
        }*/

        // Spawn the selectable
        Transform selectable = Instantiate(prefab) as Transform;
        selectable.position = new Vector3(startArea.position.x, layer.y, layer.z);
        
        // Adapt to the inclination of the ground
        selectable.Rotate(selectable.right, 4f, Space.World);

        // Randomize the rotation
        selectable.Rotate(selectable.up, Random.rotation.eulerAngles.y, Space.World);

        // Set speed
        selectable.GetComponent<Selectable>().speed = selectableSpeed;

        return selectable;
    }

    /// <summary>
    /// Spawn a selectable in on of the air layer and return the transform of the object.
    /// </summary>
    /// <returns></returns>
    private Transform spawnSelectableAir(Vector3 layer)
    {
        // Select size
        Transform prefab = cloudSmall;
        /*int size = Random.Range(0, 6);
        if (size >= 3 && size < 5)
        {
            prefab = cloudMedium;
        }
        else if (size == 5)
        {
            prefab = cloudBig;
        }*/

        // Spawn the selectable
        Transform selectable = Instantiate(prefab) as Transform;
        selectable.position = new Vector3(startArea.position.x, layer.y, layer.z);

        // Randomize the rotation
        selectable.Rotate(selectable.up, Random.rotation.eulerAngles.y, Space.World);

        // Set speed
        selectable.GetComponent<Selectable>().speed = Random.Range(minCloudSpeed, maxCloudSpeed);

        return selectable;
    }

    public void spawnBiggerSelectable (GameObject selectable1, GameObject selectable2, Types type, Sizes size)
    {
        Transform newSelectablePrefab = buildingBig;
        switch (type)
        {
            case Types.Building:
                if (size == Sizes.Small)
                {
                    newSelectablePrefab = buildingMedium;
                }
                else if (size == Sizes.Medium)
                {
                    newSelectablePrefab = buildingBig;
                }
                break;
            case Types.Tree:
                if (size == Sizes.Small)
                {
                    newSelectablePrefab = treeMedium;
                }
                else if (size == Sizes.Medium)
                {
                    newSelectablePrefab = treeBig;
                }
                break;
            case Types.Mountain:
                if (size == Sizes.Small)
                {
                    newSelectablePrefab = mountainMedium;
                }
                else if (size == Sizes.Medium)
                {
                    newSelectablePrefab = mountainBig;
                }
                break;
            case Types.Cloud:
                if (size == Sizes.Small)
                {
                    newSelectablePrefab = cloudMedium;
                }
                else if (size == Sizes.Medium)
                {
                    newSelectablePrefab = cloudBig;
                }
                break;
        }
        Transform newSelectable = Instantiate(newSelectablePrefab) as Transform;
        newSelectable.position = new Vector3(selectable1.transform.position.x + Mathf.Abs(selectable1.transform.position.x - selectable2.transform.position.x) / 2, selectable1.transform.position.y, selectable1.transform.position.z);

        // Adapt to the inclination of the ground
        newSelectable.Rotate(newSelectable.right, 4f, Space.World);
        // Randomize the rotation
        newSelectable.Rotate(newSelectable.up, Random.rotation.eulerAngles.y, Space.World);

        // Destroy the old ones
        Destroy(selectable1);
        Destroy(selectable2);
    }
}
