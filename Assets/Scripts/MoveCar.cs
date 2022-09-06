using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour
{
    public float speed = 1;
    public int rotSpeed;
    public bool move = false;
    public string unique_id;
    public Waypoints nextWaipoint;
    public Waypoints lastWayPoint;
    public bool stop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (move == true) {
            if (Mathf.Abs(int.Parse(lastWayPoint.xPos) - int.Parse(nextWaipoint.xPos)) > 1 || Mathf.Abs(int.Parse(lastWayPoint.yPos) - int.Parse(nextWaipoint.yPos)) > 1)
            {
                transform.position = nextWaipoint.transform.position;
            }

            if (lastWayPoint != nextWaipoint)
            {
                Quaternion lookAtWP = Quaternion.LookRotation(nextWaipoint.transform.position - this.transform.position);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAtWP, rotSpeed * Time.deltaTime);
                this.transform.Translate(0, 0, speed * Time.deltaTime);
            }
            else {
                move = false;
            }
        }

        if (Vector3.Distance(this.transform.position, nextWaipoint.transform.position) < .8)
        {
            move = false;
        }

    }

    public void CheckId(Waypoints[] waypoints, Object ob) {
        //Look for car to move in list of cars using unique id

        string posX = ob.positionX;
        string posY = ob.positionZ;
        foreach (Waypoints wayPoint in waypoints)
        {
            if (posX == wayPoint.xPos && posY == wayPoint.yPos)
            {
                MoveToPoint(wayPoint);
                break;
            }
        }
        
    }

    public void MoveToPoint(Waypoints wayPoint) {
        move = true;
        lastWayPoint = nextWaipoint;
        nextWaipoint = wayPoint;
    }
}

