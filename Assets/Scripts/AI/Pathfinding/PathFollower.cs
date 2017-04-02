using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{

    public int direction = 0; // 0 is left, 1 is right, 2 is down, 3 is up 
    public Vector3 targetWayPoint;

    Vector3 dir;
    public bool isPatroling = true;

    public float speed = 4f;
    public int id;

    public Pathfinding pathfinder;

    private Transform myTrans;

    void Start()
    {
        myTrans = transform;
        if (pathfinder == null)
        {
            pathfinder = GetComponentInParent<Pathfinding>();
        }
    }



    // Update is called once per frame
    void Update()
    {
        targetWayPoint = pathfinder.nextPosition;
        Walk();
        direction = Direction();
        //print("Direction is: " + dir + " which is " + direction);
    }

    void Walk()
    {
     
        myTrans.position = Vector3.MoveTowards(myTrans.position, targetWayPoint, speed * Time.deltaTime);
    }

    int Direction()
    {
        dir = (targetWayPoint - myTrans.position).normalized;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if(dir.x < 0)
            {
                return 0;
            } else
            {
                return 1;
            }
        }
        else if(Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
        {
            if(dir.y < 0)
            {
                return 2;
            } else
            {
                return 3;
            }
        }
        return direction;
    }
}
