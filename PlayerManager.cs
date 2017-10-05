using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	Animator anim;
	Vector2 velocity;
	SpriteRenderer mySpriteRender;
	Controller2D controller;

	bool grounded;

	float gravity = -0.2f;
	float acc = 0.04f;
	float dec = 0.5f;
	float frc = 0.04f;
	float top = 2;
	float jumpVelocity = 3.0f;

	void Start () {
		mySpriteRender = GetComponent<SpriteRenderer> ();
		controller = GetComponent<Controller2D> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (controller.collisions.above || controller.collisions.below){
			velocity.y = 0;
		} else if (!controller.collisions.below){
			controller.collisions.Reset ();
			velocity.y += gravity;
		}

		if (Input.GetKeyDown (KeyCode.Z) && controller.collisions.below){
			velocity.y = jumpVelocity;
		}
			
		// Movement and Animation
		if (Input.GetKey (KeyCode.A)) {
			if (velocity.x > 0) {
				velocity.x -= dec;
			} else if (velocity.x > -top) {
				velocity.x -= acc;
			}

		} else if (Input.GetKey (KeyCode.D)) {
			if (velocity.x < 0) {
				velocity.x += dec;
			} else if (velocity.x < top) {
				velocity.x += acc;
			}
		} else {
			velocity.x -= Mathf.Min (Mathf.Abs (velocity.x), frc) * Mathf.Sign (velocity.x);
		}

		if (velocity.x <= -0.1f) {
			mySpriteRender.flipX = true;
		} else if (velocity.x >= 0.1f) {
			mySpriteRender.flipX = false;
		}

		anim.SetFloat ("velocity.x", velocity.x);

		//Animation Speed
		if (velocity.x >= 3 || velocity.x <= -3) {
			anim.speed = 2f;
		} else {
			anim.speed = 1f;
		}

		velocity = new Vector2 (velocity.x, velocity.y);

		controller.Move (velocity * Time.deltaTime);
		print ("Velocity.x: " + velocity.x + "   Velocity.y: " + velocity.y);
	}
}