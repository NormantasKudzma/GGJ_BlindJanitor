using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float forceAmmount;
	public float bounceAmmount;
	Vector3 forceDirection;
	Rigidbody rb;


	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		forceDirection = new Vector3(Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		rb.AddForce (forceDirection.normalized * forceAmmount);
	}

	void OnCollisionEnter(Collision coll){
		rb.AddForce (-forceDirection * rb.velocity.magnitude * bounceAmmount, ForceMode.Impulse);
	}
}
