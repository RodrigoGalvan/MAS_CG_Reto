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

            if (lastWayPoint != nextWaipoint) { 
                Quaternion lookAtWP = Quaternion.LookRotation(nextWaipoint.transform.position - this.transform.position);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAtWP, rotSpeed * Time.deltaTime);
                //this.transform.LookAt(nextWaipoint.transform);
                this.transform.Translate(0, 0, speed * Time.deltaTime);
            }
        }

        if (Vector3.Distance(this.transform.position, nextWaipoint.transform.position) < 4)
        {
            move = false;
        }

    }

    public void MoveToPoint(Waypoints wayPoint) {
        move = true;
        lastWayPoint = nextWaipoint;
        nextWaipoint = wayPoint;
    }
}

