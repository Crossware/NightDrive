using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCar : MonoBehaviour, Car
{
    public GameObject carObject { get; set; }
    public Vector3 Position { get; set; }
    public float DespawnPosititon { get; set; }
    public float Speed { get; set; }
    public bool deleted { get; set; }

    public SlowCar(Vector3 spawnPosition, float despawnPosition, float speed){
        this.Position = spawnPosition;
        this.DespawnPosititon = despawnPosition;
        this.Speed = speed;
    }

    public SlowCar(GameObject car, Vector3 spawnPosition, float despawnPosition, float speed){
        this.Position = spawnPosition;
        this.DespawnPosititon = despawnPosition;
        this.Speed = speed;
        this.carObject = car;
    }

    public void spawn(){
        carObject.transform.position = Position;
    }

    public void respawn(Vector3 position){
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
}
