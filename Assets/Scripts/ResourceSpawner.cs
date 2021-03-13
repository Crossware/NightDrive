using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    private GameObject gasCan;
    private GameObject wrench;
    private GameObject reverser;

    private int activeNumber = 1;
    private List<GameObject> objects = new List<GameObject>();
    private List<GameObject> activeObjects = new List<GameObject>();

    private float speed = 3.0f;
    private Vector3 leftLanePosition = new Vector3(-15.0f, 5.0f, 459.0f);
    private Vector3 middleLanePosition = new Vector3(0.0f, 5.0f, 459.0f);
    private Vector3 rightLanePosition = new Vector3(15.0f, 5.0f, 459.0f);
    private Vector3 retirePosition = new Vector3(-30.0f, 0.0f, -425.0f);
    private List<Vector3> startPositions = new List<Vector3>();
    private float despawnPosition = -153.0f;
    private float minSpawnGap = 0.0f;
    private float activeSpawnGap = 0.0f;

    // Start is called before the first frame update
    void Start(){
        startPositions.Add(leftLanePosition);
        startPositions.Add(rightLanePosition);
        startPositions.Add(middleLanePosition);

        gasCan = GameObject.Find("Gas");
        wrench = GameObject.Find("Wrench");
        reverser = GameObject.Find("Reverse Control");
        objects.Add(gasCan);
        objects.Add(wrench);
        objects.Add(reverser);
    }

    // Update is called once per frame
    void FixedUpdate(){
        activeSpawnGap += 0.01f;
        //Debug.Log("Active Objects: " + activeObjects.Count);
        if(activeSpawnGap > minSpawnGap){
            if (activeObjects.Count < activeNumber){
                int lane = Random.Range(0, 3);
                spawn(objects[getNumber(0, 11)], startPositions[lane]);
            }
        }
        
        foreach(GameObject obj in activeObjects.ToArray()){
            Vector3 currentPosition = obj.transform.position;
            currentPosition.z -= speed;
            obj.transform.position = currentPosition;
            if (currentPosition.z < despawnPosition){
                respawn(obj, objects[getNumber(0, 10)]);
            }
        }
    }

    int getNumber(int min, int max){
        int objectNumber = Random.Range(min, max);
        if(objectNumber < max / 2){
            return 0;
        }
        else{
            int random = Random.Range(0, 10);
            Debug.Log("Random: " + random);
            if (random < max / 2){
                return 1;
            }
            return 2;
        }
    }

    void respawn(GameObject objectToRetire, GameObject newObject){
        objectToRetire.transform.position = retirePosition;
        activeObjects.Remove(objectToRetire);
        int lane = Random.Range(0, 3);
        newObject.SetActive(true);
        newObject.GetComponent<Collider>().enabled = true;
        newObject.transform.position = startPositions[lane];
        activeObjects.Add(newObject);
    }

    void spawn(GameObject obj, Vector3 position){
        obj.SetActive(true);
        activeObjects.Add(obj);
        obj.transform.position = position;
    }
}
