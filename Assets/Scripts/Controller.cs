using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public TextAsset textJson;
    public MoveCar[] carPrefab;
    public List<MoveCar> cars;
    public Waypoints[] waypoints;
    MoveCar nextCar;
    int stationaryCars;
    bool carExists;
    int i = 0;
    List<List<Object>> myDeserializedObjList;
    string posX;
    string posY;
    Dictionary<string, MoveCar> dic;
    // Start is called before the first frame update
    void Start()
    {
        dic = new Dictionary<string, MoveCar>();
        myDeserializedObjList = JsonConvert.DeserializeObject<List<List<Object>>>(textJson.text);
    }

    void Update()
    {
        if (i < myDeserializedObjList.Count) { 
        
        
            //See if all cars are done with their move
            stationaryCars = 0;
            foreach (KeyValuePair<string, MoveCar> entry in dic)
            {
                if (entry.Value.move == false)
                {
                    //If car is not moving then add 1 to the quantity of stationary cars
                    stationaryCars += 1;
                }

            }


            ////If the amount of stationary cars is the same as all the cars in existance
            if (stationaryCars == dic.Count || stationaryCars >= (dic.Count*0.5))
            {
                //For each object in json file execute this for loop
                for (int j = 0; j < myDeserializedObjList[i].Count; j++)
                {

                    //If the object is the kind of car then do this
                    if (myDeserializedObjList[i][j].kind == "car")
                    {
                        carExists = false;
                        if (dic.ContainsKey(myDeserializedObjList[i][j].id)) {
                            carExists = true;
                            dic[myDeserializedObjList[i][j].id].CheckId(waypoints, myDeserializedObjList[i][j]);
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
                                    if (myDeserializedObjList[i][j].color == "white")
                                    {
                                        nextCar = carPrefab[0];
                                    }
                                    else if (myDeserializedObjList[i][j].color == "green2")
                                    {
                                        nextCar = carPrefab[1];
                                    }
                                    else {
                                        nextCar = carPrefab[2];
                                    }
                                    nextCar.nextWaipoint = wayPoint;
                                    nextCar.lastWayPoint = wayPoint;
                                    nextCar.unique_id = myDeserializedObjList[i][j].id;
                                    dic.Add(nextCar.unique_id, Instantiate(nextCar, wayPoint.transform.position, Quaternion.identity));
                                    //cars.Add(Instantiate(nextCar, wayPoint.transform.position, Quaternion.identity));
                                    break;
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
