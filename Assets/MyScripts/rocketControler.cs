using UnityEngine;
using System.Collections;

public class rocketControler : MonoBehaviour {

	private Vector3 normalizeDirection;
	private Transform target;
	public int rotationSpeed;
	GameObject player;

	//Transform target;
	Transform myTransform;
	public GameObject ExplosionGameObject; // explosion prefab
	float speed;
	GameObject scoreText;

	// Use this for initialization
	void Start () {
		speed = Random.Range (1f, 3f);
		scoreText = GameObject.FindGameObjectWithTag("ScoreTextTag");
		player = GameObject.FindGameObjectWithTag("PlayerTag");

		GameObject playerShip = GameObject.Find("PlayerGameObject");
		if (playerShip != null) {
			target = playerShip.transform;
		}
			
	}


			void FixedUpdate()
			{
		if (target != null) {
			Vector3 vectorToTarget = target.position - transform.position;
			float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * rotationSpeed);

			normalizeDirection = (target.position - transform.position).normalized;
			transform.position += normalizeDirection * speed * Time.deltaTime;
		} else {
			Destroy (gameObject);
		}

			//Bottom - left point of the screen
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

			if (transform.position.y < min.y) {
				//Destroy enemy
				Destroy (gameObject);
			}
			
	}


	void OnTriggerEnter2D(Collider2D coll) {
		//Detect collision of the enemy ship with player ship or bullet
		if ((coll.tag == "PlayerTag") || (coll.tag == "PlayerBulletTag") || (coll.tag == "EnemyTag") 
			|| (coll.tag == "AsteroidTag") || (coll.tag == "EnemyBulletTag") || (coll.tag == "RocketTag")) {
				RunExplosion ();
				scoreText.GetComponent<score> ().ScheduleScore += 3;
		
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
