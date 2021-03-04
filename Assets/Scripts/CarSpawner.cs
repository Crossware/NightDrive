using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour{
    private bool startGame = false;
    private int activeCars = 5;
    private int numberOfLanes = 3;
    private float speed = 3.0f;
    public GameObject carObject;
    private List<Car> cars = new List<Car>();
    private int minGapTime = 10;
    private int maxGapTime = 20;
    private int gapOffset = 0;
    private int maxCarsPerLane = 2;
    private List<Car> leftLane = new List<Car>();
    private List<Car> middleLane = new List<Car>();
    private List<Car> rightLane = new List<Car>();

    private Vector3 leftLanePosition = new Vector3(-15.0f, 0.0f, 306.0f);
    private Vector3 middleLanePosition = new Vector3(0.0f, 0.0f, 306.0f);
    private Vector3 rightLanePosition = new Vector3(15.0f, 0.0f, 306.0f);

    private float leftLaneSpeed = 4.0f;
    private float middleLaneSpeed = 2.0f;
    private float rightLaneSpeed = 3.0f;

    private double leftLaneGap = 10;
    private double minLeftLaneGap = 20;

    private double middleLaneGap = 15;
    private double minMiddleLaneGap = 40;

    private double rightLaneGap = 20;
    private double minRightLaneGap = 35;

    private List<Vector3> startPositions = new List<Vector3>();
    private float despawnPosition = -153.0f;

    // Start is called before the first frame update
    void Start(){
        startPositions.Add(leftLanePosition);
        startPositions.Add(middleLanePosition);
        startPositions.Add(rightLanePosition);
        Debug.Log("number of lanes: " + numberOfLanes);
        Debug.Log("max Cars Per Lane: " + maxCarsPerLane);
        int totalNumberOfCars = numberOfLanes * maxCarsPerLane;

        Car car1 = new SlowCar(leftLanePosition, despawnPosition, leftLaneSpeed);
        car1.spawn();
        cars.Add(car1);
        leftLane.Add(car1);

        Car car2 = new ModerateCar(middleLanePosition, despawnPosition, middleLaneSpeed);
        car2.spawn();
        cars.Add(car2);
        middleLane.Add(car2);

        Car car3 = new FastCar(rightLanePosition, despawnPosition, rightLaneSpeed);
        car3.spawn();
        cars.Add(car3);
        rightLane.Add(car3);

        /*
        gapOffset = Random.Range(minGapTime, maxGapTime);
        ModerateCar myCar = new ModerateCar(leftLanePosition, despawnPosition, leftLaneSpeed);
        Debug.Log("Car object: " + myCar.carObject);
        startPositions.Add(leftLanePosition);
        startPositions.Add(middleLanePosition);
        startPositions.Add(rightLanePosition);
        
        for (int i = 0; i < 3; i++)
        {
            GameObject road = Instantiate(roadObject) as GameObject;
            road.transform.position = startPosition;
            objectList.Add(road);
            startPosition.z -= roadTileSize;
        }
        startPosition = new Vector3(0.0f, 0.0f, 307.2f);
        */
        /*
          for (int i = 0; i < numberOfLanes; i++)
        {
            for(int j = 0; j < maxCarsPerLane; j++)
            {
                int lane = Random.Range(0, numberOfLanes);
                Car car = getCarByLane(lane, startPositions[lane], despawnPosition, leftLaneSpeed);
                cars.Add(car.carObject);
                addToLaneList(car.carObject, lane);
            }
        } 
        
        int totalNumberOfCars = numberOfLanes * maxCarsPerLane;
        for (int i = 0; i < totalNumberOfCars; i++){
            int lane = Random.Range(0, numberOfLanes);
            Car car = getCarByLane(lane, startPositions[lane], despawnPosition);
            if(car == null){
                break;
            }
            cars.Add(car);
            addToLaneList(car.carObject, lane);
        }
         */
    }

    // Update is called once per frame
    void FixedUpdate(){
        leftLaneGap += 0.1;
        middleLaneGap += 0.1;
        rightLaneGap += 0.1;

        /*
        for (int i = 0; i < totalNumberOfCars; i++){
            int lane = Random.Range(0, numberOfLanes);
            Debug.Log(lane);
            Car car = getCarByLane(lane, startPositions[lane], despawnPosition);
            if (car == null)
            {
                break;
            }
            car.spawn();
            cars.Add(car);
            addToLaneList(car.carObject, lane);
            //car.update();
        }
        */
        gapOffset = Random.Range(minGapTime, maxGapTime);
        Debug.Log("Left lane: " + leftLane.Count);
        if (leftLane.Count < maxCarsPerLane){
            if (leftLaneGap > minLeftLaneGap + gapOffset){
                Debug.Log("called");
                Car car1 = new SlowCar(leftLanePosition, despawnPosition, leftLaneSpeed);
                car1.spawn();
                cars.Add(car1);
                leftLane.Add(car1);
                leftLaneGap = 0;
            }
        }
        foreach (Car singleCar in cars){
            /*
            Vector3 currentPosition = car.transform.position;
            currentPosition.z -= speed;
            car.transform.position = currentPosition;
            if (car.transform.position.z <= -roadTileSize)
            {
                Debug.Log("Position Z: " + topPosition.z);
                car.transform.position = topPosition;
            }
            */
         
            singleCar.update();
        }
        
       // Debug.Log("leftLaneGap: " + leftLaneGap);
       // Debug.Log("middleLaneGap: " + middleLaneGap);
       // Debug.Log("rightLaneGap: " + rightLaneGap);
    }

    Car getCarByLane(int lane, Vector3 position, float despawnPosition){
        Car carToGet = null;
        if(lane == 0){
            if(leftLane.Count >= maxCarsPerLane){
                if(middleLane.Count < maxCarsPerLane){
                    carToGet = new ModerateCar(position, despawnPosition, middleLaneSpeed);
                }
                else if(rightLane.Count < maxCarsPerLane){
                    carToGet = new FastCar(position, despawnPosition, rightLaneSpeed);
                }
            }
            else{
                carToGet = new SlowCar(position, despawnPosition, leftLaneSpeed);
            }
        }
        else if(lane == 1){
            if(middleLane.Count >= maxCarsPerLane){
                if (leftLane.Count < maxCarsPerLane){
                    carToGet = new SlowCar(position, despawnPosition, leftLaneSpeed);
                }
                else if (rightLane.Count < maxCarsPerLane){
                    carToGet = new FastCar(position, despawnPosition, rightLaneSpeed);
                }
            }
            else{
                carToGet = new ModerateCar(position, despawnPosition, middleLaneSpeed);
            }
        }
        else if(lane == 2){
            if(rightLane.Count >= maxCarsPerLane){
                if (leftLane.Count < maxCarsPerLane){
                    carToGet = new SlowCar(position, despawnPosition, leftLaneSpeed);
                }
                else if (middleLane.Count < maxCarsPerLane){
                    carToGet = new ModerateCar(position, despawnPosition, middleLaneSpeed);
                }
            }
            else{
                carToGet = new FastCar(position, despawnPosition, rightLaneSpeed);
            }
        }
        return carToGet;
    }
    /*
    void addToLaneList(GameObject car, int lane){
        if(lane == 0){
            leftLane.Add(car);
        }
        else if(lane == 1){
            middleLane.Add(car);
        }
        else{
            rightLane.Add(car);
        }
    }
    */
}
