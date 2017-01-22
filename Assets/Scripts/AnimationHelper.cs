using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class AnimationHelper : MonoBehaviour
{
	public enum Direction
	{
		RIGHT = 0,
		UP = 1,
		LEFT = 2,
		DOWN = 3,
	}

	public SkeletonDataAsset m_UpSkeleton;
	public SkeletonDataAsset m_DownSkeleton;
	public SkeletonDataAsset m_RightSkeleton;

	public bool m_AutoSkeletons = true;

	SkeletonDataAsset m_CurrentAsset;
	float m_NormalScale;
	SkeletonAnimation m_AnimationScript;
	string m_CurrentAnim;
	Transform m_Parent;

	SkeletonDataAsset[] m_SkeletonArray;
	int oldDir = 0;

	void Start()
	{
		m_AnimationScript = GetComponent<SkeletonAnimation>();

		m_CurrentAnim = "Idle";
		m_SkeletonArray = new SkeletonDataAsset[]{ m_RightSkeleton, m_DownSkeleton , m_RightSkeleton, m_UpSkeleton };
		m_CurrentAsset = m_SkeletonArray[0];

		m_NormalScale = transform.localScale.x;
		m_Parent = transform.parent.gameObject.transform;

		// cia testas 


		/*for (float i = 0; i <= 360; i += 15) {
			SetSkeletonByAngle (i);
		}*/

		/*SetSkeletonByDirection (Direction.RIGHT);
		SetSkeletonByDirection (Direction.UP);
		SetSkeletonByDirection (Direction.LEFT);
		SetSkeletonByDirection (Direction.DOWN);*/

	}

	void Update()
	{
		if (m_AutoSkeletons) {
			SetSkeletonByAngle(m_Parent.localEulerAngles.y, true);
		}
	}

	public float PlayAnimation(string anim)
	{
		if (anim != m_CurrentAnim) {
			m_AnimationScript.AnimationName = anim;
			m_CurrentAnim = anim;
		}

		var currAnim = m_AnimationScript.AnimationState.Data.SkeletonData.FindAnimation(anim);
		return currAnim != null ? currAnim.Duration : 0.0f;
	}

	public void SetSkeletonByDirection(Direction d)
	{
		SetSkeletonByAngle((int)d * 91.0f, false);
	}

	public void SetSkeletonByAngle(float angle, bool adjustAngle)
	{
		if (adjustAngle) {
			Vector3 currEuler = transform.localEulerAngles;
			currEuler.y = -angle;
			transform.localEulerAngles = currEuler;
		}

		int dir = (Mathf.RoundToInt((angle - 45.0f) / 90.0f)) % 4;

		//Debug.Log ("Angle -> " + angle + " = " + dir);

		if (dir != oldDir) {
			SetSkeleton(m_SkeletonArray[dir]);

			if (dir == 0) {
				Vector3 scale = transform.localScale;
				scale.x = m_NormalScale;
				transform.localScale = scale;
			} else if (dir == 2) {
				Vector3 scale = transform.localScale;
				scale.x = -m_NormalScale;
				transform.localScale = scale;
			}
		}

		oldDir = dir;
	}

	public void SetSkeleton(SkeletonDataAsset skel)
	{
		m_CurrentAsset = skel;
		m_AnimationScript.skeletonDataAsset = skel;
		m_AnimationScript.Initialize (true);
	}

	public void SetLoop(bool loop)
	{
		m_AnimationScript.loop = loop;
	}
}
