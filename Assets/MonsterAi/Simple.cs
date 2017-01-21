using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Simple : Base 
{
	void Start () {
		m_IdleTime = m_MaxIdleTime;
		m_State = State.STATE_IDLE;
		m_Agent = GetComponent<NavMeshAgent>();
		m_Agent.autoRepath = false;

		SphereCollider c = GetComponent<SphereCollider> ();
		if (c != null)
		{
			c.radius = m_HuntingDistance;
			c.isTrigger = true;
			// New comment
		}
	}

	void Update() 
	{
		switch (m_State) 
		{
			case State.STATE_ROAMING:
			case State.STATE_HUNTING:
			{
				if (IsDestinationReached())
				{
					m_State = State.STATE_IDLE;
					m_IdleTime = Random.Range(m_MinIdleTime, m_MaxIdleTime);
				}
				break;
			}
			case State.STATE_IDLE:
			{
				m_IdleTime -= Time.deltaTime;
				if (m_IdleTime <= 0.0f) 
				{
					StartRoaming();
				}
				break;
			}
			default:
			{
				break;
			}
		}
	}

	protected override void StartRoaming()
	{
		m_MaxMovementTime = m_MaxRoamingTime;
		m_MovementStart = Time.time;

		m_State = State.STATE_ROAMING;
		m_Agent.speed = m_RoamingSpeed;
		m_Agent.acceleration = m_RoamingAcceleration;
		m_Agent.stoppingDistance = m_RoamingStoppingDist;

		/*float newAngle = Random.Range(-m_MaxRoamingAngle, m_MaxRoamingAngle) + transform.eulerAngles.y;
		Vector3 roamTo = (Quaternion.Euler(0.0f, newAngle, 0.0f) * Vector3.one) * Random.Range(0.1f, m_MaxRoamingDistance) - transform.position;

		*/NavMeshHit hit;/*
		NavMesh.SamplePosition(roamTo, out hit, m_MaxRoamingDistance, 1);

		// fallback, select random destination
		if (hit.distance < m_MinRoamingDistance || hit.distance >= Mathf.Infinity) */
		{
			Vector3 roamTo = transform.position + Random.insideUnitSphere * m_MaxRoamingDistance;
			NavMesh.SamplePosition(roamTo, out hit, m_MaxRoamingDistance, 1);
		}

		m_Agent.SetDestination(hit.position);
		DrawPath(m_Agent.path);
	}

	void OnTriggerStay(Collider col)
	{
		if (col.CompareTag("Player")) 
		{
			StartHunting (col.gameObject.transform.position);
		}
	}

	protected override void StartHunting(Vector3 target)
	{
		m_MaxMovementTime = m_MaxHuntingTime;
		m_MovementStart = Time.time;

		m_State = State.STATE_HUNTING;
		m_Agent.speed = m_HuntingSpeed;
		m_Agent.acceleration = m_HuntingAcceleration;
		m_Agent.stoppingDistance = m_HuntingStoppingDist;

		m_Agent.SetDestination(target);
		DrawPath(m_Agent.path);
	}

	void DrawPath(NavMeshPath path)
	{
		if (path.corners.Length < 2)
		{
			return;
		}

		LineRenderer renderer = GetComponent<LineRenderer> ();
		renderer.numPositions = path.corners.Length;

		renderer.SetPosition(0, transform.position);
		for (int i = 1; i < path.corners.Length; ++i) 
		{
			renderer.SetPosition(i, path.corners [i]);
		}
	}
}
