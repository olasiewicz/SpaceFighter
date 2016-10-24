using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, yMin, yMax;
}

public class playerController : MonoBehaviour {

	public GameObject GameManagerGO;//prefab to GameManager

	public GameObject BulletPlayerGameObject;
	public GameObject bulletPosition1;
	public GameObject bulletPosition2;
	public GameObject ExplosionGameObject;//explosion prefab

	//reference to lives
	public Text LivesUIText;

	const int MaxLives = 3;
	int lives; //current lives

	public float tilt;
	public float moveForce, fireMultiplier = 2;
	Rigidbody2D rigidBody;
	public Boundary boundary;
	bool afterFire;

	//TODO
	public void Init() {
		lives = MaxLives;
		LivesUIText.text = lives.ToString ();

		gameObject.transform.position = new Vector2(0, -2);


		//player visible
		gameObject.SetActive (true);

	}

	// Use this for initialization
	void Start () {
		rigidBody = this.GetComponent<Rigidbody2D> ();
		//Init ();
	}

	void FixedUpdate () {
		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * moveForce;
		bool isFire = CrossPlatformInputManager.GetButton ("Fire");
		bool isTurbo = CrossPlatformInputManager.GetButton ("T");


	
		rigidBody.AddForce (moveVec * (isTurbo ? fireMultiplier : 1));

		//fire bullet, when button fire pressed
		if (isFire && !afterFire) {
			afterFire = true;
			//instantiate the first bullet
			GameObject bullet1 = (GameObject) Instantiate(BulletPlayerGameObject);
			bullet1.transform.position = bulletPosition1.transform.position;

			//instantiate the second bullet
			GameObject bullet2 = (GameObject) Instantiate(BulletPlayerGameObject);
			bullet2.transform.position = bulletPosition2.transform.position;

			//shoot sound
			GetComponent<AudioSource> ().Play ();
		
		}
		if (!isFire) {
			afterFire = false;
		}

		//rigidBody.position = new Vector2 (Mathf.Clamp (rigidBody.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp (rigidBody.position.y, boundary.yMin, boundary.yMax));


		//boundary x
		if (rigidBody.position.x > boundary.xMax) {
			transform.position = new Vector2 (boundary.xMin, rigidBody.position.y);
		}

		if (rigidBody.position.x < boundary.xMin) {
			transform.position = new Vector2 (boundary.xMax, rigidBody.position.y);
		}


		//boundary y
		if (rigidBody.position.y < boundary.yMin) {
			transform.position = new Vector2 (rigidBody.position.x, boundary.yMin);
		}

		if (rigidBody.position.y > boundary.yMax) {
			transform.position = new Vector2 (rigidBody.position.x, boundary.yMax);
		}

		transform.rotation = Quaternion.Euler (0.0f, 0.0f, rigidBody.velocity.x * -tilt);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//Detect collision of the player ship with enemy ship or bullet
		if ((coll.tag == "EnemyTag") || (coll.tag == "EnemyBulletTag") || (coll.tag == "AsteroidTag")) {

			RunExplosion ();

			lives--;
			LivesUIText.text = lives.ToString ();

			gameObject.SetActive (false);

			//set player visible after 1s
			Invoke ("PlayerActiveTrue", 1f);

			if(lives == 0){
				GameManagerGO.GetComponent<gameManager> ().setGameManagerState (gameManager.GameManagerState.GameOver);
				gameObject.SetActive (false);
			}
			//Destroy (gameObject);
		}
	}

	//Method to instatiate an explosion
	void RunExplosion() {
		GameObject explosion = (GameObject)Instantiate (ExplosionGameObject);

		//set position of the explosion
		explosion.transform.position = transform.position;
	}


	void PlayerActiveTrue(){
		if (lives > 0) {
			gameObject.SetActive (true);
		}
	}
}
