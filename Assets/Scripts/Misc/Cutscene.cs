using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour {

	public GameObject mcImg;
	private IEnumerator iEnum;
	private IEnumerator bigIEnum;

	public GameObject image1;
	public GameObject image2;
	public GameObject image3;
	public GameObject image4;
	public GameObject image5;
	public GameObject image6;
	public GameObject image7;

	private bool playing;
	// Use this for initialization
	void Start () {
		bigIEnum = playScene();
		StartCoroutine(bigIEnum);
		playing = true;
	}

	// Update is called once per frame
	void Update () {
		if(playing)
			moveUp();
	}

	void moveUp() {
		mcImg.transform.Translate(new Vector3(0f,.25f,0f));
	}

	private IEnumerator playScene() {
		iEnum = fadeIn(image1);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(3.55f);
		iEnum = fadeOut(image1);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(2.5f);
		iEnum = fadeIn(image2);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(3.55f);
		iEnum = fadeOut(image2);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(2.5f);
		iEnum = fadeIn(image3);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(3.55f);
		iEnum = fadeOut(image3);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(2.5f);
		iEnum = fadeIn(image4);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(3.55f);
		iEnum = fadeOut(image4);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(2.5f);
		iEnum = fadeIn(image5);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(3.55f);
		iEnum = fadeOut(image5);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(2.5f);
		iEnum = fadeIn(image6);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(3.55f);
		iEnum = fadeOut(image6);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(2.5f);
		iEnum = fadeIn(image7);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(5f);
		playing = false;
		yield return new WaitForSeconds(1f);
		iEnum = fadeOut(image7);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(2.8f);
		iEnum = fadeOut(mcImg);
		StartCoroutine(iEnum);
		yield return new WaitForSeconds(3f);
		Application.LoadLevel("Level 0");
	}

	private IEnumerator fadeIn(GameObject img) {
		Image image = img.GetComponent<Image>();
		for(int i = 0; i < 3; i++) {
			image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a+1f/3f);
			yield return new WaitForSeconds(2f/3f);
		}
	}
	private IEnumerator fadeOut(GameObject img) {
		Image image = img.GetComponent<Image>();
		for(int i = 0; i < 3; i++) {
			image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a-1f/3f);
			yield return new WaitForSeconds(2f/3f);
		}
	}
}
