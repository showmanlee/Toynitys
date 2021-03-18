using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject block;
	public GameObject blockCanvas;
	public Text scoreUI, MatchableUI;
	GameObject[,] blocks = new GameObject[7, 7];
	bool[,] matched = new bool[7, 7];
	bool control = true;
	string selected = "";
	int score;
	int combo;

	void Start() {
		for (int i = 0; i < 7; i++)
			for (int j = 0; j < 7; j++) {
				GameObject g = Instantiate(block, blockCanvas.transform);
				g.GetComponent<Block>().Initiate(Random.Range(0, 7), i, j);
				blocks[i, j] = g;
				matched[i, j] = false;
			}
		StartCoroutine(SwapWait(0, 0, 0, 0));
		score = 0; combo = 1;
		scoreUI.text = "SCORE: " + score;
	}

	// Update is called once per frame
	void Update() {
		if (control) {
			// 마우스 조작
			var ped = new PointerEventData(null);
			ped.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();
			blockCanvas.GetComponent<GraphicRaycaster>().Raycast(ped, results);

			if (Input.GetMouseButton(0) && results.Count != 0) {
				if (selected == "")
					selected = results[0].gameObject.name;
				else if (selected != results[0].gameObject.name) {
					string p1 = selected.Split('-')[0], p2 = selected.Split('-')[1];
					string q1 = results[0].gameObject.name.Split('-')[0], q2 = results[0].gameObject.name.Split('-')[1];
					if ((p1 != q1) ^ (p2 != q2)) {
						// SWAP
						int x1 = int.Parse(p1), y1 = int.Parse(p2), x2 = int.Parse(q1), y2 = int.Parse(q2);
						StartCoroutine(SwapWait(x1, x2, y1, y2));
					}
					control = false;
				}
			}
			else if (Input.GetMouseButtonUp(0))
				selected = "";
		}
		else if (Input.GetMouseButtonUp(0)) {
			control = true;
			selected = "";
		}
	}

	public bool check() {
		// 매치 여부 체킹
		for (int i = 0; i < 7; i++)
			for (int j = 0; j < 7; j++)
				matched[i, j] = false;

		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 7; j++) {
				if (blocks[i, j].GetComponent<Block>().GetColor() == blocks[i + 1, j].GetComponent<Block>().GetColor()
					&& blocks[i + 1, j].GetComponent<Block>().GetColor() == blocks[i + 2, j].GetComponent<Block>().GetColor())
					matched[i, j] = matched[i + 1, j] = matched[i + 2, j] = true;
				if (blocks[j, i].GetComponent<Block>().GetColor() == blocks[j, i + 1].GetComponent<Block>().GetColor()
					&& blocks[j, i + 1].GetComponent<Block>().GetColor() == blocks[j, i + 2].GetComponent<Block>().GetColor()) {
					matched[j, i] = matched[j, i + 1] = matched[j, i + 2] = true;
				}
			}
		}
		for (int i = 0; i < 7; i++)
			for (int j = 0; j < 7; j++)
				if (matched[i, j])
					return true;
		return false;
	}

	IEnumerator SwapWait(int x1, int x2, int y1, int y2) {
		control = false;
		blocks[x1, y1].GetComponent<Block>().Swap(x2, y2);
		blocks[x2, y2].GetComponent<Block>().Swap(x1, y1);
		var b = blocks[x1, y1];
		blocks[x1, y1] = blocks[x2, y2];
		blocks[x2, y2] = b;
		yield return new WaitForSeconds(0.25f);
		if (!check()) {
			// 매칭되는 게 없음
			blocks[x1, y1].GetComponent<Block>().Swap(x2, y2);
			blocks[x2, y2].GetComponent<Block>().Swap(x1, y1);
			b = blocks[x1, y1];
			blocks[x1, y1] = blocks[x2, y2];
			blocks[x2, y2] = b;
		}
		else {
			// 매칭됨 - 터지기
			while (true) {
				int boom = 0;
				for (int i = 0; i < 7; i++)
					for (int j = 0; j < 7; j++)
						if (matched[i, j]) {
							blocks[i, j].GetComponent<Block>().Pop();
							boom++;
						}
				yield return new WaitForSeconds(0.6f);
				for (int i = 0; i < 7; i++)
					for (int j = 6; j >= 0; j--) {
						if (blocks[j, i] == null) {
							int k;
							for (k = j; k >= 0; k--)
								if (blocks[k, i] != null)
									break;
							if (k < 0)
								break;
							blocks[k, i].GetComponent<Block>().Drop(j);
							blocks[j, i] = blocks[k, i];
							blocks[k, i] = null;
						}
					}
				yield return new WaitForSeconds(0.25f);

				control = false;
				for (int i = 0; i < 7; i++)
					for (int j = 0; j < 7; j++) {
						if (blocks[i, j] == null) {
							GameObject g = Instantiate(block, blockCanvas.transform);
							g.GetComponent<Block>().Initiate(Random.Range(0, 7), i, j);
							blocks[i, j] = g;
						}
					}
				control = true;

				score += boom * combo * 100;
				combo++;
				scoreUI.text = "SCORE: " + score;
				if (!check())
					break;
			}
			combo = 1;
		}
	}

	public void Reset() {
		for (int i = 0; i < 7; i++)
			for (int j = 0; j < 7; j++) {
				Destroy(blocks[i, j]);
				blocks[i, j] = null;
			}
		Start();
	}
}
