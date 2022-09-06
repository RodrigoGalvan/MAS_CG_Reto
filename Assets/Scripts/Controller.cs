using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public TextAsset textJson;
    public MoveCar carPrefab;
    public List<MoveCar> cars;
    public Waypoints[] waypoints;
    int stationaryCars;
    bool carExists;
    int i = 0;
    List<List<Object>> myDeserializedObjList;
    string posX;
    string posY;
    // Start is called before the first frame update
    void Start()
    {
        myDeserializedObjList = JsonConvert.DeserializeObject<List<List<Object>>>(textJson.text);
    }

    void Update()
    {
        if (i < myDeserializedObjList.Count) { 
        
        
            //See if all cars are done with their move
            stationaryCars = 0;

            //For each car that is in existance detect if it is moving or not
            foreach (MoveCar car in cars)
            {
                if (car.move == false)
                {
                    //If car is not moving then add 1 to the quantity of stationary cars
                    stationaryCars += 1;
                }
            }

            ////If the amount of stationary cars is the same as all the cars in existance
            if (stationaryCars == cars.Count)
            {
                //For each object in json file execute this for loop
                for (int j = 0; j < myDeserializedObjList[i].Count; j++)
                {

                    //If the object is the kind of car then do this
                    if (myDeserializedObjList[i][j].kind == "car")
                    {
                        carExists = false;
                        foreach(MoveCar car in cars)
                        {
                            //Look for car to move in list of cars using unique id
                            if (car.unique_id.Equals(myDeserializedObjList[i][j].id))
                            {
                                carExists = true;
                                posX = myDeserializedObjList[i][j].positionX;
                                posY = myDeserializedObjList[i][j].positionZ;
                                foreach (Waypoints wayPoint in waypoints)
                                {
                                    if (posX == wayPoint.xPos && posY == wayPoint.yPos)
                                    {
                                        car.MoveToPoint(wayPoint);
                                    }
                                }
                            }
                        }
                        //If car doesn't exist then instantiate the car
                        if (carExists == false)
                        {
                            posX = myDeserializedObjList[i][j].positionX;
                            posY = myDeserializedObjList[i][j].positionZ;
                            foreach (Waypoints wayPoint in waypoints)
                            {
                                if (posX == wayPoint.xPos && posY == wayPoint.yPos)
                                {
                                    carPrefab.nextWaipoint = wayPoint;
                                    carPrefab.lastWayPoint = wayPoint;
                                    carPrefab.unique_id = myDeserializedObjList[i][j].id;
                                    cars.Add(Instantiate(carPrefab, wayPoint.transform.position, Quaternion.identity));
                                
                                }
                            }
                        }
                    }
                }
                i++;
            }
        }
    }

}



[Serializable]
public class RootClass { 
    public List<Object> rootClass { get; set; }
}
