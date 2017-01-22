using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class PlayerController : MonoBehaviour {

	public float forceAmmount;
	//public float bounceAmmount;
	Vector3 forceDirection;
	Rigidbody rb;

	const float m_MaxLightDuration = 0.5f;
	float m_LightDuration = m_MaxLightDuration;

	float m_AttackTimer;

	AnimationHelper m_AnimHelper;
	SpotLightEffect sle;

	public SkeletonDataAsset m_DeathAnim;
	bool m_Dead = false;


	void Start () {
		rb = GetComponent<Rigidbody> ();
		m_AnimHelper = GetComponentInChildren<AnimationHelper>();
		sle = GetComponentInChildren<SpotLightEffect> ();
		tag = "Player";
		m_AttackTimer = 0.0f;
	}

	public void DiePlz()
	{
		m_Dead = true;
		m_AnimHelper.SetLoop(false);
		m_AnimHelper.PlayAnimation("Idle");
		m_AnimHelper.SetSkeleton(m_DeathAnim);
	}

	void Update () {
		if (m_Dead) {
			return;
		}

		float hspeed = Input.GetAxis ("Horizontal");
		float vspeed = Input.GetAxis ("Vertical");
		forceDirection = new Vector3(hspeed, 0, vspeed);



		Vector3 pos = transform.position;



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

		if (m_AttackTimer > 0.0f) {
			m_AttackTimer -= Time.deltaTime;
			if (m_AttackTimer < 0.333f) {
				sle.InputListener ();
			}
			if (m_AttackTimer <= 0.0f) {
				m_AnimHelper.PlayAnimation("Idle");
			}
		}

		if (Input.GetButtonDown ("Fire1") && m_AttackTimer <= 0.0f) 
		{
			m_LightDuration = m_MaxLightDuration;
			m_AttackTimer = m_AnimHelper.PlayAnimation("Click");
		}

		rb.AddForce (forceDirection.normalized * forceAmmount);



	}

//	void OnCollisionEnter(Collision coll){
//		rb.AddForce (-forceDirection * rb.velocity.magnitude * bounceAmmount, ForceMode.Impulse);
//	}
}
