using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stealth : Base 
{
	const float m_MaxPrepareTime = 1.5f;

	public float m_MinTeleportDistance = 2.0f;

	GameObject m_Player;
	float m_PrepareTimer;
	float m_MinCancelAttackDist = 1.0f;
	Vector3 m_PreparedPos;

	void Start () 
	{
		m_IdleTime = m_MaxIdleTime;
		m_State = State.STATE_IDLE;
		m_Agent = GetComponent<NavMeshAgent>();
		m_Agent.autoRepath = false;

		m_Player = GameObject.FindGameObjectWithTag("Player");

		SphereCollider c = GetComponent<SphereCollider> ();
		if (c != null)
		{
			c.radius = m_HuntingDistance;
			c.isTrigger = true;
		}
	}

	void Update() 
	{
		switch (m_State) 
		{
			case State.STATE_HUNTING:
			{
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
	}

	protected override void StartHunting(Vector3 target)
	{
		PrepareForAttack();
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
		while (dist < m_MinTeleportDistance && dist != Mathf.Infinity)
		{
			point = Random.insideUnitCircle * Random.Range(m_MinTeleportDistance, m_MinTeleportDistance * 1.35f);
			point += m_Target;
			NavMeshHit hit;
			NavMesh.SamplePosition(point, out hit, m_MaxRoamingDistance, 1);
			dist = Vector3.Distance(hit.position, m_Target);
		}

		transform.position = point;
	}

	void TryAttack()
	{
		//check for attack
	}
}
