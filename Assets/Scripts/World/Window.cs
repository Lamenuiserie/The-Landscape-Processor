using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour
{
    private float lowestYPosition;
    private float highestYPosition;

    private float startYPosition;
    private float endYPosition;
    private float movementTimer;


	// Use this for initialization
	void Start ()
    {
        movementTimer = 0f;
        startYPosition = transform.position.y;
        endYPosition = startYPosition + 0.1f;
        lowestYPosition = startYPosition;
        highestYPosition = endYPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        movementTimer += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(startYPosition, endYPosition, movementTimer / 0.5f), transform.position.z);

	    if (transform.position.y >= highestYPosition)
        {
            startYPosition = highestYPosition;
            endYPosition = lowestYPosition;
            movementTimer = 0f;
        }
        else if (transform.position.y <= lowestYPosition)
        {
            startYPosition = lowestYPosition;
            endYPosition = highestYPosition;
            movementTimer = 0f;
        }
        
	}
}
