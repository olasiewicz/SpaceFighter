using UnityEngine;
using System.Collections;

public class bulletController : MonoBehaviour {

	GameObject[] gameObjects;
	public float speed;
	GameObject player;


	// Use this for initialization
	void Start () {
		speed = Random.Range (1f, 3f);
		player = GameObject.FindGameObjectWithTag("PlayerTag");
	}

	// Update is called once per frame
	void FixedUpdate () {

		//Get enemy current possision
		Vector2 possisionVector = transform.position;

		//Compute the enemy new possision
		possisionVector = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);

		//Set the enemy possision
		transform.position = possisionVector;

		//Bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		if (transform.position.y < min.y) {
			//Destroy enemy
			Destroy (gameObject);
		}
	}



	void OnTriggerEnter2D(Collider2D coll) {
		//Detect collision of the enemy ship with player ship or bullet
		if (coll.tag == "PlayerTag") {
			Destroy (gameObject);
			player.GetComponent<playerController> ().BulletCounter = 60;
			player.GetComponent<playerController> ().BulletScoreController(60);
		}


	}

}
