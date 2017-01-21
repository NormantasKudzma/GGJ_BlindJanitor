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

	int m_CurrentDir;
	float m_NormalScale;
	SkeletonAnimation m_AnimationScript;
	string m_CurrentAnim;
	Transform m_Parent;

	SkeletonDataAsset[] m_SkeletonArray;

	void Start()
	{
		m_AnimationScript = GetComponent<SkeletonAnimation>();

		m_CurrentAnim = "Idle";
		m_CurrentDir = 0;
		m_SkeletonArray = new SkeletonDataAsset[]{ m_RightSkeleton, m_DownSkeleton , m_RightSkeleton, m_UpSkeleton };

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
		SetSkeletonByAngle(m_Parent.localEulerAngles.y);
	}

	public void PlayAnimation(string anim)
	{
		if (anim != m_CurrentAnim) {
			m_AnimationScript.AnimationName = anim;
			m_CurrentAnim = anim;
		}
	}

	public void SetSkeletonByDirection(Direction d)
	{
		SetSkeletonByAngle((int)d * 91.0f);
	}

	public void SetSkeletonByAngle(float angle)
	{
		Vector3 currEuler = transform.localEulerAngles;
		currEuler.y = -angle;
		transform.localEulerAngles = currEuler;

		int dir = (Mathf.RoundToInt((angle - 45.0f) / 90.0f)) % 4;
		//Debug.Log ("Angle -> " + angle + " = " + dir);

		if (dir != m_CurrentDir) {
			m_CurrentDir = dir;

			m_AnimationScript.skeletonDataAsset = m_SkeletonArray [dir];
			m_AnimationScript.Initialize (true);

			if (dir == 0) {// dir == 2) {
				Vector3 scale = transform.localScale;
				scale.x = m_NormalScale;
				transform.localScale = scale;
			} else if (dir == 2) {
				Vector3 scale = transform.localScale;
				scale.x = -m_NormalScale;
				transform.localScale = scale;
			}
		}
		/*m_AnimationScript.skeletonDataAsset = m_DownSkeleton;
		m_AnimationScript.Initialize (true);*/

	}
}
