using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour
{
    
    private float startYPosition;
    private float endYPosition;
    private float movementTimer;


	// Use this for initialization
	void Start ()
    {
        movementTimer = 0f;
        startYPosition = 0f;
        endYPosition = 0.1f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        movementTimer += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(startYPosition, endYPosition, movementTimer / 0.5f), transform.position.z);

	    if (transform.position.y >= 0.1f)
        {
            startYPosition = 0.1f;
            endYPosition = 0f;
            movementTimer = 0f;
        }
        else if (transform.position.y <= 0f)
        {
            startYPosition = 0f;
            endYPosition = 0.1f;
            movementTimer = 0f;
        }
        
	}
}
