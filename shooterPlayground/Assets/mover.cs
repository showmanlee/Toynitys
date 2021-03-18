using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class mover : MonoBehaviour {

	public projectile proj;
	void Start() {

	}
	void Update() {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		// 우클릭 이동
		if (Physics.Raycast(ray, out hit, 1000) && Input.GetMouseButton(1)) {
			if (hit.transform.name != "Player") {
				GetComponent<NavMeshAgent>().isStopped = false;
				GetComponent<NavMeshAgent>().destination = hit.point;
			}
		}

		// 좌클릭 사격
		if (Physics.Raycast(ray, out hit, 1000) && Input.GetMouseButtonDown(0)) {
			if (hit.transform.name != "Player") {
				projectile p = Instantiate(proj, transform.position, Quaternion.identity);
				p.fire(hit.point - transform.position);
			}
		}

		// 정지
		if (Input.GetKeyDown(KeyCode.S))
			GetComponent<NavMeshAgent>().isStopped = true;
	}
}
