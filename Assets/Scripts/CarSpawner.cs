using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour{
    private bool startGame = false;
    private bool allCarsSpawned = false;

    private int numberOfLanes = 3;
    public GameObject carObject;
    private List<Car> cars = new List<Car>();
    private int minGapTime = 10;
    private int maxGapTime = 20;
    private int gapOffset = 0;
    private int maxCarsPerLane = 2;

    private List<Car> leftLane = new List<Car>();
    private List<Car> middleLane = new List<Car>();
    private List<Car> rightLane = new List<Car>();

    private List<GameObject> leftLanePool = new List<GameObject>();
    private List<GameObject> middleLanePool = new List<GameObject>();
    private List<GameObject> rightLanePool = new List<GameObject>();

    private Vector3 leftLanePosition = new Vector3(-18.0f, 0.10f, 559.0f);
    private Vector3 middleLanePosition = new Vector3(-1.0f, 0.10f, 459.0f);
    private Vector3 rightLanePosition = new Vector3(15.0f, 0.10f, 409.0f);

    private float leftLaneSpeed = 2.5f;         //Prior: 1.0f
    private float middleLaneSpeed = 2.5f;       //Prior: 0.5f
    private float rightLaneSpeed = 2.5f;        //Prior: 2.0f

    private double leftLaneGap = 10;
    private double minLeftLaneGap = 20;

    private double middleLaneGap = 15;
    private double minMiddleLaneGap = 70;

    private double rightLaneGap = 20;
    private double minRightLaneGap = 25;

    private float despawnPosition = -153.0f;

    // Start is called before the first frame update
    void Start(){
        GameObject leftCar = GameObject.Find("4x4");
        GameObject middleCar = GameObject.Find("Bus");
        GameObject rightCar = GameObject.Find("Red 18 Wheeler");

        leftLanePool.Add(leftCar);
        leftLanePool.Add(GameObject.Find("Red Truck"));

        middleLanePool.Add(middleCar);
        middleLanePool.Add(GameObject.Find("4x4 (1)"));

        rightLanePool.Add(rightCar);
        rightLanePool.Add(GameObject.Find("4x4 (2)"));

        Car car1 = new Car(leftCar, leftLanePosition, despawnPosition, leftLaneSpeed);
        car1.spawn();
        cars.Add(car1);
        leftLane.Add(car1);

        Vector3 adjustedBusPos = new Vector3(middleLanePosition.x, 1.6f, middleLanePosition.z);
        Car car2 = new Car(middleCar, adjustedBusPos, despawnPosition, middleLaneSpeed);
        car2.spawn();
        cars.Add(car2);
        middleLane.Add(car2);

        Car car3 = new Car(rightCar, rightLanePosition, despawnPosition, rightLaneSpeed);
        car3.spawn();
        cars.Add(car3);
        rightLane.Add(car3);
    }

    void FixedUpdate(){
        if(cars.Count < maxCarsPerLane * numberOfLanes){
            leftLaneGap += 0.1;
            middleLaneGap += 0.1;
            rightLaneGap += 0.1;
            gapOffset = Random.Range(minGapTime, maxGapTime);
            if (leftLane.Count < maxCarsPerLane){
                if (leftLaneGap > minLeftLaneGap + gapOffset){
                    Car car = new Car(leftLanePool[leftLane.Count], leftLanePosition, despawnPosition, leftLaneSpeed);
                    car.spawn();
                    cars.Add(car);
                    leftLane.Add(car);
                    leftLaneGap = 0;
                }
            }
            if (middleLane.Count < maxCarsPerLane){
                if (middleLaneGap > minMiddleLaneGap + gapOffset){
                    Car car = new Car(middleLanePool[middleLane.Count], middleLanePosition, despawnPosition, middleLaneSpeed);
                    car.spawn();
                    cars.Add(car);
                    middleLane.Add(car);
                    middleLaneGap = 0;
                }
            }
            if (rightLane.Count < maxCarsPerLane){
                if (rightLaneGap > minRightLaneGap + gapOffset){
                    Car car = new Car(rightLanePool[rightLane.Count], rightLanePosition, despawnPosition, rightLaneSpeed);
                    car.spawn();
                    cars.Add(car);
                    rightLane.Add(car);
                    rightLaneGap = 0;
                }
            }
        }
        
        foreach (Car singleCar in cars){
            singleCar.update();
        }
    }
}
