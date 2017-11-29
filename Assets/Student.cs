using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour {
	private int numStudent;
	private int xAxis;
	private int zAxis;
	public Person studentPrefab;
	public Person student;

	// Use this for initialization
	void Start () {
		numStudent = Random.Range (1, 11); // Student num. 1 ~ 10	
		xAxis = Random.Range (-475, 476);
		zAxis = Random.Range (-175, 176);

		for (int i = 0; i < numStudent; i++) {
			xAxis = Random.Range (-475, 476);
			zAxis = Random.Range (-175, 176);

			student = Instantiate(studentPrefab) as Person;
			student.transform.localPosition = new Vector3(xAxis, 15, zAxis);
		}
	}
}
