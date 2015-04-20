using UnityEngine;
using System.Collections;

public class SelectableManager : MonoBehaviour
{
    /************* PUBLIC VARIABLES **************/
    // Procedural spawning of selectables
    public float minTimeBetweenSpawn;
    public float maxTimeBetweenSpawn;
    public float selectableSpeed;
    public float minTimeBetweenCloudSpawn;
    public float maxTimeBetweenCloudSpawn;
    public float minCloudSpeed;
    public float maxCloudSpeed;

    // Layers
    public Vector3 layer1;
    public Vector3 layer2;
    public Vector3 layer3;
    public Vector3 layerAir1;
    public Vector3 layerAir2;
    public Vector3 layerAir3;

    // Prefabs
    public Transform buildingSmall;
    public Transform buildingMedium;
    public Transform buildingBig;
    public Transform mountainSmall;
    public Transform mountainMedium;
    public Transform mountainBig;
    public Transform tree;
    public Transform cloud;
    public Transform road;
    public Transform pylon;


    /************* IMPORTANT GAME OBJECTS **************/
    // Warping areas
    private Transform startArea;
    private Transform selectablesFolder;


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
                    Transform selectable = spawnSelectable(layer1);
                    selectable.parent = selectablesFolder;
                    timerLayer1 = 0f;
                }
            }
            // Spawn layer ground 2
            if (timerLayer2 >= minTimeBetweenSpawn)
            {
                if (Random.value >= (maxTimeBetweenSpawn - timerLayer2) / maxTimeBetweenSpawn)
                {
                    Transform selectable = spawnSelectable(layer2);
                    selectable.parent = selectablesFolder;
                    timerLayer2 = 0f;
                }
            }
            // Spawn layer ground 3
            if (timerLayer3 >= minTimeBetweenSpawn)
            {
                if (Random.value >= (maxTimeBetweenSpawn - timerLayer3) / maxTimeBetweenSpawn)
                {
                    Transform selectable = spawnSelectable(layer3);
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
                    Vector3 layerAir = layerAir1;
                    int layerAirIndex = Random.Range(0, 3);
                    if (layerAirIndex == 1)
                    {
                        layerAir = layerAir2;
                    }
                    else if (layerAirIndex == 2)
                    {
                        layerAir = layerAir3;
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
                smallPrefab = tree;
                mediumPrefab = tree;
                bigPrefab = tree;
                break;
        }

        // Select size
        Transform prefab = smallPrefab;
        int size = Random.Range(0, 6);
        if (size >= 3 && size < 5)
        {
            prefab = mediumPrefab;
        }
        else if (size == 5)
        {
            prefab = bigPrefab;
        }

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
        Transform prefab = cloud;
        int size = Random.Range(0, 6);
        if (size >= 3 && size < 5)
        {
            prefab = cloud;
        }
        else if (size == 5)
        {
            prefab = cloud;
        }

        // Spawn the selectable
        Transform selectable = Instantiate(prefab) as Transform;
        selectable.position = new Vector3(startArea.position.x, layer.y, layer.z);

        // Randomize the rotation
        selectable.Rotate(selectable.up, Random.rotation.eulerAngles.y, Space.World);

        // Set speed
        selectable.GetComponent<Selectable>().speed = Random.Range(minCloudSpeed, maxCloudSpeed);

        return selectable;
    }
}
