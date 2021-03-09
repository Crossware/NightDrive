using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float fuel;
    private float fuelCost;
    private float fuelAdded;
    private float health;
    private float healthCost;
    private float healthAdded;
    private GameObject player;
    public AudioClip itemPickup;
    public AudioClip carHit;
    public AudioSource collectableSound;

    //If you instantiate a private variable you don't need to give it a value in start
    //If it's public you need to give it a value in start
    private float speed = 0.5f;
    // Start is called before the first frame update
    void Start(){
        fuel = 100.0f;
        fuelCost = 0.10f;
        fuelAdded = 10.0f;
        health = 100.0f;
        healthAdded = 10.0f;

        Vector3 spawnPosition = new Vector3(-1.0f, 0.10f, -65.69f);
        player = GameObject.Find("Player");
        player.transform.position = spawnPosition;
    }

    void OnCollisionEnter(Collision other){
        //Debug.Log("Called1");
        GameObject colliderObject = other.gameObject;
        other.collider.enabled = false;
        if (other.collider.tag == "Car"){
            collectableSound.PlayOneShot(carHit);
            Debug.Log("Hit Car " + colliderObject.name);
            health -= 10.0f;
        }
        if (other.collider.tag == "Gas"){
            Debug.Log("Hit Gas");
            collectableSound.PlayOneShot(itemPickup);
            addOn(fuelAdded, fuel, 100.0f);
            colliderObject.SetActive(false);
        }
        if (other.collider.tag == "Wrench"){
            Debug.Log("Hit Wrench");
            collectableSound.PlayOneShot(itemPickup);
            addOn(healthAdded, health, 100.0f);
            colliderObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void FixedUpdate(){
        player.GetComponent<Rigidbody>().WakeUp();
        //Debug.Log("Health: " + health);
        //Debug.Log("Fuel: " + fuel);
        fuel -= fuelCost;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
            if(player.transform.position.x >= -23.0f){
                player.transform.Translate(-speed, 0f, 0f);
            }
            //player.transform.Rotate(Vector3.up, -10);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            if(player.transform.position.x <= 22.0f){
                player.transform.Translate(speed, 0f, 0f);
            }
            //player.transform.Rotate(Vector3.up, 10);
        }

    }

    void addOn(float howMuchToAdd, float numberToAddTo, float cap)
    {
        if(numberToAddTo + howMuchToAdd > cap){
            numberToAddTo = cap;
        }
        else{
            numberToAddTo += numberToAddTo;
        }
    }
}
