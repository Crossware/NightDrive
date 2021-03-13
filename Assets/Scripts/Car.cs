using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car
{
    public GameObject carObject { get; set; }
    public Vector3 Position { get; set; }
    public float DespawnPosititon { get; set; }
    public float Speed { get; set; }
    private float originalSpeed;
    public bool deleted { get; set; }

    private bool wasRoadBlock = false;
    private GameObject originalCarModel;
    private GameObject roadBlock = GameObject.Find("Road_Block");

    public Car(GameObject car, Vector3 spawnPosition, float despawnPosition, float speed){
        this.Position = spawnPosition;
        this.DespawnPosititon = despawnPosition;
        this.Speed = speed;
        this.originalSpeed = speed;
        this.carObject = car;
        this.originalCarModel = car;
    }

    public void spawn(){
        carObject.transform.position = Position;
    }

    public void respawn(Vector3 position){
        /*
        if (wasRoadBlock){
            carObject = originalCarModel;
           
            Speed = originalSpeed;
            wasRoadBlock = false;
            return;
        }
        if (!wasRoadBlock && shouldSpawnRoadBlock()){
            swapToRoadBlock(position);
        }
        */
        carObject.transform.position = position;
        carObject.GetComponent<Collider>().enabled = true;
    }

    public void update(){
        Vector3 currentPosition = carObject.transform.position;
        currentPosition.z -= Speed;
        carObject.transform.position = currentPosition;
        if (currentPosition.z < DespawnPosititon){
            respawn(Position);
        }
    }

    private bool shouldSpawnRoadBlock(){
        int spawnChance = Random.Range(0, 100);
        if(spawnChance < 100 / 5){
            return true;
        }
        return false;
    }

    public void swapToRoadBlock(Vector3 position){
        carObject = roadBlock;
        Speed = 0.5f;
        carObject.transform.position = position;
        carObject.GetComponent<Collider>().enabled = true;
        wasRoadBlock = true;
    }
}
