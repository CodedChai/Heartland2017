using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class BasicAI : MonoBehaviour {
    public GameObject patrolPointsParent;
    public Transform[] patrolPoints;
    public int state = 0; // 0 is patrol, 1 is path to player, 2 is attack, 3 is search for player
    public bool updatePatrol = true;
    public float giveUpTime = 4f;
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
    public bool updateSearch = true;
    public float searchStandInPlaceTime = 3f;
    public Stopwatch sw;
    public RotationHandler rotHandler;
    public CharacterType charType;
    public bool updateAttack = true;
    public float attackWait = .33f;

    public float patrolStationaryTime = 1f;

    RaycastHit2D hit0;
    RaycastHit2D hit1;
    RaycastHit2D hit2;
    RaycastHit2D hit3;
    RaycastHit2D hit4;
    RaycastHit2D hit5;
    RaycastHit2D hit6;

    private Transform lastKnown = null;

    // Use this for initialization
    void Start () {
        patrolPoints = patrolPointsParent.GetComponentsInChildren<Transform>();
        pathFollower = GetComponent<PathFollower>();
        pathfinding = GetComponentInParent<Pathfinding>();
        grid = GetComponentInParent<Grid>();
        myTrans = transform;
        // enemyController = GetComponentInParent<GameObject>();
        pathfinding.target = patrolPoints[currentPatrolPoint];
        sw = new Stopwatch();
        player = GameObject.Find("Player");
        rotHandler = GetComponentInChildren<RotationHandler>();
        charType = GetComponent<CharacterType>();
    }

    // Update is called once per frame
    void Update () {
        StateHandler();
	}


    void StateHandler()
    {
        // This will change state from patrol or search to chase
        if ((state == 0 || state == 3) && SeePlayer())
        {
            state = 1;
            pathFollower.speed = 3;
            pathFollower.shouldMove = true;
        }

        // This keeps track of vision with player, will change state to search
        if(state == 1 || state == 3)
        {
            if (sw.Elapsed.Seconds == 0)
            {
                if (!PlayerVision())
                {
                    sw.Start();
                }
            }
            else if (sw.Elapsed.Seconds > giveUpTime)
            {
                sw.Stop();
                sw.Reset();
                if (!PlayerVision() && lastKnown == null)
                {
                    // Set the seeking transform to player's last known location and whatnot
                    lastKnown = new GameObject("Player's Last Known Location").transform;
                    lastKnown.position = pathfinding.target.position;
                    pathfinding.target = lastKnown;
                    state = 3;
                    pathFollower.speed = 1.5f;

                }

            }
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

    IEnumerator PausePatrolUpdates(float pauseLength)
    {
        updatePatrol = false;
        yield return new WaitForSeconds(pauseLength);
        updatePatrol = true;
        yield return null;
    }

    IEnumerator PauseMoving(float pauseLength)
    {
        pathFollower.shouldMove = false;
        yield return new WaitForSeconds(pauseLength);
        pathFollower.shouldMove = true;
        yield return null;
    }
    IEnumerator PauseAttack(float pauseLength)
    {
        updateAttack = false;
        yield return new WaitForSeconds(pauseLength);
        updateAttack = true;
        yield return null;
    }

    IEnumerator SearchWait(float pauseLength)
    {
        updateSearch = false;
        yield return new WaitForSeconds(pauseLength);
        updateSearch = true;
        if (PlayerVision())
        {
            print("Found you!! Time to chase.");
            state = 1;
            pathFollower.speed = 3f;
            pathfinding.target = player.transform;
        } else
        {
            print("Can't find them, guess I should head back to my post.");
            state = 0;
            pathFollower.speed = 1f;
            lastKnown = null;
            DestroyImmediate(GameObject.Find("Player's Last Known Location"));
        }
        yield return null;
    }

    void Patrol()
    {
        if(updatePatrol && Vector2.Distance(pathFollower.targetWayPoint, myTrans.position) < .01f)
        {
            StartCoroutine(PausePatrolUpdates(.4f));
            if (pathFollower.shouldMove)
            {
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
                else if (!patrolReverse)
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
                pathfinding.target = patrolPoints[currentPatrolPoint];
                //StartCoroutine(PauseMoving(patrolStationaryTime));
            }
        }
    }

    void Chase()
    {
        print("Chasing");
        if(charType.isSpecial && Random.Range(0,1) > .95f)
        {
            rotHandler.angle = AngleBetweenVector2(pathfinding.target.position, myTrans.position) + 90f;
            rotHandler.transform.localEulerAngles = new Vector3(0f, 0f, rotHandler.angle);
            charType.Primary();     // Teleport
        } else if (charType.isRanged && Random.Range(0, 1) > .95f)
        {
            rotHandler.angle = AngleBetweenVector2(pathfinding.target.position, myTrans.position) + 90f;
            rotHandler.transform.localEulerAngles = new Vector3(0f, 0f, rotHandler.angle);
            charType.Tertiary();    // Do a ranged attack
        }
        if(Vector2.Distance(pathfinding.target.position, myTrans.position) < attackDistance)
        {
            print("I should attack.");
            state = 2;
            pathFollower.shouldMove = false;
        }
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    void Attack()
    {
        if (updateAttack)
        {
            // Attack the player
            // For melee type
            rotHandler.angle = AngleBetweenVector2(pathfinding.target.position, myTrans.position) + 90f;
            rotHandler.transform.localEulerAngles = new Vector3(0f, 0f, rotHandler.angle);
            if (!charType.isSpecial && charType.isMelee && !charType.isRanged)
            {
                if (Time.time % 10 > 5f)
                {
                    charType.Secondary();
                }
                else
                {
                    charType.Tertiary();
                }
            } // For ranged type
            else if (!charType.isSpecial && charType.isMelee && charType.isRanged)
            {
                charType.Secondary();
            }
            // For teleport type
            else if (charType.isSpecial && charType.isMelee && charType.isRanged)
            {
                if (Time.time % 10 > 7f)
                {
                    charType.Primary();
                }
                else if (Time.time % 10 > 6f)
                {
                    charType.Secondary();
                }
            }
            StartCoroutine(PauseAttack(attackWait));
        }
        

        // If we're too far away from the player go back and chase them again
        if (Vector2.Distance(pathfinding.target.position, myTrans.position) > attackDistance)
        {
            print("I should chase.");
            state = 1;
            pathFollower.speed = 3;
            pathFollower.shouldMove = true;
        }
    }

    void Search()
    {
        if (updateSearch) {
            if (Vector2.Distance(pathFollower.targetWayPoint, myTrans.position) < .01f)
            {
                StartCoroutine(SearchWait(searchStandInPlaceTime));
            }

        }
    }

    bool PlayerVision()
    {
        Vector3 visionVec = (player.transform.position - myTrans.position).normalized;
        UnityEngine.Debug.DrawRay(myTrans.position, visionVec * 10f, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(myTrans.position, visionVec, Mathf.Infinity);
        if(hit.collider != null && !hit.collider.CompareTag("Player"))
        {
            print(hit.collider.tag);
            // WE LOST VISION OF THE PLAYER
            print("I can't see the target.");
            return false;
        } else if(hit.collider != null && hit.collider.CompareTag("Player"))
        {
            print("I still have eyes on the target.");
            if(pathfinding.target != player.transform)
            {
                pathfinding.target = player.transform;
            }
            return true;
        }
        // sw.Start(); when lost vision, stop calling this method, once sw.time > giveUpTime call playervision again and if it can still see it then proceed as normal but if it can't see it then go to current position of player, stop moving then go back to patrol
        return false;
    }

    bool SeePlayer()
    {
        bool playerSeen = false;
        UnityEngine.Debug.DrawRay(myTrans.position, pathFollower.dirVec * lineOfSite, Color.cyan);
        UnityEngine.Debug.DrawRay(myTrans.position - new Vector3(.4f, .4f, 0f), pathFollower.dirVec * lineOfSite, Color.cyan);
        UnityEngine.Debug.DrawRay(myTrans.position + new Vector3(.4f, .4f, 0f), pathFollower.dirVec * lineOfSite, Color.cyan);
        UnityEngine.Debug.DrawRay(myTrans.position - new Vector3(.6f, .6f, 0f), pathFollower.dirVec * lineOfSite, Color.cyan);
        UnityEngine.Debug.DrawRay(myTrans.position + new Vector3(.6f, .6f, 0f), pathFollower.dirVec * lineOfSite, Color.cyan);
        UnityEngine.Debug.DrawRay(myTrans.position - new Vector3(-.6f, .6f, 0f), pathFollower.dirVec * lineOfSite, Color.cyan);
        UnityEngine.Debug.DrawRay(myTrans.position + new Vector3(-.6f, .6f, 0f), pathFollower.dirVec * lineOfSite, Color.cyan);

        hit0 = Physics2D.Raycast(myTrans.position, pathFollower.dirVec, lineOfSite);
        hit1 = Physics2D.Raycast(myTrans.position - new Vector3(.4f, .4f, 0f), pathFollower.dirVec, lineOfSite);
        hit2 = Physics2D.Raycast(myTrans.position + new Vector3(.4f, .4f, 0f), pathFollower.dirVec, lineOfSite);
        hit3 = Physics2D.Raycast(myTrans.position - new Vector3(.6f, .6f, 0f), pathFollower.dirVec, lineOfSite);
        hit4 = Physics2D.Raycast(myTrans.position + new Vector3(.6f, .6f, 0f), pathFollower.dirVec, lineOfSite);
        hit5 = Physics2D.Raycast(myTrans.position - new Vector3(-.6f, .6f, 0f), pathFollower.dirVec, lineOfSite);
        hit6 = Physics2D.Raycast(myTrans.position + new Vector3(-.6f, .6f, 0f), pathFollower.dirVec, lineOfSite);
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
                print("" + hit3.collider.gameObject.name);
                pathfinding.target = hit3.transform;
                return true;
            }
        }
        if (hit4.collider != null)
        {
            if (hit4.collider.CompareTag("Player"))
            {
                print("" + hit4.collider.gameObject.name);

                print("See player");
                pathfinding.target = hit4.transform;
                return true;
            }
        }
        if (hit5.collider != null)
        {
            if (hit5.collider.CompareTag("Player"))
            {
                print("See player");
                pathfinding.target = hit5.transform;
                return true;
            }
        }
        if (hit6.collider != null)
        {
            if (hit6.collider.CompareTag("Player"))
            {
                print("See player");
                pathfinding.target = hit6.transform;
                return true;
            }
        }
        return false;
    }
}
