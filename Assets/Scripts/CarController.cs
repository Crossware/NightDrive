using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CarController : MonoBehaviour
{
    private float fuel = 100.0f;
    private float fuelCost = 0.05f;
    private float fuelAdded = 20.0f;
    private float health = 100.0f;
    private float healthCost = 10.0f;
    private float healthAdded = 10.0f;
    private float cap = 100.0f;
    private float distanceTravelled = 0.0f;
    private GameObject player;
    public AudioClip itemPickup;
    public AudioClip carHit;
    public AudioSource collectableSound;
    public TextMeshProUGUI healthInfo;
    public TextMeshProUGUI fuelInfo;
    public TextMeshProUGUI scoreInfo;
    public Image healthCar;

    //If you instantiate a private variable you don't need to give it a value in start
    //If it's public you need to give it a value in start
    private float speed = 0.5f;
    // Start is called before the first frame update
    void Start(){
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
            health -= healthCost;
        }
        if (other.collider.tag == "Gas"){
            Debug.Log("Hit Gas");
            collectableSound.PlayOneShot(itemPickup);
            addOn(fuelAdded, ref fuel, cap);
            colliderObject.SetActive(false);
        }
        if (other.collider.tag == "Wrench"){
            Debug.Log("Hit Wrench");
            collectableSound.PlayOneShot(itemPickup);
            addOn(healthAdded, ref health, cap);
            colliderObject.SetActive(false);
        }

    }

    /*
        Image myImage = Instantiate(healthCar);
        Vector3 imagePos = new Vector3(myImage.transform.position.x, myImage.transform.position.y - 30.0f, myImage.transform.position.z);
        myImage.transform.position = imagePos;
     */

    // Update is called once per frame
    void FixedUpdate(){
        player.GetComponent<Rigidbody>().WakeUp();
        fuel -= fuelCost;
        distanceTravelled += 1.0f;
        updateUI();
        checkIfLost();
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

    void addOn(float howMuchToAdd, ref float numberToAddTo, float cap){
        if(numberToAddTo + howMuchToAdd >= cap){
            numberToAddTo = cap;
        }
        else{
            numberToAddTo += howMuchToAdd;
        }
    }

    void checkIfLost(){
        //Debug.Log("Health: " + health);
        //Debug.Log("Fuel: " + fuel);
        if(health <= 0 || fuel <= 0){
            PlayerPrefs.SetFloat("score", distanceTravelled);
            SceneManager.LoadScene("Menu Lose");
        }
    }

    void updateUI(){
        healthInfo.text = "Health: " + health + "/" + cap;
        fuelInfo.text = "Fuel: " + fuel.ToString("F0") + "/" + cap;
        scoreInfo.text = "Distance Travelled: " + distanceTravelled.ToString("F0");
    }
}
