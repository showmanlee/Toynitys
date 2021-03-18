using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	List<string> words = new List<string>();
	List<GameObject> rains = new List<GameObject>();
	float t { get; set; }
	bool isOver { get; set; }
	int level { get; set; }		// max = 20
	int score { get; set; }
	int count { get; set; }
	int life { get; set; }

	public GameObject raindrop;
	public InputField input;
	public Text status;
	public Slider guage;
	public Text gameOver;

	void Start() {
		StreamReader sr = new StreamReader("Assets/Dict.txt");
		string s;
		while ((s = sr.ReadLine()) != null)
			words.Add(s);

		isOver = false;
		level = 1;
		t = 0;
		score = 0;
		count = 0;
		life = 20;
		guage.value = life;
	}

	void Update() {
		t += Time.deltaTime;
		if (!isOver) {
			if (t > 0.5f + 0.2f * (20 - level)) {
				RainAdd();
				t = 0;
			}
			if (Input.GetKeyDown(KeyCode.Return)) {
				string s = input.text;
				input.text = "";
				for (int i = 0; i < rains.Count; i++) {
					if (s == rains[i].GetComponent<RainDrop>().getText) {
						float coef = (rains[i].transform.localPosition.y + 370) / 100f;
						score += (int)(10 * level * coef);
						status.text = "LEVEL: " + level + "\nSCORE: " + score;
						GameObject g = rains[i];
						rains.RemoveAt(i);
						Destroy(g);
						break;
					}
				}
				input.Select();
				input.ActivateInputField();
			}
			for (int i = 0; i < rains.Count; i++) {
				if (rains[i].transform.localPosition.y <= -210) {
					GameObject g = rains[i];
					rains.RemoveAt(i);
					Destroy(g);
					life--;
					guage.value = life;
					if (life <= 0) {
						isOver = true;
						gameOver.gameObject.SetActive(true);
					}
				}
			}
		}
	}

	void RainAdd() {
		GameObject go = Instantiate(raindrop, GameObject.Find("Canvas").transform);
		go.GetComponent<RainDrop>().Initiate(words[Random.Range(0, words.Count)], Random.Range(-500, 500), 30 + 20 * level);
		rains.Add(go);
		count++;
		if (count % 20 == 0 && level < 20)
			level++;
		status.text = "LEVEL: " + level + "\nSCORE: " + score;
	}
}
