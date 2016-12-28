using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiAction : MonoBehaviour {

	private CharacterController controller;
	private Animator animator;
	private Vector3 destination;	
	private Vector3 randDestination;	
	private Vector3 velocity;        
	private Vector3 direction;       
	private float waitTime;       
	private float currentTime;
	private GameObject girl;

	private AnimatorStateInfo stateInfo;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		velocity = Vector3.zero;
		girl = GameObject.Find("SportyGirl");
		girl.SendMessage ("Attacked");
		waitTime = 2.0f;
		currentTime = 0.0f;
		randDestination = RandDestination();
	}

	// Update is called once per frame
	void Update () {
		stateInfo = animator.GetCurrentAnimatorStateInfo (0);
		destination = new Vector3(girl.transform.position.x, girl.transform.position.y, girl.transform.position.z);

		if (Vector3.Distance (destination, transform.position) > 5) {
			animator.SetBool ("Attack", false);

			if (Vector3.Distance (randDestination, transform.position) < 0.5) {
				currentTime += Time.deltaTime;
				if (currentTime > waitTime) {
					animator.SetBool ("Walk", true);
					randDestination = RandDestination ();
					currentTime = 0.0f;
					Walk (randDestination, 0.5f);
				} else {
					animator.SetBool ("Walk", false);
				}
			} else {
				animator.SetBool ("Walk", true);
				Walk (randDestination, 0.5f);
			}
		} else if (Vector3.Distance (destination, transform.position) < 1.5) {
			animator.SetBool ("Attack", true);

			direction = (destination - transform.position).normalized;
			transform.LookAt (new Vector3 (destination.x, transform.position.y, destination.z));

			if (stateInfo.IsName ("Attack")) {
//				girl.SendMessage ("Attacked");
			}
		} else {
			animator.SetBool ("Attack", false);
			animator.SetBool ("Walk", true);

			Walk (destination, 1.0f);
		}
	}

	private void Walk (Vector3 destination, float speed) {
		if (controller.isGrounded) {
			velocity = Vector3.zero;
			direction = (destination - transform.position).normalized;
			transform.LookAt (new Vector3 (destination.x, transform.position.y, destination.z));
			velocity = direction * speed;
		}
		velocity.y += Physics.gravity.y * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}

	private Vector3 RandDestination () {
		Vector3 random = Random.insideUnitCircle * 10;
		return transform.position + new Vector3(random.x, 0, random.z);
	}
}
