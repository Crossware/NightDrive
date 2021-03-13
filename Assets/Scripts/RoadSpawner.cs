using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    private Vector3 topPosition = new Vector3(0.0f, -1.4f, 459.0f);
    private float speed = 3.0f;
    public GameObject roadObject;
    private List<GameObject> objectList = new List<GameObject>();
    private float roadTileSize = 153.0f;
    private float yOffset = 0.05f;

    // Start is called before the first frame update
    void Start(){
        //Look at the FrameRateCounter class for how it's doing the UI elements
        for (int i = 0; i < 4; i++){
            decimal listPos = i;
            if (listPos / 2 == 0 || listPos / 2 == 1){
                //Debug.Log("0 or 1");
                yOffset = 0.01f;
            }
            else{
                //Debug.Log("other");
                yOffset = -0.01f;
            }
            GameObject road = Instantiate(roadObject) as GameObject;
            Vector3 positionWithOffset = new Vector3(topPosition.x, topPosition.y + yOffset, topPosition.z);
            road.transform.position = positionWithOffset;
            objectList.Add(road);
            topPosition.z -= roadTileSize;
        }
        topPosition = new Vector3(0.0f, -1.4f, 459.0f);
    }

    // Update is called once per frame
    void FixedUpdate(){
        foreach (GameObject obj in objectList){
            Vector3 currentPosition = obj.transform.position;
            currentPosition.z -= speed;
            obj.transform.position = currentPosition;
            if (obj.transform.position.z <= -roadTileSize){
                decimal listPos = objectList.IndexOf(obj);
                if (listPos / 2 == 0 || listPos / 2 == 1){
                    //Debug.Log("0 or 1");
                    yOffset = 0.01f;
                }
                else{
                    //Debug.Log("other");
                    yOffset = -0.01f;
                }
                Vector3 positionWithOffset = new Vector3(topPosition.x, topPosition.y + yOffset, topPosition.z);
                obj.transform.position = positionWithOffset;
            }
        }
    }
}
