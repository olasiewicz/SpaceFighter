using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

	public GameObject playButton;
	public GameObject exitButton;
	public GameObject playerShip;
	public GameObject enemySpawner;
	public GameObject asteroidSpawner;
	public GameObject scoreTextUIGO;
	public GameObject BackgroundMusicGameObject;
	GameObject backgroundMusic;

	public enum GameManagerState{Opening, GamePlay, GameOver};

	GameManagerState GMState;

	// Use this for initialization
	void Start () {
		GMState = GameManagerState.Opening;
		UpdateGameManagerState ();
		backgroundMusic = (GameObject) Instantiate(BackgroundMusicGameObject);

		//hide game over

		//set play button visible
		//playButton.transform.position = new Vector2(-100, -100);
	}
	
	//function to update gameManager state
	void UpdateGameManagerState(){
		switch (GMState) {

		case GameManagerState.Opening:

			playButton.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2);
			exitButton.transform.position = new Vector2 (Screen.width / 2, Screen.height / 3 + Screen.height / 11);
			playerShip.SetActive (false);

			break;

		case GameManagerState.GamePlay:
			
			//hide start button
			playButton.transform.position = new Vector2(-100, -100);

			//hide exit button
			exitButton.transform.position = new Vector2(-100, -100);

			playerShip.GetComponent<playerController> ().Init ();

			//start enemySpawner and asteroidSpawner
			enemySpawner.GetComponent<enemySpawner>().ScheduleEnemySpawner();
			asteroidSpawner.GetComponent<asteroidSpawner>().ScheduleAsteroidSpawner();

			//reset score
			scoreTextUIGO.GetComponent<score>().ScheduleScore = 0;

			//play backgroud music
			backgroundMusic.GetComponent<AudioSource> ().Play ();
			
			break;

		case GameManagerState.GameOver:

			//stop enemy and asteroid spawner
			enemySpawner.GetComponent<enemySpawner>().UnscheduleEnemySpawner();
			asteroidSpawner.GetComponent<asteroidSpawner>().UnscheduleAsteroidSpawner();
		
			//display game over

			//change status to opening after 5s
			Invoke("ChangeGameManagerStateToOpening", 5f);

			//stop backgroud music
			backgroundMusic.GetComponent<AudioSource> ().Stop ();
			
			break;
		}
	}

	public void setGameManagerState(GameManagerState state){
		GMState = state;
		UpdateGameManagerState ();
	}

	//call after start button click
	public void StartGamePlay1(){
		GMState = GameManagerState.GamePlay;
		UpdateGameManagerState ();
		//Application.Quit();
	}

	//call after exit button click
	public void ExitGameButton(){
		Application.Quit();
	}

	//function to change gameManagerState to opening
	public void ChangeGameManagerStateToOpening(){
		setGameManagerState (GameManagerState.Opening);
	}

}
