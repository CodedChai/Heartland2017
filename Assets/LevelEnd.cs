using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collided with end level");
        if(collision.transform.name == "Player")
            GameObject.FindGameObjectWithTag("Unique").GetComponent<StateTrack>().LoadLevel();
    }
}
