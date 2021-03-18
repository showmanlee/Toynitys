using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	public GameObject ped;

	void Start() {

	}

	void Update() {

		// 마우스/키보드 화면 스크롤
		if (Input.mousePosition.x / Screen.width < 0.05f || Input.GetKey(KeyCode.LeftArrow))
			transform.position += new Vector3(-50 * Time.deltaTime, 0, 0);
		if (Input.mousePosition.x / Screen.width > 0.95f || Input.GetKey(KeyCode.RightArrow))
			transform.position += new Vector3(50 * Time.deltaTime, 0, 0);
		if (Input.mousePosition.y / Screen.height < 0.05f || Input.GetKey(KeyCode.DownArrow))
			transform.position += new Vector3(0, 0, -50 * Time.deltaTime);
		if (Input.mousePosition.y / Screen.height > 0.95f || Input.GetKey(KeyCode.UpArrow))
			transform.position += new Vector3(0, 0, 50 * Time.deltaTime);

		// 보드 한계
		if (transform.position.x > 50)
			transform.position = new Vector3(50, transform.position.y, transform.position.z);
		if (transform.position.x < -50)
			transform.position = new Vector3(-50, transform.position.y, transform.position.z);
		if (transform.position.z > 39)
			transform.position = new Vector3(transform.position.x, transform.position.y, 39);
		if (transform.position.z < -61)
			transform.position = new Vector3(transform.position.x, transform.position.y, -61);

		// 원래대로 돌아오기
		if (Input.GetKey(KeyCode.Space))
			transform.position = new Vector3(ped.transform.position.x, 25, ped.transform.position.z - 11);
	}
}
