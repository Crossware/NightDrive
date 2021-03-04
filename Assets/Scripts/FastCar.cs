using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastCar : MonoBehaviour, Car
{
    public GameObject carObject { get; set; }
    public Vector3 Position { get; set; }
    public float DespawnPosititon { get; set; }
    public float Speed { get; set; }
    public bool deleted { get; set; }


    public FastCar(Vector3 spawnPosition, float despawnPosition, float speed){
        this.Position = spawnPosition;
        this.DespawnPosititon = despawnPosition;
        this.Speed = speed;
    }

    public void spawn(){
        carObject = Instantiate(GameObject.Find("4x4 pickup")) as GameObject;
        carObject.transform.position = Position;
    }

    public void spawn(Vector3 position){
        //carObject = Instantiate(GameObject.Find("4x4 pickup")) as GameObject;
        carObject.transform.position = position;
    }

    public void despawn(){
        Destroy(carObject);
    }

    public void update(){
        //Debug.Log("Car object: " + carObject);
        Vector3 currentPosition = carObject.transform.position;
        currentPosition.z -= Speed;
        carObject.transform.position = currentPosition;
        if (currentPosition.z < DespawnPosititon)
        {
            //Debug.Log("Position Z: " + Position.z);
            spawn(Position);
        }
    }
}
