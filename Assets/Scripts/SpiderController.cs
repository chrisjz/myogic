using UnityEngine;
using System.Collections;

public class SpiderController : MonoBehaviour {
	public int health = 1;							// Enemy health
	public Vision vision = Vision.FOV;
	public float fovDistance = 15;					// How far the enemy can see
	public float attackDistance = 1;				// Distance to player until enemy starts attacking
	public float followPlayerSpeed = 12;			// Speed enemy follows player

	// Animations
	public string walkAnimationName = "walk";
	public string runAnimationName = "run";
	public string attackAnimationName = "attack1";
	public string hitAnimationName = "hit1";
	public string deathAnimationName = "death1";

	public enum Vision {FOV, Radius};
	
	protected NavMeshAgent agent;
	protected GameObject player;
	protected RaycastHit hit;
	protected Movement movement;
	protected float defaultSpeed;
	protected bool dead; 							// If enemy is dead

	// These are all the movement types that the enemy can do
	protected enum Movement{Attack, Follow, Freeze};

	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();
		player = GameObject.FindWithTag("Player");
		
		dead = false;
		defaultSpeed = agent.speed;
		movement = Movement.Freeze;
	}

	void Update () {
		if (!player || dead) {
			return;
		}

		// Check if player is within the enemy's field of view
		IsObjectInViewByTag("Player");

		if (movement == Movement.Follow)
			FollowPlayer ();
		else if (movement == Movement.Attack)
			Attack ();
		else
			Freeze ();
	}
	
	protected void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Projectile")
			Damage (1);
	}
	
	// Check if certain object is in view of enemy. Object identified by its tag
	protected void IsObjectInViewByTag( string tag) {
		float distance = fovDistance + 1;

		if (vision == Vision.FOV && Physics.Raycast (transform.position, this.transform.forward, out hit) && hit.transform.tag == tag) {
			distance = hit.distance;
		} else if (vision == Vision.Radius) {
			distance = Vector3.Distance(player.transform.position, transform.position);
		}

		if (distance <= attackDistance)
			movement = Movement.Attack;
		else if (distance <= fovDistance)
			movement = Movement.Follow;
	}

	protected void Damage (int amount) {
		health -= amount;

		if (health <= 0)
			StartCoroutine(Death ());
	}

	protected IEnumerator Death () {
		dead = true;
		animation.Play (deathAnimationName);
		agent.Stop ();
		collider.enabled = false;
		yield return new WaitForSeconds(deathAnimationName.Length);
		Destroy (gameObject);
	}
	
	protected virtual void FollowPlayer () {
		animation.Play (walkAnimationName);
		agent.SetDestination (player.transform.position);
		agent.speed = followPlayerSpeed;
	}

	protected virtual void Freeze () {
		agent.speed = defaultSpeed;
	}

	protected void Attack () {
		animation.Play (attackAnimationName);
	}
}
