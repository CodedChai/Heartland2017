using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : CharacterType {

	// Use this for initialization
	void Start () {
		//overload movespeed and hp here.
		movespeed = 3;
		hp = 4;
	}

    // Update is called once per frame
    override public void Primary(float joyX, float joyY)
    {
        print("neawt");
    }
}
