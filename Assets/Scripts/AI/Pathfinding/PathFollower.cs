using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Vector3 targetWayPoint;

    public float speed = 4f;
    public int id;

    public Pathfinding pathfinder;

    void Start()
    {
        if (pathfinder == null)
        {
            pathfinder = GetComponentInParent<Pathfinding>();
        }
    }



    // Update is called once per frame
    void Update()
    {
        targetWayPoint = pathfinder.nextPosition;
        walk();

    }

    void walk()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
    }
}
