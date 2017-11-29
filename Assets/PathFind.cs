using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind : MonoBehaviour {
	Rigidbody student;
	public Vector3 roomPos1; // Start to name plate
	public Vector3 roomPos2; // Name plate to front of Prof. Clark
	Ray ray;

	// Used to get the angle between two pts.
	private float X;
	private float Z;
	private float targetX;
	private float targetZ;
	private float angle;

	// Count num. of prof. office remembered
	private int cnt;

	// Idle
	private int xAxis;
	private int zAxis;

	// Remembered offices
	private bool one;
	private bool two;
	private bool three;
	private bool four;
	private bool five;
	private bool six;

	//First advice
	private int advice;
	private int iterate;
	public Vector3 firstOffice; // First office to be visited

	private int memory; //How many does the student know?

	private bool check;

	//Timer 
	float timer;
	float timerMax;
	float idleTimer;
	float idleMax;

	// Use this for initialization
	void Start () {
		student = GetComponent<Rigidbody>();
		timer = 0f;
		timerMax = 0.5f;
		iterate = 0;

		one = false;
		two = false; 
		three = false;
		four = false;
		five = false;
		six = false;

		advice = Random.Range (1, 7);
		// Target pos.
		switch (advice) {
		case 1:
			firstOffice = new Vector3 (-226, 15, 264);
			break;
		case 2:
			firstOffice = new Vector3 (226, 15, 264);    
			break;
		case 3:
			firstOffice = new Vector3 (523, 15, 0);
			break;
		case 4:
			firstOffice = new Vector3 (226, 15, -264);
			break;
		case 5:
			firstOffice = new  Vector3 (-226, 15, -264);
			break;
		case 6:
			firstOffice = new Vector3 (-523, 15, 0);
			break;
		}

		firstTarget ();
	}

	// Update is called once per frame
	void Update () {
		X = targetX - student.position.x;
		Z = targetZ - student.position.z;

		if (Mathf.Abs (X) < 5 && Mathf.Abs (Z) < 5) { // If we arrived front of the name plate
			student.velocity = new Vector3 (0, 0, 0);
			transform.rotation = Quaternion.Euler (0, 0, 0); //Change student angle
			transform.position = new Vector3 (X + student.position.x, 15, Z + student.position.z);

			if (student.position.x == roomPos1.x && student.position.z == roomPos1.z) {
				if ((roomPos1.x == firstOffice.x && roomPos1.z == firstOffice.z) || (cnt > 0)) {
					firstOffice = new Vector3 (0, 100, 0); // If we got to the correct office we never need the corrd. again so set it to somewhere unreachable	
					cnt++;

					if (check) {
						check = false;

						int fiftyChance = Random.Range(1, 3); // 1 or 2
						// Target idle:1 or new target:2
						switch (fiftyChance) {
						case 1:
							// Random generate an idle pos.
							xAxis = Random.Range (-475, 476);
							zAxis = Random.Range (-175, 176);

							targetX = xAxis;
							targetZ = zAxis;
							X = targetX - student.position.x;
							Z = targetZ - student.position.z;

							break;
						case 2:
							// Get new target
							chooseTarget ();

							// Check if student knows where the office is
							if (roomPos1.x == -226 && roomPos1.z == 264){
								if (!one) {
									firstTarget ();
								}
							}
							else if (roomPos1.x == 226 && roomPos1.z == 264){
								if (!two) {
									firstTarget ();
								}
							}
							else if (roomPos1.x == 523 && roomPos1.z == 0){
								if (!three) {
									firstTarget ();
								}
							}
							else if (roomPos1.x == 226 && roomPos1.z == -264){
								if (!four) {
									firstTarget ();
								}
							}
							else if (roomPos1.x == -226 && roomPos1.z == -264){
								if (!five) {
									firstTarget ();
								}
							}
							else if (roomPos1.x == -523 && roomPos1.z == 0){
								if (!six) {
									firstTarget ();
								}
							}

							break;
						}
					} 
					else {
						// Delete one memory of office randomly if memory count is at least 4
						if (memory >= 4) {
							int deleteMem = Random.Range (1, 7);
							// Target pos.
							switch (advice) {
							case 1:
								one = false;
								break;
							case 2:
								two = false; 
								break;
							case 3:
								three = false;
								break;
							case 4:
								four = false;
								break;
							case 5:
								five = false;
								break;
							case 6:
								six = false;
								break;
							}
							memory = 3;
						}

						// Remember this office
						if (student.position.x == -226 && student.position.z == 264){
							one = true;
							memory++;
						}
						else if (student.position.x == 226 && student.position.z == 264){
							two = true;
							memory++;
						}
						else if (student.position.x == 523 && student.position.z == 0){
							three = true;
							memory++;
						}
						else if (student.position.x == 226 && student.position.z == -264){
							four = true;
							memory++;
						}
						else if (student.position.x == -226 && student.position.z == -264){
							five = true;
							memory++;
						}
						else if (student.position.x == -523 && student.position.z == 0){
							six = true;
							memory++;
						}

						targetX = roomPos2.x;
						targetZ = roomPos2.z;
						X = targetX - student.position.x;
						Z = targetZ - student.position.z;
					}
				} 
				else if (cnt == 0) {
					firstTarget ();
				}
			}
			else if (student.position.x == roomPos2.x && student.position.z == roomPos2.z) {
				if (timer < timerMax) {
					timer += Time.deltaTime;
				}
				else {
					timer = 0f;

					// Back to front of name plate
					targetX = roomPos1.x;
					targetZ = roomPos1.z;
					X = targetX - student.position.x;
					Z = targetZ - student.position.z;
					angle = Mathf.Atan2 ((float)Z, (float)X);
					student.velocity = new Vector3 (200 * Mathf.Cos (angle), 0, 200 * Mathf.Sin (angle));

					check = true;
				}
			}
			else if (student.position.x == xAxis && student.position.z == zAxis) {
				if (timer < timerMax) {
					timer += Time.deltaTime;
				}
				else {
					timer = 0f;

					// Get new target
					chooseTarget ();
				}
			}
		} 
		else {
			angle = Mathf.Atan2 ((float)Z, (float)X);

			// Create a ray from the transform position along the transform's z-axis
			//ray = new Ray (transform.position, new Vector3 (30 * Mathf.Cos (angle), 0, 30 * Mathf.Sin (angle)));
			Debug.DrawRay (transform.position, new Vector3 (30 * Mathf.Cos (angle), 0, 30 * Mathf.Sin (angle)), Color.red); //Test ray

			if ((Physics.Raycast (transform.position, new Vector3 (30 * Mathf.Cos (angle), 0, 30 * Mathf.Sin (angle)), 30))) {
				timer += Time.deltaTime;
				if (timer > 0.1f && timer < 3f) {
					GetComponent<Renderer> ().material.color = Color.yellow;
				}
				else if (timer >= 3f) {
					GetComponent<Renderer> ().material.color = Color.red;
				}

				angle += 90;
				Debug.DrawRay (transform.position, new Vector3 (30 * Mathf.Cos (angle), 0, 30 * Mathf.Sin (angle)), Color.red); //Test ray
				transform.rotation = Quaternion.Euler (0, 90 + angle, 0); //Change student angle
				student.velocity = new Vector3 (200 * Mathf.Cos (angle), 0, 200 * Mathf.Sin (angle));
			} else {
				timer = 0f;
				GetComponent<Renderer> ().material.color = Color.white;

				transform.rotation = Quaternion.Euler (0, 90 + angle, 0); //Change student angle
				student.velocity = new Vector3 (200 * Mathf.Cos (angle), 0, 200 * Mathf.Sin (angle));
			}
		}
	}

	void firstTarget(){
		if (cnt == 0) {
			iterate++;
			// Target pos.
			switch (iterate) {
			case 1:
				roomPos1 = new Vector3 (-226, 15, 264);
				roomPos2 = new Vector3 (-226, 15, 375);
				break;
			case 2:
				roomPos1 = new Vector3 (226, 15, 264);    
				roomPos2 = new Vector3 (226, 15, 375); 
				break;
			case 3:
				roomPos1 = new Vector3 (523, 15, 0);
				roomPos2 = new Vector3 (625, 15, 0); 
				break;
			case 4:
				roomPos1 = new Vector3 (226, 15, -264);
				roomPos2 = new Vector3 (226, 15, -375); 
				break;
			case 5:
				roomPos1 = new  Vector3 (-226, 15, -264);
				roomPos2 = new Vector3 (-226, 15, -375); 
				break;
			case 6:
				roomPos1 = new Vector3 (-523, 15, 0);
				roomPos2 = new Vector3 (-625, 15, 0); 
				break;
			}

			targetX = roomPos1.x;
			targetZ = roomPos1.z;
		}
	}

	void chooseTarget(){
		int roomNum = Random.Range(1, 7); // Pick a random room
		// Target room according to roomNum.
		switch (roomNum)
		{
		case 1:
			roomPos1 = new Vector3 (-226, 15, 264);
			roomPos2 = new Vector3 (-226, 15, 375);
			break;
		case 2:
			roomPos1 = new Vector3 (226, 15, 264);    
			roomPos2 = new Vector3 (226, 15, 375); 
			break;
		case 3:
			roomPos1 = new Vector3 (523, 15, 0);
			roomPos2 = new Vector3 (625, 15, 0); 
			break;
		case 4:
			roomPos1 = new Vector3 (226, 15, -264);
			roomPos2 = new Vector3 (226, 15, -375); 
			break;
		case 5:
			roomPos1 = new  Vector3 (-226, 15, -264);
			roomPos2 = new Vector3 (-226, 15, -375); 
			break;
		case 6:
			roomPos1 = new Vector3 (-523, 15, 0);
			roomPos2 = new Vector3 (-625, 15, 0); 
			break;
		}

		targetX = roomPos1.x;
		targetZ = roomPos1.z;
		X = targetX - student.position.x;
		Z = targetZ - student.position.z;
	}
}