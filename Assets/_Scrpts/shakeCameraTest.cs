using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplatterSystem.TopDown
{
	public class shakeCameraTest : MonoBehaviour
	{
		public float duration = 2f;
		public float speed = 20f;
		public float magnitude = 2f;
		public AnimationCurve damper = new AnimationCurve(
			new Keyframe(0f, 1f), new Keyframe(0.9f, .33f, -2f, -2f), new Keyframe(1f, 0f, -5.65f, -5.65f));
		public float minStepSize = 0f;
		public bool useCameraProjection = false;
		private Camera MainCamera;

		private Vector3 originalPos;
		private Vector2 seed;
		private PlayerFarestPos endDetect;

		private void Awake()
		{
			MainCamera = GetComponent<Camera>();
		}
        private void Start()
        {
			originalPos = transform.localPosition;
			endDetect = FindObjectOfType<PlayerFarestPos>();
        }
        void OnEnable()
		{
			seed = new Vector2(Random.value * 1000f, Random.value * 1000f);
		}

        private void Update()
        {
			if (endDetect.gameEnd)
			{
				transform.SetParent(null);
			}
        }

        public virtual void Shake()
		{
			StopAllCoroutines();
			StartCoroutine(Shake(MainCamera.transform, originalPos, duration, speed, magnitude, damper));
		}

		public virtual void Stop()
		{
			StopAllCoroutines();
		}

		IEnumerator Shake(Transform transform, Vector3 originalPosition, float duration, float speed, float magnitude,
						AnimationCurve damper = null)
		{
			float elapsed = 0f;
			while (elapsed < duration)
			{
				elapsed += Time.deltaTime;
				float damperedMag = (damper != null) ? (damper.Evaluate(elapsed / duration) * magnitude) : magnitude;
				float x = (Mathf.PerlinNoise(Time.time * speed + seed.x, 0f) * damperedMag) - (damperedMag / 2f);
				float y = (Mathf.PerlinNoise(0f, Time.time * speed + seed.y) * damperedMag) - (damperedMag / 2f);
				if (minStepSize > 0f)
				{
					x = x - x % minStepSize;
					y = y - y % minStepSize;
				}
				transform.position += new Vector3(x, y);
				yield return null;
			}
			transform.localPosition = originalPosition;
		}

	}


}
