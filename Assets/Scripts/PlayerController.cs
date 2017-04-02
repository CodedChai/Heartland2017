using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public CharacterType character;
    float speed;
    int maxHP;

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        speed = character.GetMoveSpeed();
        maxHP = character.GetHP();
        character.Primary(1, 1);
        character.Secondary(1, 1);
	}

	// Update is called once per frame
	void Update () {
	}

    // Update movement on physics due to collisions
    void FixedUpdate()
    {
        Move();
        UpdateAnimationVars();
    }

    void Move()
    {
        transform.Translate(new Vector3(speed * Input.GetAxis("Horizontal") * Time.deltaTime, speed * Input.GetAxis("Vertical") * Time.deltaTime, 0f));
    }

    void UpdateAnimationVars() {
        animator.SetFloat("x_mov", speed * Input.GetAxis("Horizontal"));
        animator.SetFloat("y_mov", speed * Input.GetAxis("Vertical"));
    }


}
