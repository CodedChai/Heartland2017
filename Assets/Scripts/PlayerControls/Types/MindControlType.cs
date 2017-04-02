using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindControlType : CharacterType {

    public RotationHandler rot;
    public Transform rotationObj;

	// Use this for initialization
	void Start () {
        rot = GetComponentInChildren<RotationHandler>();
        rotationObj = rot.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
