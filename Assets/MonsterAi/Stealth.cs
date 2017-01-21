using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stealth : Base 
{
	enum EyesState
	{
		EYES_IDLE,
		EYES_FADEIN,
		EYES_FADEOUT,
	}

	const float m_MaxPrepareTime = 1.5f;

	public float m_MinTeleportDistance = 4.0f;
	public float m_MaxTeleportDistance = 8.0f;

	GameObject m_Player;
	float m_PrepareTimer;
	float m_MinCancelAttackDist = 1.75f;
	Vector3 m_PreparedPos;

	SpriteRenderer m_Eyes;

	float m_MaxBlinkEyesDelta = 5.0f;
	float m_BlinkEyesTimer;
	EyesState m_EyesState;


	void Start () 
	{
		m_IdleTime = m_MaxIdleTime;
		m_State = State.STATE_IDLE;
		m_Agent = GetComponent<NavMeshAgent>();
		m_Agent.autoRepath = false;

		m_BlinkEyesTimer = m_MaxBlinkEyesDelta;
		m_EyesState = EyesState.EYES_IDLE;

		m_Player = GameObject.FindGameObjectWithTag("Player");

		SphereCollider c = GetComponent<SphereCollider> ();
		if (c != null)
		{
			c.radius = m_HuntingDistance;
			c.isTrigger = true;
		}

		m_Eyes = GetComponentInChildren<SpriteRenderer>();
	}

	void Update() 
	{
		switch (m_State) 
		{
			case State.STATE_HUNTING:
			{
				if (IsDestinationReached()) 
				{
					Debug.Log("Destination reached going to idle");
					m_State = State.STATE_IDLE;
				} 
				else 
				{
					m_Agent.SetDestination(m_Player.transform.position);
				}
				break;
			}
			case State.STATE_IDLE:
			{
				break;
			}
			case State.STATE_PREPARE:
			{
				m_PrepareTimer -= Time.deltaTime;
				if (m_PrepareTimer <= 0.0f) 
				{
					TryAttack();
				}
				break;
			}
			default:
			{
				break;
			}
		}

		if (m_BlinkEyesTimer > 0.0f) 
		{
			m_BlinkEyesTimer -= Time.deltaTime;
			if (m_BlinkEyesTimer <= 0.0f) 
			{
				BlinkEyes();
			}
		}

		switch (m_EyesState)
		{

		}
	}

	protected override void StartHunting(Vector3 target)
	{
		if (m_State != State.STATE_PREPARE
			&& m_State != State.STATE_HUNTING) 
		{
			PrepareForAttack();
		}
	}

	protected override void StartRoaming()
	{

	}

	void PrepareForAttack()
	{
		m_State = State.STATE_PREPARE;
		m_PreparedPos = m_Player.transform.position;
		m_PrepareTimer = m_MaxPrepareTime;

		float dist = 0.0f;
		Vector3 point = Vector3.one;
		while (dist < m_MinTeleportDistance 
				|| dist == Mathf.Infinity
				|| point.x == Mathf.Infinity
				|| point.y == Mathf.Infinity)
		{
			point = Random.insideUnitCircle * Random.Range(m_MinTeleportDistance, m_MaxTeleportDistance);
			point += m_Target;
			NavMeshHit hit;
			NavMesh.SamplePosition(point, out hit, m_MaxRoamingDistance, 1);
			dist = Vector3.Distance(hit.position, m_Target);
			point = hit.position;
		}

		transform.position = point;

		BlinkEyes();
	}

	void TryAttack()
	{
		float movedDist = Vector3.Distance(m_PreparedPos, m_Player.transform.position);
		Debug.Log("User moved " + movedDist);

		if (movedDist < m_MinCancelAttackDist) {
			Debug.Log("Lazy user");

			m_MaxMovementTime = m_MaxHuntingTime;
			m_MovementStart = Time.time;

			m_State = State.STATE_HUNTING;
			m_Agent.speed = m_HuntingSpeed;
			m_Agent.acceleration = m_HuntingAcceleration;
			m_Agent.stoppingDistance = m_HuntingStoppingDist;

			m_Agent.SetDestination(m_Player.transform.position);
		} 
		else 
		{
			m_State = State.STATE_IDLE;
		}
	}

	void BlinkEyes()
	{
		//do stuff
	}
}
