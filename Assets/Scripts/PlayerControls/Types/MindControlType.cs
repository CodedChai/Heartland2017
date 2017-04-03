using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindControlType : CharacterType {

    public RotationHandler rot;
    public Transform rotationObj;

    // Use this for initialization
    void Start()
    {
        movespeed = 3;

        name = "Hypno";
        //overwrite movespeed and hp here.
        hp = 4;
        //setting melee
    }

    // Update is called once per frame
    void Update () {
		
	}
}
