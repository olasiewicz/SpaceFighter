using UnityEngine;
using System.Collections;

public class liveBallControler : MonoBehaviour {

	public GameObject ExplosionGameObject; // explosion prefab
	GameObject EnemyGun;
	GameObject player;
	public float speed;
	GameObject scoreText;

	// Use this for initialization
	void Start () {
		speed = Random.Range (1f, 3f);
		scoreText = GameObject.FindGameObjectWithTag("ScoreTextTag");
		player = GameObject.FindGameObjectWithTag("PlayerTag");

	}

	// Update is called once per frame
	void FixedUpdate () {

//		//Get ball current possision
//		Vector2 possisionVector = transform.position;
//
//		//Compute the ball new possision
//		possisionVector = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
//
//		//Set the ball possision
//		transform.position = possisionVector;

		//Bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		if (transform.position.y < min.y) {
			//Destroy ball
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//Detect collision of the ball
		if (((coll.tag == "PlayerTag")) || (coll.tag == "PlayerBulletTag") || (coll.tag == "RocketTag") 
			|| (coll.tag == "AsteroidTag") || (coll.tag == "EnemyTag") || (coll.tag == "SawTag")) {
			RunExplosion ();
			scoreText.GetComponent<score> ().ScheduleScore += 2;
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
