using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public CharacterType character;//archetype/movesets
    float speed;
    int maxHP;
    Vector3 prevPos;//to make physics stuff look a bit smoother with some bounceback
	
    // Use this for initialization
	void Start ()
    {
        speed = character.GetMoveSpeed();
        maxHP = character.GetHP();
        prevPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            character.Primary(Input.GetAxis("HorizontalR"), Input.GetAxis("VerticalR"));
        }
        else if(Input.GetButtonDown("Fire2"))
        {
            character.Secondary(Input.GetAxis("HorizontalR"), Input.GetAxis("VerticalR"));
        }
        else if(Input.GetButtonDown("Fire3"))
        {
            character.Tertiary(Input.GetAxis("HorizontalR"), Input.GetAxis("VerticalR"));
        }


    }

    // Update movement on physics due to collisions
    void FixedUpdate()
    {
        prevPos = transform.position;
        Move();
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


}
