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
    public Grid grid;
    public float lineOfSite = 4f;
    public int currentPatrolPoint = 1;
    public bool patrolReverse = false;
    public float attackDistance = .05f;
    private Transform myTrans;

    RaycastHit2D hit0;
    RaycastHit2D hit1;
    RaycastHit2D hit2;
    RaycastHit2D hit3;
    RaycastHit2D hit4;

    // Use this for initialization
    void Start () {
        patrolPoints = patrolPointsParent.GetComponentsInChildren<Transform>();
        pathFollower = GetComponent<PathFollower>();
        pathfinding = GetComponentInParent<Pathfinding>();
        grid = GetComponentInParent<Grid>();
        myTrans = transform;
       // enemyController = GetComponentInParent<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (update)
        {
            StateHandler();
           
        }
      
	}


    void StateHandler()
    {
        if ((state == 0 || state == 3) && SeePlayer())
        {
            state = 1;
        }

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

    IEnumerator PauseUpdates(float pauseLength)
    {
        update = false;
        yield return new WaitForSeconds(pauseLength);
        update = true;
        yield return null;
    }

    void Patrol()
    {
        pathfinding.target = patrolPoints[currentPatrolPoint];
        if(Vector2.Distance(pathFollower.targetWayPoint, myTrans.position) < .01f)
        {
            StartCoroutine(PauseUpdates(.1f));
            if (patrolReverse)
            {
                if (currentPatrolPoint == 1) // Starts at 1 for now because of silliness with unity
                {
                    patrolReverse = false;
                }
                else
                {
                    currentPatrolPoint--;
                }
            }
            else if(!patrolReverse)
            {
                if (currentPatrolPoint == patrolPoints.Length - 1)
                {
                    patrolReverse = true;
                }
                else
                {
                    currentPatrolPoint++;
                }
            }
        }
    }

    void Chase()
    {
        print("Chasing");

        if(Vector2.Distance(pathfinding.target.position, myTrans.position) < attackDistance)
        {
            print("I should attack.");
            state = 2;
            pathFollower.shouldMove = false;
        }
    }

    void Attack()
    {
        if (Vector2.Distance(pathfinding.target.position, myTrans.position) > attackDistance)
        {
            print("I should chase.");
            state = 1;
            pathFollower.shouldMove = true;
        }
    }

    void Search()
    {

    }

    bool SeePlayer()
    {
        bool playerSeen = false;
        Debug.DrawRay(myTrans.position, pathFollower.dirVec * lineOfSite, Color.cyan);
        Debug.DrawRay(myTrans.position - new Vector3(.4f, .4f, 0f), pathFollower.dirVec * lineOfSite, Color.cyan);
        Debug.DrawRay(myTrans.position + new Vector3(.4f, .4f, 0f), pathFollower.dirVec * lineOfSite, Color.cyan);
        Debug.DrawRay(myTrans.position - new Vector3(.8f, .8f, 0f), pathFollower.dirVec * lineOfSite, Color.cyan);
        Debug.DrawRay(myTrans.position + new Vector3(.8f, .8f, 0f), pathFollower.dirVec * lineOfSite, Color.cyan);

        hit0 = Physics2D.Raycast(myTrans.position, pathFollower.dirVec, lineOfSite);
        hit1 = Physics2D.Raycast(myTrans.position - new Vector3(.4f, .4f, 0f), pathFollower.dirVec, lineOfSite);
        hit2 = Physics2D.Raycast(myTrans.position + new Vector3(.4f, .4f, 0f), pathFollower.dirVec, lineOfSite);
        hit3 = Physics2D.Raycast(myTrans.position - new Vector3(.8f, .8f, 0f), pathFollower.dirVec, lineOfSite);
        hit4 = Physics2D.Raycast(myTrans.position + new Vector3(.8f, .8f, 0f), pathFollower.dirVec, lineOfSite);

        playerSeen = CheckCollisions();

        return playerSeen;
    }

    bool CheckCollisions()
    {
        if(hit0.collider != null)
        {
            if (hit0.collider.CompareTag("Player")){
                print("See player");
                pathfinding.target = hit0.transform;
                return true;
            }
        }
        if (hit1.collider != null)
        {
            if (hit1.collider.CompareTag("Player"))
            {
                print("See player");
                pathfinding.target = hit1.transform;
                return true;
            }
        }
        if (hit2.collider != null)
        {
            if (hit2.collider.CompareTag("Player"))
            {
                print("See player");
                pathfinding.target = hit2.transform;
                return true;
            }
        }
        if (hit3.collider != null)
        {
            if (hit3.collider.CompareTag("Player"))
            {
                print("See player");
                pathfinding.target = hit3.transform;
                return true;
            }
        }
        if (hit4.collider != null)
        {
            if (hit4.collider.CompareTag("Player"))
            {
                print("See player");
                pathfinding.target = hit4.transform;
                return true;
            }
        }
        return false;
    }
}
