using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    //Id of traffic light
    public string id;


    public void ColorTL(string color, string _id) {
        //If id is the one of the traffic light then change the color
        if (id.Equals(_id)) {
            if (color.Equals("red"))
            {
                this.GetComponent<Light>().color = Color.red;
            }
            else {
                this.GetComponent<Light>().color = Color.green;
            }
        }
    }
}
