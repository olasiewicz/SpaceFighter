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

	public GameObject BulletPlayerGameObj;
	public GameObject bulletPosition1;
	public GameObject bulletPosition2;
	public GameObject ExplosionGameObject;//explosion prefab
	GameObject[] gameObjects;
	GameObject protector;
	GameObject shield;
	GameObject turboFire;
	GameObject clockProtection;
	GameObject playerTurboMusic;
	AudioSource shootAudio;
	AudioSource shieldAudio;
	AudioSource weaponChargeAudio;
	GameObject bulletScore1;
	GameObject bulletScore2;
	GameObject bulletScore3;

	private bool isPlayerProtected = false;
	int counter;
	private int bulletCounter;

	public int BulletCounter {
		get {
			return bulletCounter;
		}
		set {
			bulletCounter = value;
		}
	}

	bool isAllowToFire;

	public bool IsPlayerProtected {
		get {
			return isPlayerProtected;
		}
		set {
			isPlayerProtected = value;
		}
	}

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
		LivesUIText.text = "" + 3;

		gameObject.transform.position = new Vector2(0, -2);

		//player visible
		gameObject.SetActive (true);
		bulletScore3.SetActive (true);
		bulletScore2.SetActive (true);
		bulletScore1.SetActive (true);
		isAllowToFire = true;
		bulletCounter = 60;
		protector.SetActive (true);
		clockProtection.GetComponent<clockController> ().InitClock (3);
		Invoke ("setIsNotPlayerProtected", 3f);
	}

	// Use this for initialization
	void Start () {

		AudioSource[] audio = GetComponents<AudioSource> ();
		shootAudio = audio [0];
		shieldAudio = audio [1];
		weaponChargeAudio = audio [2];
		rigidBody = this.GetComponent<Rigidbody2D> ();
		protector = GameObject.FindGameObjectWithTag("ProtectorTag");
		shield = GameObject.FindGameObjectWithTag("ShieldTag");
		turboFire = GameObject.FindGameObjectWithTag("TurboTag");
		clockProtection = GameObject.FindGameObjectWithTag("ClockTag");
		bulletScore1 = GameObject.FindGameObjectWithTag("BulletScore1Tag");
		bulletScore2 = GameObject.FindGameObjectWithTag("BulletScore2Tag");
		bulletScore3 = GameObject.FindGameObjectWithTag("BulletScore3Tag");
		turboFire.SetActive (true);
	}


	void FixedUpdate () {
		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * moveForce;
		bool isFire = CrossPlatformInputManager.GetButton ("Fire");
		bool isTurbo = CrossPlatformInputManager.GetButton ("T");
	
		rigidBody.AddForce (moveVec * (isTurbo ? fireMultiplier : 1));

		if (isTurbo) {
			turboFire.SetActive (true);
		} else {
			turboFire.SetActive (false);
			}

		//fire bullet, when button fire pressed
		if (isFire && !afterFire && isAllowToFire) {
			afterFire = true;
			//instantiate the first bullet
			GameObject bullet1 = (GameObject) Instantiate(BulletPlayerGameObj);
			bullet1.transform.position = bulletPosition1.transform.position;
			bulletCounter--;

			BulletScoreController (bulletCounter);

			//instantiate the second bullet
			GameObject bullet2 = (GameObject) Instantiate(BulletPlayerGameObj);
			bullet2.transform.position = bulletPosition2.transform.position;

			//shoot sound
			shootAudio.Play ();
		
		}
		if (!isFire) {
			afterFire = false;
		}

	
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
		if (((coll.tag == "EnemyTag") || (coll.tag == "EnemyBulletTag") || (coll.tag == "AsteroidTag") || (coll.tag == "RocketTag") 
			|| (coll.tag == "BombTag") || (coll.tag == "SawTag"))) {

			if (!isPlayerProtected) {

				RunExplosion ();
				lives--;
				LivesUIText.text = lives.ToString ();
				gameObject.SetActive (false);
				destroyObjects ("RocketTag");
		
				//set player visible after 1s
				Invoke ("PlayerActiveTrue", 1f);

				if (lives == 0) {
					GameManagerGO.GetComponent<gameManager> ().setGameManagerState (gameManager.GameManagerState.GameOver);
					gameObject.SetActive (false);
				}
				//Destroy (gameObject);
			}
		}

		if ((coll.tag == "ShieldTag") && !isPlayerProtected) {
			//shoot sound
			shieldAudio.Play ();
			isPlayerProtected = true;
			clockProtection.GetComponent<clockController> ().InitClock (10);
			protector.SetActive (true);
			Invoke ("setIsNotPlayerProtected", 10f);
		}

		if ((coll.tag == "WeaponChargeTag")) {
			weaponChargeAudio.Play ();
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
			isPlayerProtected = true;	
			clockProtection.GetComponent<clockController> ().InitClock (3);
			protector.SetActive (true);
			counter = 0;
		
				//Invoke ("Invisible", 0.3f);

			Invoke ("setIsNotPlayerProtected", 3f);

		}
	}

	void destroyObjects (string name)
	{
		gameObjects = GameObject.FindGameObjectsWithTag (name);

		for (var i = 0; i < gameObjects.Length; i++) {
			Destroy (gameObjects [i]);
		}
	}

	void setIsNotPlayerProtected(){
		isPlayerProtected = false;
		protector.SetActive (false);
	}
		


		

	void DisappearanceLogic() 
	{
		if(gameObject.activeSelf) 
		{
			gameObject.SetActive (false);
		}
		else
		{
			gameObject.SetActive (true);
		}
	}
		

	void Invisible(){
		gameObject.SetActive (false);

			Invoke ("Visible", 0.3f);
			counter++;
	}

	void Visible(){
		gameObject.SetActive (true);
		if (counter < 6) {
			Invoke ("Invisible", 0.3f);
		}
	}

	public void TurboMusicOff() {
		turboFire.GetComponent<AudioSource> ().Stop ();
	}

	public void BulletScoreController (int bulletCounter)
	{
		 if (bulletCounter <= 60 && bulletCounter >= 40) {
			bulletScore3.SetActive (true);
			bulletScore2.SetActive (true);
			bulletScore1.SetActive (true);
			isAllowToFire = true;
		} else if (bulletCounter < 40 && bulletCounter >= 20) {
			bulletScore3.SetActive (false);
			bulletScore2.SetActive (true);
			bulletScore1.SetActive (true);
			isAllowToFire = true;
		} else if (bulletCounter < 20 && bulletCounter > 0) {
			bulletScore3.SetActive (false);
			bulletScore2.SetActive (false);
			bulletScore1.SetActive (true);
			isAllowToFire = true;
		} else {
			bulletScore3.SetActive (false);
			bulletScore2.SetActive (false);
			bulletScore1.SetActive (false);
			isAllowToFire = false;
		}
	}

}
