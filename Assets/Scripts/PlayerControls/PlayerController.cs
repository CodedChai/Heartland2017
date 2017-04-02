using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public CharacterType characterType;//archetype/movesets
    float speed;
    int maxHP;
    public int hp;
    Vector3 prevPos;//to make physics stuff look a bit smoother with some bounceback

    private Animator animator;

    // Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        speed = characterType.GetMoveSpeed();
        maxHP = characterType.GetHP();
        hp = maxHP;
        prevPos = transform.position;
        if(characterType == null)
        {
            characterType = GetComponent<MindControlType>();
        }
        hp = maxHP;
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            characterType.Primary();
        }
        else if(Input.GetButtonDown("Fire2"))
        {
            characterType.Secondary();
        }
        else if(Input.GetButtonDown("Fire3"))
        {
            characterType.Tertiary();
        }


    }

    // Update movement on physics due to collisions
    void FixedUpdate()
    {
        prevPos = transform.position;
        Move();
        UpdateAnimationVars();
    }

    //basic movement. Might make this check to see if it's doing stuff but idk
    void Move()
    {
        transform.Translate(new Vector3(speed * Input.GetAxis("Horizontal") * Time.deltaTime, speed * Input.GetAxis("Vertical") * Time.deltaTime));
    }

    //record pos.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = prevPos;
    }

    void UpdateAnimationVars() {
      animator.SetFloat("x_mov", speed * Input.GetAxis("Horizontal"));
      animator.SetFloat("y_mov", speed * Input.GetAxis("Vertical"));
    }


}
