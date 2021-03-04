using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    private Vector3 topPosition;
    private float speed = 3.0f;
    public GameObject roadObject;
    private List<GameObject> objectList = new List<GameObject>();
    private float roadTileSize = 153.0f;

    // Start is called before the first frame update
    void Start()
    {
        topPosition = new Vector3(0.0f, -1.4f, 306f);
        
        //topPosition = new Vector3(0.0f, 0.0f, 307.2f);
        //middlePosition = new Vector3(0.0f, 0.0f, 153.6f);
        //bottomPosition = new Vector3(0.0f, 0.0f, 0.0f);

        //roadObject = GameObject.Find("Road");
        for (int i = 0; i < 3; i++)
        {
            GameObject road = Instantiate(roadObject) as GameObject;
            road.transform.position = topPosition;
            objectList.Add(road);
            topPosition.z -= roadTileSize;
        }
        topPosition = new Vector3(0.0f, -1.4f, 306.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject obj in objectList)
        {
            Vector3 currentPosition = obj.transform.position;
            currentPosition.z -= speed;
            obj.transform.position = currentPosition;
            if (obj.transform.position.z <= -roadTileSize)
            {
                //Debug.Log("Position Z: " + topPosition.z);
                obj.transform.position = topPosition;
            }
        }
    }
}
