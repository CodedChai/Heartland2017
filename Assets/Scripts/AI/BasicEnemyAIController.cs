using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAIController : MonoBehaviour {

    public int damage;

    private Animator animator;
    private Transform target;
    private bool skipMove;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
