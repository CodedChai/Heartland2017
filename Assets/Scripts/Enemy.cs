using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public int damage = 1;
    public GameObject gameController;
    public float attackRange = 1f;
    public float moveSpeed = 1f;
    public float rotationSpeed = 1f;

    private Animator animator;
    private Transform target;
    private Transform myTransform;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        //get target from game controller
    }

    void Awake()
    {
        myTransform = transform;    // This is for efficiency
    }

	void Update () {
        if (Vector2.Distance(target.position, myTransform.position) >= attackRange)
        {
            MoveTowardsTarget();
        } else
        {
            Attack();
        }
    }

 
    void MoveTowardsTarget()
    {
        Vector2 dir = target.position - myTransform.position;
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.FromToRotation(Vector3.right, dir), rotationSpeed * Time.deltaTime);
        myTransform.position += (target.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
    }

    void Attack()
    {

    }

}
