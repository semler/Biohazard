using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class Actions : MonoBehaviour {

	private Animator animator;
	private AnimatorStateInfo stateInfo;
	private Vector3 velocity;


	private int hp;



	void Start ()
	{
		animator = GetComponent<Animator> ();

		hp = 3;

	}

	void Update () {
		if (hp == 0) {
			Death ();
			return;
		}

		stateInfo = animator.GetCurrentAnimatorStateInfo (0);

		if (Input.GetButton ("Fire1")) {
			Attack ();
			return;
		}

		if (stateInfo.normalizedTime > 1 && stateInfo.IsName ("Base Layer.Attack")) {
			animator.SetBool ("Attack", false);
		}
			
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (v != 0) {
			animator.SetBool ("Guard", false);
		}

		animator.SetFloat ("Speed", v);

		velocity = new Vector3 (0, 0, v);
		velocity = transform.TransformDirection (velocity);

		velocity *= 1;
			
		transform.localPosition += velocity * Time.fixedDeltaTime;
		transform.Rotate (0, h * 2, 0);	




	}

	private void Attack () {
		animator.SetFloat ("Speed", 0f);
		animator.SetBool ("Guard", true);
		animator.SetBool ("Attack", true);
	}

	private void Damage () {
		animator.SetBool ("Damage", true);
	}

	private void Death () {
		animator.SetBool ("Death", true);
	}

	private void Reset(){
		hp = 3;
		animator.SetFloat ("Speed", 0f);
		animator.SetBool ("Death", false);

	}


	public void Attacked() {
		Debug.Log ("Attacked");

	}
}
