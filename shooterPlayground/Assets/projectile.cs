using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

	Vector3 direction;
	void Start() {
	}

	void Update() {
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			GameManager.instance.scoring();
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}

	// 발사 코드
	public void fire(Vector3 dir) {
		direction = Vector3.Normalize(dir);
		direction.y = 0;
		StartCoroutine(IMover());
		StartCoroutine(Ikiller());
	}

	IEnumerator IMover() {
		while (true) {
			yield return new WaitForEndOfFrame();
			transform.position += direction * Time.deltaTime * 30;
		}
	}
	IEnumerator Ikiller() {
		yield return new WaitForSeconds(3);
		Destroy(gameObject);
	}
}
