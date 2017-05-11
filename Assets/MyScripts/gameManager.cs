using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gameManager : MonoBehaviour {

	public GameObject playButton;
	public GameObject exitButton;
	public GameObject gameOverButton;
	public GameObject highScoresButton;
	public GameObject playerShip;
	public GameObject enemySpawner;
	public GameObject enemySpawner2;
	public GameObject bombSpawner;
	public GameObject asteroidSpawner;
	public GameObject protectorSpawner;
	public GameObject bulletSpawner;
	public GameObject sawSpawner;
	public GameObject liveSpawner;
	public GameObject scoreTextUIGO;
	public GameObject BackgroundMusicGameObject;
	public GameObject bestScoreCount;
	public GameObject onlineHighScores;
	public GameObject nickNameGO;
	public GameObject nickLabelGO;

	GameObject joystick;
	GameObject highScoresCanvas;
	InputField nickEditText;
	GameObject backgroundMusic;
	GameObject[] gameObjects;
	string nickName;
	string uniqueIdentifier;
	Text nickLabel;
	string stringLastID = "";
	bool ifnickExist = false;

	public enum GameManagerState{Preparing, Opening, GamePlay, GameOver};

	GameManagerState GMState;

	// Use this for initialization
	void Start () {
		GMState = GameManagerState.Preparing;
		UpdateGameManagerState ();
		backgroundMusic = (GameObject) Instantiate(BackgroundMusicGameObject);
		joystick = GameObject.FindGameObjectWithTag("JoystickTag");
		highScoresCanvas = GameObject.FindGameObjectWithTag("CanvasTag");
		nickEditText = nickNameGO.GetComponent<InputField> ();
		nickLabel = nickLabelGO.GetComponent<Text> ();
		//highScoresCanvas.SetActive (false);
		onlineHighScores.transform.position = new Vector2 (-5000, -5000);
		nickName = "spacefighter.bplaced.net/nick.php";
		uniqueIdentifier = "spacefighter.bplaced.net/uniqueID.php";
		//hide game over

		//set play button visible
		//playButton.transform.position = new Vector2(-100, -100);
	}
	
	//function to update gameManager state
	void UpdateGameManagerState(){
		switch (GMState) {

		case GameManagerState.Preparing:


			if (!(PlayerPrefs.GetString ("Nick").Equals(""))) {
				GMState = GameManagerState.Opening;
				UpdateGameManagerState ();
			} else {

				nickNameGO.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2 + Screen.height / 11);
				nickLabelGO.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2 + Screen.height / 6);
				playButton.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2);
				exitButton.transform.position = new Vector2 (-100, -100);
				gameOverButton.transform.position = new Vector2 (-100, -100);
				highScoresButton.transform.position = new Vector2 (-100, -100);
				bestScoreCount.transform.position = new Vector2 (-100, -100);
				playerShip.SetActive (false);
			}
			break;	

		case GameManagerState.Opening:

			playButton.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2);
			exitButton.transform.position = new Vector2 (Screen.width / 2, Screen.height / 3 + Screen.height / 11);
			gameOverButton.transform.position = new Vector2 (-100, -100);
			nickNameGO.transform.position = new Vector2 (-100, -100);
			nickLabelGO.transform.position = new Vector2 (-100, -100);
			highScoresButton.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2 + Screen.height / 7);
			bestScoreCount.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2 + Screen.height / 4);
			playerShip.SetActive (false);
			setBestScore ();
			break;

		case GameManagerState.GamePlay:
			
			//hide start button
			playButton.transform.position = new Vector2 (-100, -100);

			//hide exit button
			exitButton.transform.position = new Vector2 (-100, -100);

			bestScoreCount.transform.position = new Vector2 (-100, -100);

			highScoresButton.transform.position = new Vector2 (-100, -100);

			playerShip.GetComponent<playerController> ().Init ();

			//start enemySpawner and asteroidSpawner
			enemySpawner.GetComponent<enemySpawner> ().ScheduleEnemySpawner ();
			enemySpawner2.GetComponent<enemySpawner2> ().ScheduleRocketSpawner ();
			asteroidSpawner.GetComponent<asteroidSpawner> ().ScheduleAsteroidSpawner ();
			bombSpawner.GetComponent<bombSpawner> ().ScheduleBombSpawner ();
			protectorSpawner.GetComponent<protectorSpawner> ().ScheduleProtectionSpawner ();
			bulletSpawner.GetComponent<bulletSpawner> ().ScheduleBulletSpawner ();
			sawSpawner.GetComponent<sawSpawner> ().ScheduleSawSpawner ();
			liveSpawner.GetComponent<liveSpawner> ().ScheduleLiveSpawner ();

			//reset score
			scoreTextUIGO.GetComponent<score>().ScheduleScore = 0;

			//play backgroud music
			backgroundMusic.GetComponent<AudioSource> ().Play ();
			
			break;

		case GameManagerState.GameOver:

			//stop enemy and asteroid spawner
			enemySpawner.GetComponent<enemySpawner>().UnscheduleEnemySpawner();
			enemySpawner2.GetComponent<enemySpawner2>().UnscheduleRocketSpawner();
			asteroidSpawner.GetComponent<asteroidSpawner>().UnscheduleAsteroidSpawner();
			bombSpawner.GetComponent<bombSpawner>().UnscheduleBombSpawner();
			protectorSpawner.GetComponent<protectorSpawner> ().UnscheduleProtectionSpawner ();
			bulletSpawner.GetComponent<bulletSpawner> ().UnscheduleBulletSpawner (); 
			sawSpawner.GetComponent<sawSpawner> ().UnscheduleSawSpawner ();
			liveSpawner.GetComponent<liveSpawner> ().UnscheduleLiveSpawner ();
		
			destroyObjects ("EnemyTag");
			destroyObjects ("AsteroidTag");
			destroyObjects ("RocketTag");
			destroyObjects ("TurboTag");
			destroyObjects ("BombTag");
			destroyObjects ("ShieldTag");
			destroyObjects ("SawTag");
			destroyObjects ("WeaponChargeTag");
			destroyObjects ("LiveBallTag");
			destroyObjects ("LiveShipTag");

			gameOverButton.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2);
			//bestScoreText.GetComponent<score>().BestScoreCount = scoreTextUIGO.GetComponent<score>().ScheduleScore;
			setBestScore();

			//change status to opening after 2s
			Invoke("ChangeGameManagerStateToOpening", 2f);

			//stop backgroud music
			backgroundMusic.GetComponent<AudioSource> ().Stop ();
			playerShip.GetComponent<playerController> ().TurboMusicOff();
			
			break;
		}
	}

	public void setGameManagerState(GameManagerState state){
		GMState = state;
		UpdateGameManagerState ();
	}

	//call after start button click
	public void StartGamePlay1(){
		if (GMState == GameManagerState.Preparing) {


			string stringEditText = nickEditText.text;
			if (stringEditText.Equals("")){
				nickLabel.text = "First Enter Your Nick";
				return;
			}
			ifnickExist = false;
			checkIfNickExist (stringEditText);

		} else {
			GMState = GameManagerState.GamePlay;
			UpdateGameManagerState ();
		}
	}

	//call after exit button click
	public void ExitGameButton(){
		Application.Quit();
	}

	//call after close button click
	public void CloseButtonClick(){
		joystick.SetActive (true);
		onlineHighScores.transform.position = new Vector2 (-5000, -5000);
	}

	//call after high scores button click
	public void HighScoreButtonClick(){
		joystick.SetActive (false);
		//highScoresCanvas.SetActive (true);
		onlineHighScores.transform.position = new Vector2 (0, 0);
		//highScoresCanvas.transform.position = new Vector2 (0, 0);
		onlineHighScores.GetComponent<HSController> ().startGetScores ();
	}

	//function to change gameManagerState to opening
	public void ChangeGameManagerStateToOpening(){
		setGameManagerState (GameManagerState.Opening);
	}

	void destroyObjects (string name)
	{
		gameObjects =  GameObject.FindGameObjectsWithTag (name);

		for (var i = 0; i < gameObjects.Length; i++) {
			gameObjects [i].SetActive(false);

		}
	}

	void setBestScore ()
	{
		int actualResult = scoreTextUIGO.GetComponent<score> ().ScheduleScore;
		int bestScore = PlayerPrefs.GetInt ("bestScore");
		if (PlayerPrefs.HasKey ("bestScore")) {

			if (bestScore >= actualResult) {
				bestScoreCount.GetComponent<score> ().BestCount = bestScore;
			} else {
				PlayerPrefs.SetInt ("bestScore", scoreTextUIGO.GetComponent<score>().ScheduleScore);
				bestScoreCount.GetComponent<score>().BestCount = scoreTextUIGO.GetComponent<score>().ScheduleScore;
			}
		} else {
			PlayerPrefs.SetInt ("bestScore", scoreTextUIGO.GetComponent<score> ().ScheduleScore);
			bestScoreCount.GetComponent<score>().BestCount = scoreTextUIGO.GetComponent<score>().ScheduleScore;
		}
			
		onlineHighScores.GetComponent<HSController> ().updateOnlineHighscoreData ("" + PlayerPrefs.GetInt("MyID") , PlayerPrefs.GetString("Nick"), actualResult);
		if (bestScore < actualResult) {
			onlineHighScores.GetComponent<HSController> ().startPostScores ();
		}
	}


	public void checkIfNickExist(string stringNick1) {
		StartCoroutine(NickExist(stringNick1));
	}

	IEnumerator NickExist(string stringET) {

		Scrolllist.Instance.loading = true;


		WWW hs_getNick = new WWW("http://"+ nickName);

		yield return hs_getNick;

		if (hs_getNick.error != null)
		{
			//Debug.Log("There was an error getting the high score: " + hs_get.error);

		}
		else
		{

			//Change .text into string to use Substring and Split
			string helpNick = hs_getNick.text;

			string[] nicknamesArray = helpNick.Split (";"[0]);

			foreach (string s in nicknamesArray) {
				if (s.Equals (stringET)) {
					nickLabel.text = "Nick already exist. Try again";
					ifnickExist = true;
					break;
				}
			}
			if (ifnickExist == false) {    
				checkLastID ();

				GMState = GameManagerState.Opening;
				UpdateGameManagerState ();
			}

			//Debug.Log("Nick: " +  helpNick);

			Scrolllist.Instance.loading = false;
			Scrolllist.Instance.getScrollEntrys ();

		}

	}





	public void checkLastID() {
		StartCoroutine(LastID());
	}




	IEnumerator LastID (){

		WWW hs_getID = new WWW("http://"+ uniqueIdentifier);

		yield return hs_getID;

		if (hs_getID.error != null)
		{
			//Debug.Log("There was an error getting the high score: " + hs_get.error);

		}
		else
		{

			//Change .text into string to use Substring and Split
			string helpID = hs_getID.text;
			string[] idArray = helpID.Split (';');
			int highestID = 0;
			for (int i = 0; i < idArray.Length; i++) {
				if (i > highestID) {
					highestID = i;
				}
			}
			//stringLastID = idArray [idArray.Length - 2];
			//help= help.Substring(5, hs_get.text.Length-5);
			//200 is maximum length of highscore - 100 Positions (name+score)

			foreach (string s in idArray) {
				Debug.Log("halloLastID: " +  s);
			}

			//onlineHighscore  = helpID.Split(";"[0]);
			Debug.Log("halloLastID: " +  stringLastID);
			//int pomID = int.Parse (stringLastID);
			int myID = highestID + 1;
			Debug.Log("myID: " +  myID);
			PlayerPrefs.SetInt ("MyID", myID);
			PlayerPrefs.SetString ("Nick", nickEditText.text);
		}

	}




}
