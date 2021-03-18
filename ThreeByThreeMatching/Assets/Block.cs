using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 7x7 블록 - 중심 = (-90, -30), 간격 = 85
// 기준점 = (-345, 225)
// y값 커질수록 떨어지기

public class Block : MonoBehaviour {
	int color { get; set; }     // 색깔
	int[] pos = new int[2];     // 위치(0~6)
	bool movable = false;       // 움직이는 중?
	float t = 0;				// time

	public void Initiate(int color, int x, int y) {
		this.color = color;
		pos[0] = x; pos[1] = y;
		GetComponent<Image>().color = new Color(0.3f + (color / 4 == 1 ? 0.5f : 0f), 0.3f + (color % 4 / 2 == 1 ? 0.5f : 0f), 0.3f + (color % 2 == 1 ? 0.5f : 0f));
		transform.localPosition = new Vector3(-345 + pos[1] * 85, 225 - pos[0] * 85, 0);
		transform.name = pos[0] + "-" + pos[1];
	}

	public int GetColor() => color;

	public void Pop() {
		StartCoroutine(Poping());
	}

	public void Selected(bool b) {
		if (b)
			transform.localScale = new Vector3(1.05f, 1.05f, 1);
		else
			transform.localScale = new Vector3(1f, 1f, 1);
	}

	public void Swap(int x, int y) {
		movable = true;
		pos[0] = x; pos[1] = y;
		transform.name = pos[0] + "-" + pos[1];
		t = 0;
		StartCoroutine(Moving());
		Selected(false);

	}

	public void Drop(int n) {
		pos[0] = n;
		transform.name = pos[0] + "-" + pos[1];
		movable = true;
		t = 0;
		StartCoroutine(Moving());
	}

	IEnumerator Moving() {
		Vector3 v = new Vector3(-345 + pos[1] * 85, 225 - pos[0] * 85, 0) - transform.localPosition;
		while (movable) {
			t += Time.deltaTime;
			transform.localPosition += v * Time.deltaTime * 4;
			if (t > 0.25f) {
				movable = false;
				transform.localPosition = new Vector3(-345 + pos[1] * 85, 225 - pos[0] * 85, 0);
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForEndOfFrame();
	}

	IEnumerator Poping() {
		transform.localScale = new Vector3(1.1f, 1.1f, 1);
		GetComponent<Image>().color -= new Color(0, 0, 0, 0.5f);
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}

