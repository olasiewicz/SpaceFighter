using UnityEngine;
using System.Collections;

public class asteroidControler : MonoBehaviour {
	public GameObject ExplosionGameObject; // explosion prefab
	public float speed;
	GameObject scoreText;
	int shootCount;
	private ParticleSystem ps;
	ParticleSystem.EmissionModule em;
	// Use this for initialization
	void Start () {
		speed = 0.8f;
		shootCount = 0;
		scoreText = GameObject.FindGameObjectWithTag("ScoreTextTag");
		ps = GetComponent<ParticleSystem>();
		ps.Stop();
		em = ps.emission;
		em.enabled = false;
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
		if ((coll.tag == "PlayerTag") || (coll.tag == "PlayerBulletTag") || (coll.tag == "SawTag")) {
			RunExplosion ();
			shootCount++;

			if (shootCount == 2) {
				ps.Play();
				em.enabled = true;
			} else if (shootCount > 5){ 
				scoreText.GetComponent<score> ().ScheduleScore += 5;
				Destroy (gameObject);
				shootCount = 0;
			}
		}
	}

	//Method to instatiate an explosion
	void RunExplosion() {
		GameObject explosion = (GameObject)Instantiate (ExplosionGameObject);

		//set position of the explosion
		explosion.transform.position = transform.position;
	}
}
