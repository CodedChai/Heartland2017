using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDstate : MonoBehaviour {

    public Sprite blank;
    public Sprite sendMC;
    public Sprite getMC;

    public Sprite whoosh;
    public Sprite bigWhoosh;

    public Sprite shooty1;
    public Sprite shooty2;
    public Sprite shooty3;

    public Sprite blink;

    public Image warning;
    public Image hypno;
    public Image primary;
    public Image secondary;
    public Image tertiary;

    public bool blinking = true;
    float blinkTime = .5f;
    float time = 0;

    // Update is called once per frame
    void Update()
    {
        if (blinking)
        {
            warning.enabled = true;
        }
        else
        {
            warning.enabled = false;
        }
    }

    public void Hypno()
    {
        hypno.sprite = sendMC;
        secondary.sprite = whoosh;
        tertiary.sprite = blank;
        primary.sprite = blank;
    }

    public void Teleport()
    {
        hypno.sprite = getMC;
        tertiary.sprite = shooty1;
        secondary.sprite = whoosh;
        primary.sprite = blink;
    }

    public void Puncher()
    {
        hypno.sprite = getMC;
        tertiary.sprite = bigWhoosh;
        secondary.sprite = whoosh;
        primary.sprite = blank;
    }

    public void Shooter()
    {
        hypno.sprite = getMC;
        tertiary.sprite = shooty2;
        secondary.sprite = shooty3;
        primary.sprite = blank;
    }
}
