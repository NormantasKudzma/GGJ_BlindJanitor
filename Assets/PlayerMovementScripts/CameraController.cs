using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform player;
	public float cameraFollowRadius;
	public float cameraFollowMagnitude;

	Vector3 flatPlayerPos;
	Vector3 flatCameraPos;
	float distance;
	Vector3 cameraMoveDir;

	void Start () {
		
	}

	void Update () {
		flatPlayerPos = new Vector3 (player.position.x, 0, player.position.z);
		flatCameraPos = new Vector3 (transform.position.x, 0, transform.position.z);
		distance = Vector3.Distance(flatPlayerPos, flatCameraPos);
		if(distance > cameraFollowRadius){
			cameraMoveDir = flatPlayerPos - flatCameraPos;
			transform.Translate (cameraMoveDir.normalized * cameraFollowMagnitude * distance * Time.deltaTime, Space.World);
		}
	}
}
