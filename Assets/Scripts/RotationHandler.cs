using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour {
    private float angle;
    SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start () {
        //spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
    }

    void Rotate()
    {
        float x = Input.GetAxis("HorizontalR");
        float y = Input.GetAxis("VerticalR");
        if (x != 0.0f || y != 0.0f)
        {
            angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg - 90f;
            //print(angle);
            transform.localEulerAngles = new Vector3(0f, 0f, angle);

        }
    }
}
