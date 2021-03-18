using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public GameObject mob;
	public Text scoreUI;
	int count, score;

	private void Awake() {
		instance = this;
	}

	void Start() {
		count = score = 0;
		StartCoroutine(Ispawner());
		
		// 마우스가 화면 바깥으로 나가지 않게 하기
		Cursor.lockState = CursorLockMode.Confined;
	}


	void Update() {

	}
	
	public void scoring() {
		score++;
		scoreUI.text = score.ToString();
	}

	// 적 생성기
	IEnumerator Ispawner() {
		while (true) {
			yield return new WaitForSeconds(1);
			count++;
			Instantiate(mob, new Vector3(Random.Range(-45, 45), 1, Random.Range(-45, 45)), Quaternion.identity);
		}
	}
}
