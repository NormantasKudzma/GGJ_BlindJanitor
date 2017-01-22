using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Startscript : MonoBehaviour {

	public SpriteRenderer dude;
	public SpriteRenderer start;
	public GameObject startbutn;

	float tilTitle = 1.5f;
	float titleProgress = 0.0f;
	float tilStart = 2.5f;
	float startProgress = 0.0f;

	// Use this for initialization
	void Start () {
		Button btn = GameObject.Find ("Button").GetComponent<Button> ();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		tilTitle -= Time.deltaTime;
		if (tilTitle <= 0.0f) {
			titleProgress += Time.deltaTime * 0.5f;
			if (titleProgress <= 1.0f) {
				var c = dude.color;
				c.a = titleProgress;
				dude.color = c;
			}
		}

		tilStart -= Time.deltaTime;
		if (tilStart <= 0.0f) {
			startProgress += Time.deltaTime * 0.5f;
			if (startProgress <= 1.0f) {
				var c = start.color;
				c.a = startProgress;
				start.color = c;
			}
		}
	}

	void TaskOnClick()
	{
		SceneManager.LoadScene("Scenexxx");
	}
}
