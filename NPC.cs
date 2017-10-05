using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	Vector2 velocity;
	Controller2D controller;

	bool grounded;

	float gravity = -0.2f;
	float xspeed = 2f;
	float famine = 100f;
	float sleep = 100f;
	float thirst = 100f;

	void Start () {
		controller = GetComponent<Controller2D> ();
	}

	void Update () {
		if (controller.collisions.above || controller.collisions.below){
			velocity.y = 0;
		} else if (!controller.collisions.below){
			controller.collisions.Reset ();
			velocity.y += gravity;
		}

		controller.Move (velocity * Time.deltaTime);
	}
}
