using UnityEngine;
using System.Collections;

public class MagicProjectile : MonoBehaviour {
	public enum CollisionActions {Destroy, Nothing, Stick};

	public CollisionActions collisionAction = CollisionActions.Destroy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void OnCollisionEnter (Collision col) {
		switch (collisionAction) {
		case CollisionActions.Destroy:
			Destroy(gameObject);
			break;
		case CollisionActions.Stick:
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			break;
		}
	}
}
