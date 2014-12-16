using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class MagicController : MonoBehaviour {
	// Myo game object to connect with.
	// This object must have a ThalmicMyo script attached.
	public GameObject myo = null;

	// Point from where magic projectiles will launch from the hand
	// This object must be a plane placed either in front of the palm or at the tip of the hand's fingers
	public GameObject handMagicLauncher;

	public GameObject fistProjectile;
	public GameObject waveInProjectile;
	public GameObject waveOutProjectile;
	
	// Speed of projectile
	public float fistSpeed = 1000;
	public float waveInSpeed = 1000;
	public float waveOutSpeed = 1000;

	// Cooldown until next projectile can be launched
	public float projectileCooldown = 1;

	protected ThalmicMyo myoController = null;
	
	private float projectileTimer = 0;

	void Awake () {

	}

	// Use this for initialization
	void Start () {
		myoController = myo.GetComponent<ThalmicMyo> ();
	}

	void Update () {
		if (!myo && !myoController)
			return;
		
		if (projectileTimer > 0) {
			projectileTimer -= Time.deltaTime;
			return;
		} else {
			projectileTimer = projectileCooldown;
		}

		switch (myoController.pose) {
		case Pose.Fist:
			Attack (fistProjectile, fistSpeed);
			break;
		case Pose.WaveIn:
			Attack (waveInProjectile, waveInSpeed);
			break;
		case Pose.WaveOut:
			Attack (waveOutProjectile, waveOutSpeed);
			break;
		}
	}

	protected void Attack(GameObject projectilePrefab, float speed) {
		GameObject projectile;

		projectile = (GameObject) Instantiate (projectilePrefab.gameObject, handMagicLauncher.transform.position, Quaternion.identity);
		projectile.rigidbody.AddForce (handMagicLauncher.gameObject.transform.forward * speed);
	}
}
