using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public MoveCar[] carPrefab; //List of car prefabs
    public Waypoints[] waypoints; //List of waypoints
    public TrafficLight[] trafficLights; //List of traffic lights
    MoveCar nextCar; //The car that is to be moved
    int stationaryCars; //Amount of cars that are stationary
    bool carExists; //If car exists or is to be instantiated
    int i = 0; //Iterator through list of jsons
    List<List<Object>> myDeserializedObjList; //List of agents in list of jsons
    string posX; //Pos in x of car to be instantiated
    string posY; //Pos in y of car to be instantiated
    Dictionary<string, MoveCar> dic; //Dictionary of id of car and its object
    public GameObject startB; //Canvas of menu
    public TextAsset[] jsons; //List of jsons
    GameObject[] gameObjects; //List of cars to be destroyed each time a new json will be read
    bool start; //Bool to start reading json

    void Start()
    {
        start = false;
        dic = new Dictionary<string, MoveCar>();
    }

  
    void Update()
    {
        //If everything is ready then start
        if (start == true) { 
            //Iterate through all the list6 of jsons
            if (i < myDeserializedObjList.Count)
            {
                //See if all cars are done with their move
                stationaryCars = 0;
                //Iterate through the dictionary
                foreach (KeyValuePair<string, MoveCar> entry in dic)
                {
                    if (entry.Value.move == false)
                    {
                        //If car is not moving then add 1 to the quantity of stationary cars
                        stationaryCars += 1;
                    }

                }


                ////If the amount of stationary cars is the same as all the cars in existance or if 80% have already stoped
                if (stationaryCars == dic.Count || stationaryCars >= (dic.Count * 0.8))
                {
                    //For each object in json file execute this for loop
                    for (int j = 0; j < myDeserializedObjList[i].Count; j++)
                    {
                        //If the object is the kind of car then do this
                        if (myDeserializedObjList[i][j].kind == "car")
                        {
                            //Car doesnt exist until we find it in dictionary
                            carExists = false;
                            if (dic.ContainsKey(myDeserializedObjList[i][j].id))
                            {
                                //If car found theen execute method checkID in MoveCar type object
                                carExists = true;
                                dic[myDeserializedObjList[i][j].id].CheckId(waypoints, myDeserializedObjList[i][j]);
                            }

                            //If car doesn't exist then instantiate the car
                            if (carExists == false)
                            {
                                //Get y and x pos of the car to be insantiateed
                                posX = myDeserializedObjList[i][j].positionX;
                                posY = myDeserializedObjList[i][j].positionZ;
                                foreach (Waypoints wayPoint in waypoints)
                                {
                                    //Look for the waypoint that matches that description
                                    if (posX == wayPoint.xPos && posY == wayPoint.yPos)
                                    {
                                        //Choose prefab that corresponds
                                        if (myDeserializedObjList[i][j].color == "white")
                                        {
                                            nextCar = carPrefab[0];
                                        }
                                        else if (myDeserializedObjList[i][j].color == "green2")
                                        {
                                            nextCar = carPrefab[1];
                                        }
                                        else
                                        {
                                            nextCar = carPrefab[2];
                                        }
                                        //Set the next waypoint of car and unique id
                                        nextCar.nextWaipoint = wayPoint;
                                        nextCar.lastWayPoint = wayPoint;
                                        nextCar.unique_id = myDeserializedObjList[i][j].id;
                                        //Insantiate the car in the waypoint that matched description
                                        dic.Add(nextCar.unique_id, Instantiate(nextCar, wayPoint.transform.position, Quaternion.identity));
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //If the object is type traffic light then set the color for the traffic light
                            foreach (TrafficLight tl in trafficLights)
                            {
                                tl.ColorTL(myDeserializedObjList[i][j].color, myDeserializedObjList[i][j].id);
                            }
                        }
                    }
                    i++;
                }
            }
            else {
                //If all json has been read then show menu
                startB.SetActive(true);
            }
        }
    }

    public void StartGame(string Json)
    {
        //Reset everything in scene and read new json to start simulation
        dic.Clear();
        i = 0;
        myDeserializedObjList = JsonConvert.DeserializeObject<List<List<Object>>>(Json);
        gameObjects = GameObject.FindGameObjectsWithTag("Car");
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
        start = true;
        startB.SetActive(false);
    }

    //Differeent jsons that can be selected
    public void Json1()
    {
        StartGame(jsons[0].text);
    }
    public void Json2()
    {
        StartGame(jsons[1].text);
    }
    public void Json3()
    {
        StartGame(jsons[2].text);
    }
    public void Json4()
    {
        StartGame(jsons[3].text);
    }
    public void Json5()
    {
        StartGame(jsons[4].text);
    }


}


//Json class which is a list of objects
[Serializable]
public class RootClass { 
    public List<Object> rootClass { get; set; }
}

