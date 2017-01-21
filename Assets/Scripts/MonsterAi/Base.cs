using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Base : MonoBehaviour
{
	public enum State {
		STATE_HUNTING,
		STATE_ROAMING,
		STATE_IDLE,

		STATE_PREPARE,
	}

	public float m_MaxIdleTime = 1.5f;
	public float m_MinIdleTime = 0.5f;

	public float m_RoamingAcceleration = 4.0f;
	public float m_RoamingSpeed = 1.2f;
	public float m_MaxRoamingDistance = 7.0f;
	protected float m_MinRoamingDistance = 0.8f;
	protected float m_MaxRoamingAngle = 60.0f;
	protected float m_RoamingStoppingDist = 0.8f;

	public float m_HuntingAcceleration = 8.0f;
	public float m_HuntingSpeed = 3.5f;
	public float m_HuntingDistance = 2.0f;
	protected float m_HuntingStoppingDist = 0.3f;

	public float m_MaxHuntingTime = 2.0f;
	public float m_MaxRoamingTime = 5.0f;

	protected Vector3 m_Target;
	protected NavMeshAgent m_Agent;
	protected float m_MaxMovementTime;
	protected float m_MovementStart;

	protected float m_IdleTime;
	protected State m_State;

	protected abstract void StartHunting(Vector3 target);
	protected abstract void StartRoaming();

	protected bool IsDestinationReached()
	{
		if ((!m_Agent.pathPending
			&& m_Agent.remainingDistance <= m_Agent.stoppingDistance
			&& (!m_Agent.hasPath || m_Agent.velocity.sqrMagnitude == 0f))
			|| (Time.time - m_MovementStart) > m_MaxMovementTime)
		{
			return true;
		}
		return false;
	}

	protected void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player")) 
		{
			m_Target = col.gameObject.transform.position;
			StartHunting(m_Target);
		}
	}

	protected void OnCollisionEnter(Collision col)
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;

		if (col.gameObject.CompareTag("Player")) 
		{
			m_State = State.STATE_IDLE;
			Debug.Log ("Player ded?");
		}
	}
}
