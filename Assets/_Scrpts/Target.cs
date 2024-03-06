using System.Collections;
using UnityEngine;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace SplatterSystem.TopDown {

	public class Target : MonoBehaviour {
		public MeshSplatterManager splatter;
		public float splatOffset = 0f;
		public shakeCameraTest screenShake;
		public SplatterSettings hitSplatterSettings;

		public Color hitSplatterColor_1 = Color.red;
		public Color hitSplatterColor_2 = Color.red;
		private Color hitSplatterColor;

		public SplatterSettings dieSplatterSettins;

		public Color dieSplatterColor_1 = Color.red;
		public Color dieSplatterColor_2 = Color.red;
		private Color dieSplatterColor;

		public GameObject Pointer;
		private float possibilityOFdroppingPointer = 0.2f;

		[Space(10)]
		public float healthPoints = 100f;

		//private static int numTargets = 0;
		private float shakeMagnitude;
		private float shakeDuration;
		private endpoint end;
		private PlayerController player;

		[SerializeField] private AudioSource audiosource_1;
		[SerializeField] private AudioSource audiosource_2;
		[SerializeField] private AudioSource audiosource_3;

        /*private void Start()
        {
			end = FindObjectOfType<endpoint>();

			if (splatter == null)
			{
				splatter = FindObjectOfType<MeshSplatterManager>();
				return;
			}

			if (screenShake == null)
			{
				screenShake = FindObjectOfType<shakeCameraTest>();
				shakeMagnitude = screenShake.magnitude * healthPoints / 300f;
				shakeDuration = screenShake.duration * healthPoints / 300f;
			}
		}*/
        private void Start()
        {
			player = FindObjectOfType<PlayerController>();
        }

        private void OnEnable()
        {
			end = FindObjectOfType<endpoint>();
			healthPoints = 100;

			if (splatter == null)
			{
				splatter = FindObjectOfType<MeshSplatterManager>();
				return;
			}

			if (screenShake == null)
			{
				screenShake = FindObjectOfType<shakeCameraTest>();
				shakeMagnitude = screenShake.magnitude * healthPoints / 1100f;
				shakeDuration = screenShake.duration * healthPoints / 1100f;
			}

			generateRandomColor();
		}

		private void generateRandomColor()
		{
			float r = Random.Range(hitSplatterColor_1.r, hitSplatterColor_2.r);
			float g = Random.Range(hitSplatterColor_1.g, hitSplatterColor_2.g);
			float b = Random.Range(hitSplatterColor_1.b, hitSplatterColor_2.b);
			float a = Random.Range(hitSplatterColor_1.a, hitSplatterColor_2.a);

			hitSplatterColor = new Color(r, g, b, a);

			float r1 = Random.Range(dieSplatterColor_1.r, dieSplatterColor_2.r);
			float g1 = Random.Range(dieSplatterColor_1.g, dieSplatterColor_2.g);
			float b1 = Random.Range(dieSplatterColor_1.b, dieSplatterColor_2.b);
			float a1 = Random.Range(dieSplatterColor_1.a, dieSplatterColor_2.a);

			dieSplatterColor = new Color(r1, g1, b1, a1);
		}
        public void HandleHit(float damage, Vector2 direction) {
			healthPoints = Mathf.Max(healthPoints - damage, 0f);
			if (healthPoints <= 0f) {
				HandleDeath();
			} 
			else {
				Vector2 hitPos = (Vector2) transform.position + splatOffset * direction;
				splatter.Spawn(hitSplatterSettings, hitPos, direction, hitSplatterColor);
				audiosource_3.PlayOneShot(AudioManager.instance.SpatteSound);
				//add force when being hit
				//GetComponent<Rigidbody2D>().AddForce(direction * 5, ForceMode2D.Impulse);
			}
		}

		private void HandleDeath() {

			splatter.Spawn(dieSplatterSettins, transform.position, transform.position - player.transform.position, dieSplatterColor);
			audiosource_1.PlayOneShot(AudioManager.instance.SpatteSound);
			audiosource_2.PlayOneShot(AudioManager.instance.EnemyDieSound);
			
			if (screenShake != null) {
				screenShake.magnitude = shakeMagnitude;
				screenShake.duration = shakeDuration;
				screenShake.Shake();
			}

			Vector3 endpos = end.transform.position - transform.position;

			float x = Random.Range(0, 1f);
			if (x < possibilityOFdroppingPointer)
			{
				GameObject tmp = Instantiate<GameObject>(Pointer, transform.position, Quaternion.identity);
				tmp.transform.up = endpos;
				audiosource_3.PlayOneShot(AudioManager.instance.GuideDrop);
			}

			gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
			gameObject.GetComponent<Collider2D>().enabled = false;

			StartCoroutine(DisableGameobjectDelay());
			/*
			// If this is last target - restart.
			numTargets--;
			if (numTargets <= 0) {
				StartCoroutine(HandleGameCompleted());
			} else {
				gameObject.SetActive(false);
			}*/
		}

		private IEnumerator DisableGameobjectDelay()
		{
			yield return new WaitForSeconds(1);
			gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
			gameObject.GetComponent<Collider2D>().enabled = true;

			gameObject.SetActive(false);
		}

		private IEnumerator HandleGameCompleted() {
			var renderer = GetComponent<Renderer>();
			renderer.enabled = false;

			yield return new WaitForSeconds(1.0f);
			splatter.FadeOut();

			yield return new WaitForSeconds(2.0f);
			#if UNITY_5_3_OR_NEWER
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			#endif
		}
	}

}