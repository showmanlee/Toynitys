using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RainDrop : MonoBehaviour {
	string text { get; set; }
	float speed { get; set; }
	GameObject gameManager;

	void Start() {

	}

	void Update() {
		transform.localPosition -= new Vector3(0, speed * Time.deltaTime, 0);
	}

	public void Initiate(string t, int pos, float s) {
		gameManager = GameObject.Find("GameManager");
		text = t;
		speed = s;
		transform.localPosition = new Vector3(pos, 370, 0);
		gameObject.name = text;
		GetComponent<Text>().text = text;
	}

	public string getText => text;
}
