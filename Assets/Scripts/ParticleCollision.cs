using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(ParticleSystem))]
public class ParticleCollision : MonoBehaviour {

	public GameObject rainDropPrefab;
	private List<Animator> drops;
	private ParticleSystem particles;
	private ParticleCollisionEvent[] collisionEvents;

	void Start() {
		particles = GetComponent<ParticleSystem> ();
		drops = new List<Animator> ();
	}

	void Update() {

		if (!particles.emission.enabled)
			return;
		
		List<Animator> toDelete = new List<Animator> ();
		foreach (Animator d in drops) {
			if (d.GetCurrentAnimatorStateInfo (0).IsName ("ReadyToDestroy"))
				toDelete.Add (d);
		}

		foreach (Animator d in toDelete) {
			Destroy (d.gameObject);
			drops.Remove (d);
		}
		toDelete.Clear ();
	}

	void OnParticleCollision(GameObject other) {
		collisionEvents = new ParticleCollisionEvent[16];
		int numCollisionEvents = particles.GetCollisionEvents (other, collisionEvents);
		int i = 0;
		while (i < numCollisionEvents) {
			Vector3 collisionHitLoc = collisionEvents [i].intersection;
			GameObject drop = Instantiate (rainDropPrefab) as GameObject;
			drop.transform.position = Vector3.Max(collisionHitLoc, new Vector3(-1000,-1000,-1000));
			drop.transform.SetParent (transform.parent);
			drops.Add (drop.GetComponent<Animator>());
			i++;
		}
	}

}
