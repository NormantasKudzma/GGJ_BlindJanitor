using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightEffect : MonoBehaviour {

	Light light;
	public float defaultAngle;
	public float maxAngle;
	public float angleIncrease;
	public GameObject sonarSprite;
	public float defaultSpriteScale = 1.2f;
	public float retractSpeed; 
	float currentSpriteScale = 1.2f;
	GameObject spriteGO;
	float currentAngle;
	bool animateLight = false;
	float timeToFull;
	float nextStepSpriteTime;
	float currentNextStepSpriteTime;
	int indexSprite = 0;
	SpriteRenderer sr;

	bool retractStage = false;
	public Sprite[] sprites;



	void Start () {
		currentAngle = defaultAngle;
		light = GetComponent<Light> ();
		light.spotAngle = currentAngle;
	}

	void Update ()
	{
		
		timeToFull = (maxAngle - defaultAngle) / angleIncrease;
		nextStepSpriteTime = timeToFull / sprites.Length;

		InputListener ();
		LightAnimation ();
	
	}

	void InputListener(){
		if (Input.GetKeyDown (KeyCode.Space) && animateLight == false) {
			animateLight = true;
			spriteGO = (GameObject)Instantiate (sonarSprite, transform.position - Vector3.up * 5f, Quaternion.Euler (90, 0, 0));
			sr = spriteGO.GetComponent<SpriteRenderer> ();
			indexSprite = 0;
			currentSpriteScale = defaultSpriteScale;
			spriteGO.transform.parent = transform.parent;
		}
	}

	void LightAnimation(){
		if (animateLight) {
			if (retractStage == false) {
				if (currentAngle >= defaultAngle && currentAngle < maxAngle) {
					currentNextStepSpriteTime += Time.deltaTime;
					if (currentNextStepSpriteTime > nextStepSpriteTime) {
						indexSprite++;
						currentNextStepSpriteTime = 0;
					}
					if (indexSprite < sprites.Length) {
						sr.sprite = sprites [indexSprite];
					}
					currentAngle += angleIncrease * Time.deltaTime;
					currentSpriteScale += (2.8f / timeToFull) * Time.deltaTime;
				} else if (currentAngle > maxAngle) {
					Destroy (spriteGO.gameObject);
					retractStage = true;
				}

			} else {
				currentAngle -= angleIncrease * Time.deltaTime * retractSpeed;
				if (currentAngle < defaultAngle) {
					animateLight = false;
					currentAngle = defaultAngle;
					retractStage = false;
				}

			}

			if (spriteGO != null) {
				spriteGO.transform.localScale = Vector3.one * currentSpriteScale;
			}
			light.spotAngle = currentAngle;
		}
	}
}