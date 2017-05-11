using UnityEngine;
using System.Collections;

public class liveShipController : MonoBehaviour {

	public GameObject ExplosionGameObject; // explosion prefab
	public GameObject liveBallGameObject;
	GameObject EnemyGun;
	GameObject player;
	public float speed;
	GameObject livesText;
	private bool isShoot;

	// Use this for initialization
	void Start () {
		speed = Random.Range (1f, 1f);
		player = GameObject.FindGameObjectWithTag("PlayerTag");
		liveBallGameObject = GameObject.FindGameObjectWithTag("LiveBallTag");
		livesText = GameObject.FindGameObjectWithTag("TextLivesTag");
		isShoot = false;

	}

	// Update is called once per frame
	void FixedUpdate () {

				//Get ball current possision
				Vector2 possisionVector = transform.position;

		if (!isShoot) {
			//Compute the ball new possision
			possisionVector = new Vector2 (transform.position.x - speed * Time.deltaTime, transform.position.y);
		} else {
			possisionVector = new Vector2 (transform.position.x, transform.position.y - speed * Time.deltaTime);
		}
		
				//Set the ball possision
				transform.position = possisionVector;

		//Bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		if (transform.position.x < min.x) {
			//Destroy ball and live
			Destroy (gameObject);
			Destroy (liveBallGameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//Detect collision of the ball
		if (coll.tag == "PlayerBulletTag")
			 {
			if (!isShoot) {
				RunExplosion ();
			}
			Destroy (liveBallGameObject);
			isShoot = true;
		}
		if (coll.tag == "PlayerTag") {
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
