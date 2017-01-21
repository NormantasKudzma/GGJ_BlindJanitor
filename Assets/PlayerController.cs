using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	float m_MaxSpeed = 0.06f;
	const float m_MaxLightDuration = 0.5f;
	float m_LightDuration = m_MaxLightDuration;

	// Use this for initialization
	void Start () {
		tag = "Player";
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;

		pos.x += Input.GetAxis("Horizontal") * m_MaxSpeed;
		pos.z += Input.GetAxis("Vertical") * m_MaxSpeed;

		transform.position = pos;

		if (m_LightDuration > 0.0f) 
		{
			m_LightDuration -= Time.deltaTime;

			if (m_LightDuration <= 0.0f) 
			{
				GetComponent<BoxCollider> ().enabled = false;
			}
		}

		if (Input.GetButtonDown ("Fire1")) 
		{
			GetComponent<BoxCollider>().enabled = true;
			m_LightDuration = m_MaxLightDuration;
		}
    }
}
