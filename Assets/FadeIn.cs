using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
    public Image fader;

	// Use this for initialization
	void Start () {
        StartCoroutine(Enter(1f));
	}

    public IEnumerator Enter(float time)
    {
        fader.enabled = true;
        for(float x = 0; x < time; x+=Time.deltaTime)
        {
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, 1f - x / time);
            yield return new WaitForEndOfFrame();
        }
        fader.enabled = false;
        yield return null;
    }

    public void Exit()
    {
        StartCoroutine(Exit(1f));
    }

    public IEnumerator Exit(float time)
    {
        fader.enabled = true;
        for (float x = 0; x < time; x += Time.deltaTime)
        {
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, x / time);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
