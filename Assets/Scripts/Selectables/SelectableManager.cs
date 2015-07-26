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
    public float minTimeBetweenLayer1Spawn;
    public float maxTimeBetweenLayer1Spawn;
    public float minTimeBetweenLayer2Spawn;
    public float maxTimeBetweenLayer2Spawn;
    public float minTimeBetweenLayer3Spawn;
    public float maxTimeBetweenLayer3Spawn;
    public float groundSpeed;
    public float mountainSpeed;
    public float minCloudSpeed;
    public float maxCloudSpeed;
    public float minTimeBetweenCloudSpawn;
    public float maxTimeBetweenCloudSpawn;
    public float minTimeBetweenMountainSpawn;
    public float maxTimeBetweenMountainSpawn;

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
    // Warping area
    private Transform selectablesFolder;
    // Layers
    private Vector3 groundLayer1;
    private Vector3 groundLayer2;
    private Vector3 groundLayer3;
    private Vector3 airLayer1;
    private Vector3 airLayer2;
    private Vector3 mountainLayer;


    /************* PRIVATE VARIABLES **************/
    // Timers
    private float timerRandomSpawn;
    private float timerLayer1;
    private float timerLayer2;
    private float timerLayer3;
    private float timerAirLayer;
    private float timerMountainLayer;


	// Use this for initialization
	void Start ()
    {
        // Important game objects
        selectablesFolder = transform.FindChild("Selectables");
        Transform layers = transform.FindChild("Layers");
        groundLayer1 = layers.FindChild("GroundLayer1").transform.position;
        groundLayer2 = layers.FindChild("GroundLayer2").transform.position;
        groundLayer3 = layers.FindChild("GroundLayer3").transform.position;
        airLayer1 = layers.FindChild("AirLayer1").transform.position;
        airLayer2 = layers.FindChild("AirLayer2").transform.position;
        mountainLayer = layers.FindChild("MountainLayer").transform.position;

        // Init
        timerRandomSpawn = 0f;
        timerLayer1 = 0f;
        timerLayer2 = 0f;
        timerLayer3 = 0f;
        timerAirLayer = 0f;
        timerMountainLayer = 0f;
	}

	// Update is called once per frame
	void Update ()
    {
        // TODO REMOVE for build
        /*if (Input.GetKeyDown(KeyCode.O))
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
        }*/

        // Main timer for randomization
        timerRandomSpawn += Time.deltaTime;

        // Timer between spawn on layers
        timerLayer1 += Time.deltaTime;
        timerLayer2 += Time.deltaTime;
        timerLayer3 += Time.deltaTime;
        timerAirLayer += Time.deltaTime;
        timerMountainLayer += Time.deltaTime;

        // Check every second if a new spawn can happen
        if (timerRandomSpawn >= 1f)
        {
            // Spawn layer ground 1
            if (timerLayer1 >= minTimeBetweenLayer1Spawn)
            {
                if (Random.value >= (maxTimeBetweenLayer1Spawn - timerLayer1) / maxTimeBetweenLayer1Spawn)
                {
                    Transform selectable = spawnGroundSelectable(groundLayer1);
                    selectable.parent = selectablesFolder;
                    timerLayer1 = 0f;
                }
            }
            // Spawn layer ground 2
            if (timerLayer2 >= minTimeBetweenLayer2Spawn)
            {
                if (Random.value >= (maxTimeBetweenLayer2Spawn - timerLayer2) / maxTimeBetweenLayer2Spawn)
                {
                    Transform selectable = spawnGroundSelectable(groundLayer2);
                    selectable.parent = selectablesFolder;
                    timerLayer2 = 0f;
                }
            }
            // Spawn layer ground 3
            if (timerLayer3 >= minTimeBetweenLayer3Spawn)
            {
                if (Random.value >= (maxTimeBetweenLayer3Spawn - timerLayer3) / maxTimeBetweenLayer3Spawn)
                {
                    Transform selectable = spawnGroundSelectable(groundLayer3);
                    selectable.parent = selectablesFolder;
                    timerLayer3 = 0f;
                }
            }

            // Spawn layer air 1 and 2
            if (timerAirLayer >= minTimeBetweenCloudSpawn)
            {
                if (Random.value >= (maxTimeBetweenCloudSpawn - timerAirLayer) / maxTimeBetweenCloudSpawn)
                {
                    // Select air layer
                    Vector3 layerAir = airLayer1;
                    int layerAirIndex = Random.Range(0, 2);
                    if (layerAirIndex == 1)
                    {
                        layerAir = airLayer2;
                    }

                    Transform selectable = spawnAirSelectable(layerAir);
                    selectable.parent = selectablesFolder;
                    timerAirLayer = 0f;
                }
            }

            // Spawn mountain layer
            if (timerMountainLayer >= minTimeBetweenMountainSpawn)
            {
                if (Random.value >= (maxTimeBetweenMountainSpawn - timerMountainLayer) / maxTimeBetweenMountainSpawn)
                {
                    Transform selectable = spawnMountainSelectable(mountainLayer);
                    selectable.parent = selectablesFolder;
                    timerMountainLayer = 0f;
                }
            }

            timerRandomSpawn = 0f;
        }
	}

    /// <summary>
    /// Spawn a selectable and return the transform of the object.
    /// </summary>
    /// <returns></returns>
    private Transform spawnGroundSelectable (Vector3 layer)
    {
        // Select type
        Transform prefab = buildingSmall;
        int spawnType = Random.Range(0, 3);
        switch (spawnType)
        {
            case 0:
                prefab = buildingSmall;
                break;
            case 1:
                prefab = rockSmall;
                break;
            case 2:
                prefab = treeSmall;
                break;
        }

        // Spawn the selectable
        Transform selectable = Instantiate(prefab) as Transform;
        selectable.position = new Vector3(layer.x, layer.y, layer.z);

        initGroundSelectable(selectable);

        return selectable;
    }

    /// <summary>
    /// Spawn a selectable in on of the air layer and return the transform of the object.
    /// </summary>
    /// <returns></returns>
    private Transform spawnAirSelectable(Vector3 layer)
    {
        // Spawn the cloud
        Transform selectable = Instantiate(cloudSmall) as Transform;
        selectable.position = new Vector3(layer.x, layer.y, layer.z);

        initAirSelectable(selectable);

        return selectable;
    }

    /// <summary>
    /// Spawn a mountain in on of the mountain layer and return the transform of the object.
    /// </summary>
    /// <returns></returns>
    private Transform spawnMountainSelectable(Vector3 layer)
    {
        // Spawn the cloud
        Transform selectable = Instantiate(mountainSmall) as Transform;
        selectable.position = new Vector3(layer.x, layer.y, layer.z);

        initMountainSelectable(selectable);

        return selectable;
    }

    private void initGroundSelectable(Transform selectable)
    {
        // Adapt to the inclination of the ground
        selectable.Rotate(selectable.right, 7f, Space.World);

        // Randomize the rotation
        selectable.Rotate(selectable.up, Random.rotation.eulerAngles.y, Space.World);

        // Set speed
        selectable.GetComponent<Selectable>().speed = groundSpeed;
    }

    private void initMountainSelectable(Transform selectable)
    {
        // Adapt to the inclination of the ground
        selectable.Rotate(selectable.right, 16f, Space.World);

        // Randomize the rotation
        selectable.Rotate(selectable.up, Random.rotation.eulerAngles.y, Space.World);

        // Set speed
        selectable.GetComponent<Selectable>().speed = mountainSpeed;
    }

    private void initAirSelectable(Transform selectable)
    {
        // Randomize the rotation
        selectable.Rotate(selectable.up, Random.rotation.eulerAngles.y, Space.World);

        // Set speed
        selectable.GetComponent<Selectable>().speed = Random.Range(minCloudSpeed, maxCloudSpeed);
    }

    /// <summary>
    /// Merge two selectables together to make a bigger one.
    /// </summary>
    /// <param name="selectable1"></param>
    /// <param name="selectable2"></param>
    /// <param name="type"></param>
    /// <param name="size"></param>
    public void mergeSelectables (GameObject selectable1, GameObject selectable2, Types type, Sizes size)
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
            case Types.Rock:
                if (size == Sizes.Small)
                {
                    newSelectablePrefab = rockMedium;
                }
                else if (size == Sizes.Medium)
                {
                    newSelectablePrefab = rockBig;
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
        GameObject selectable = selectable1;
        if (selectable2.GetComponent<Selectable>().selected)
        {
            selectable = selectable2;
        }

        newSelectable.position = new Vector3(selectable1.transform.position.x + Mathf.Abs(selectable1.transform.position.x - selectable2.transform.position.x) / 2, selectable.transform.position.y, selectable.transform.position.z);
        newSelectable.parent = selectablesFolder;

        // Init
        newSelectable.GetComponent<Selectable>().haveBeenMerged = true;
        newSelectable.gameObject.layer = LayerMask.NameToLayer("Selected");
        if (type == Types.Cloud)
        {
            initAirSelectable(newSelectable);
        }
        else if (type == Types.Mountain)
        {
            initMountainSelectable(newSelectable);
        }
        else
        {
            initGroundSelectable(newSelectable);
        }

        // Destroy the old ones
        Destroy(selectable1);
        Destroy(selectable2);
    }
}
