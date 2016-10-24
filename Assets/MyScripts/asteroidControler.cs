using UnityEngine;
using System.Collections;

public class asteroidControler : MonoBehaviour {
	public GameObject ExplosionGameObject; // explosion prefab
	public float speed;
	// Use this for initialization
	void Start () {
		speed = 0.8f;
	}

	// Update is called once per frame
	void FixedUpdate () {

		//Get enemy current possision
		Vector2 possisionVector = transform.position;

		//Compute the asteroid new possision
		possisionVector = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);

		//Set the asteroid possision
		transform.position = possisionVector;

		//Bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		if (transform.position.y < min.y) {
			//Destroy asteroid
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//Detect collision of the asteroid with player ship or bullet
		if ((coll.tag == "PlayerTag") || (coll.tag == "PlayerBulletTag")) {
			RunExplosion ();
			Destroy (gameObject);
		}
	}

	//Method to instatiate an explosion
	void RunExplosion() {
		GameObject explosion = (GameObject)Instantiate (ExplosionGameObject);

		//set position of the explosion
		explosion.transform.position = transform.position;
	}
}
