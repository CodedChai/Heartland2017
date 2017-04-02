using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour {

    public GameObject patrolPointsParent;
    public Transform[] patrolPoints;
    public int state = 0; // 0 is patrol, 1 is path to player, 2 is attack, 3 is search for player
    public bool update = true;

    public GameObject enemyController;
    private PathFollower pathFollower;
    public GameObject player;
    public Pathfinding pathfinding;


    public int currentPatrolPoint = 1;
    public bool patrolReverse = false;
    

	// Use this for initialization
	void Start () {
        patrolPoints = patrolPointsParent.GetComponentsInChildren<Transform>();
        pathFollower = GetComponent<PathFollower>();
        pathfinding = GetComponentInParent<Pathfinding>();
       // enemyController = GetComponentInParent<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (update)
        {
            Patrol();
        }
	}


    void StateUpdate()
    {
        switch (state)
        {
            case 0:
                Patrol();
                break;
            case 1:
                Chase();
                break;
            case 2:
                Attack();
                break;
            case 3:
                Search();
                break;
            default:
                Patrol();
                break;
        }
    }

    void Patrol()
    {
        pathfinding.seeker = patrolPoints[currentPatrolPoint];
        if(Vector2.Distance(pathFollower.targetWayPoint, transform.position) < Vector2.kEpsilon)
        {
            if (!patrolReverse)
            {
                if (currentPatrolPoint + 1 == patrolPoints.Length)
                {
                    currentPatrolPoint--;
                    patrolReverse = true;
                }
                else
                {
                    currentPatrolPoint++;
                }
            }
            else
            {
                if (currentPatrolPoint - 1 == 0)
                {
                    currentPatrolPoint++;
                    patrolReverse = false;
                }
                else
                {
                    currentPatrolPoint--;
                }
            }
        }
    }

    void Chase()
    {

    }

    void Attack()
    {

    }

    void Search()
    {

    }
}
