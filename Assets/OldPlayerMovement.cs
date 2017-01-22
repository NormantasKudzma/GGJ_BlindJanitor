using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerMovement : MonoBehaviour {

	float m_MaxSpeed = 0.09f;
	const float m_MaxLightDuration = 0.5f;
	float m_LightDuration = m_MaxLightDuration;

	float m_AttackTimer;

	AnimationHelper m_AnimHelper;

	// Use this for initialization
	void Start () {
		m_AnimHelper = GetComponentInChildren<AnimationHelper>();
		tag = "Player";
		m_AttackTimer = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;

		float hspeed = Input.GetAxis ("Horizontal");
		float vspeed = Input.GetAxis ("Vertical");

		if (hspeed > 0.0f) {
			m_AnimHelper.SetSkeletonByDirection (AnimationHelper.Direction.RIGHT);
		} else if (hspeed < 0.0f) {
			m_AnimHelper.SetSkeletonByDirection (AnimationHelper.Direction.LEFT);
		} else if (vspeed > 0.0f) {
			m_AnimHelper.SetSkeletonByDirection (AnimationHelper.Direction.DOWN);
		} else if (vspeed < 0.0f) {
			m_AnimHelper.SetSkeletonByDirection (AnimationHelper.Direction.UP);
		}

		if (m_AttackTimer <= 0.0f) {
			if (hspeed != 0.0f || vspeed != 0.0f) {
				m_AnimHelper.PlayAnimation ("Walk");
			} else {
				m_AnimHelper.PlayAnimation ("Idle");
			}
		}

		pos.x += hspeed * m_MaxSpeed;
		pos.z += vspeed * m_MaxSpeed;

		transform.position = pos;

		if (m_LightDuration > 0.0f) 
		{
			m_LightDuration -= Time.deltaTime;

			if (m_LightDuration <= 0.0f) 
			{
				GetComponent<BoxCollider> ().enabled = false;
			}
		}

		if (m_AttackTimer > 0.0f) {
			m_AttackTimer -= Time.deltaTime;
			if (m_AttackTimer <= 0.0f) {
				m_AnimHelper.PlayAnimation("Idle");
			}
		}

		if (Input.GetButtonDown ("Fire1") && m_AttackTimer <= 0.0f) 
		{
			GetComponent<BoxCollider>().enabled = true;
			m_LightDuration = m_MaxLightDuration;
			m_AttackTimer = m_AnimHelper.PlayAnimation("Click");
		}
	}
}