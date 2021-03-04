using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject player;

    //If you instantiate a private variable you don't need to give it a value in start
    //If it's public you need to give it a value in start
    private float speed = 0.5f;
    // Start is called before the first frame update
    void Start(){
        Vector3 spawnPosition = new Vector3(-1.0f, 0.0f, -65.69f);
        player = GameObject.Find("Player");
        player.transform.position = spawnPosition;
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
            player.transform.Translate(-speed, 0f, 0f);
            //player.transform.Rotate(Vector3.up, -10);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            player.transform.Translate(speed, 0f, 0f);
            //player.transform.Rotate(Vector3.up, 10);
        }

    }
}
