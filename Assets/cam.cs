using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour {
	float speed;

	// Mouse
	float h = 5.0f;
	float v = 2.0f;
	float yaw = 0.0f;
	float pitch = 0.0f;

	// Use this for initialization
	void Start () {
		speed = 20f;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
		}
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
		}
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
		}
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			transform.Translate(new Vector3(0,0, speed * Time.deltaTime));
		}
			
		yaw += h * Input.GetAxis("Mouse X");
		pitch -= v * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
	}
}
