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
       //While move is true then keep moving
        if (move == true) {
            //If next waypoint is farther than 1 in distance then move car instantly
            if (Mathf.Abs(int.Parse(lastWayPoint.xPos) - int.Parse(nextWaipoint.xPos)) > 1 || Mathf.Abs(int.Parse(lastWayPoint.yPos) - int.Parse(nextWaipoint.yPos)) > 1)
            {
                transform.position = nextWaipoint.transform.position;
            }

            //While the lastwaypoint is not the next move if not stop
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

        //If distance to vector is this then the car is practicaly stoped
        if (Vector3.Distance(this.transform.position, nextWaipoint.transform.position) < .8)
        {
            move = false;
        }

    }

    public void CheckId(Waypoints[] waypoints, Object ob) {
        string posX = ob.positionX;
        string posY = ob.positionZ;
        //Iterate through waypoints and find the correct one to move to that waypoint
        foreach (Waypoints wayPoint in waypoints)
        {
            if (posX == wayPoint.xPos && posY == wayPoint.yPos)
            {
                //Move to waypoint
                MoveToPoint(wayPoint);
                break;
            }
        }
        
    }

    public void MoveToPoint(Waypoints wayPoint) {
        move = true;
        //Set last waypoint and new waypoint
        lastWayPoint = nextWaipoint;
        nextWaipoint = wayPoint;
    }
}

