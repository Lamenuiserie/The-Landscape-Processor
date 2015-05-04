using UnityEngine;
using System.Collections;

public class GroundManager : MonoBehaviour
{
    /// <summary>
    /// Speed at which the pieces of grounds go.
    /// </summary>
    public float speed;

    private Transform groundEnd;
    private Transform groundsFolder;
    private Transform[] grounds;

    private Transform hiddenStart;
    private Transform hiddenEnd;

    /// <summary>
    /// Hidden objects for shadows.
    /// </summary>
    private Transform hiddens;


	// Use this for initialization
	void Start ()
    {
        groundEnd = transform.FindChild("GroundEnd");
        groundsFolder = transform.FindChild("Grounds");
        grounds = groundsFolder.GetComponentsInChildren<Transform>();
        hiddenStart = transform.FindChild("HiddenStart");
        hiddenEnd = transform.FindChild("HiddenEnd");
        hiddens = transform.FindChild("Hiddens");
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Move hidden objects
        for (int i = 0; i < hiddens.childCount; i++)
        {
            Transform hidden = hiddens.GetChild(i);
            if (hidden.position.x >= hiddenEnd.position.x)
            {
                hidden.position = new Vector3(hiddenStart.position.x, hidden.position.y, hidden.position.z);
            }
            else
            {
                hidden.Translate(hidden.right * speed * Time.deltaTime);
            }
        }

        // Move ground elements
        Transform mostRight = findMostRight();
        if (mostRight.position.x >= groundEnd.position.x)
        {
            Transform mostLeft = findMostLeft();
            for (int i = 1; i < grounds.Length; i++)
            {
                if (grounds[i].position.x >= groundEnd.position.x)
                {
                    grounds[i].position = new Vector3(mostLeft.position.x - 168f, grounds[i].localPosition.y, grounds[i].localPosition.z);
                }
            }
        }
        else
        {
            groundsFolder.Translate(groundsFolder.right * speed * Time.deltaTime);
        }
	}

    private Transform findMostLeft ()
    {
        Vector3 mostLeftPosition = new Vector3(99999f, 0f, 0f);
        Transform mostLeft = null;
        for (int i = 1; i < grounds.Length; i++)
        {
            if (grounds[i].position.x < mostLeftPosition.x)
            {
                mostLeft = grounds[i];
                mostLeftPosition = grounds[i].position;
            }
        }

        return mostLeft;
    }


    private Transform findMostRight()
    {
        Vector3 mostRightPosition = new Vector3(-99999f, 0f, 0f);
        Transform mostRight = null;
        for (int i = 1; i < grounds.Length; i++)
        {
            if (grounds[i].position.x > mostRightPosition.x)
            {
                mostRight = grounds[i];
                mostRightPosition = grounds[i].position;
            }
        }

        return mostRight;
    }
}
