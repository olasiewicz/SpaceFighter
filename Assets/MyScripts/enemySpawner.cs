using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {

	public GameObject enemyGameObject;//prefab
	float maxSpawnRateInSecond  =3f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Method to Spawn the enemy
	void spawnEnemy(){

		//Bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

		//Top - right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		//Instantiate the enemy
		GameObject enemy = (GameObject)Instantiate(enemyGameObject);
		enemy.transform.position = new Vector2 (Random.Range (min.x + 0.3f, max.x - 0.3f), max.y);

		ScheduleNextEnemy ();
	}

	void ScheduleNextEnemy(){
		float spawnInSecond;
		if (maxSpawnRateInSecond > 1f) {
			spawnInSecond = Random.Range (1f, maxSpawnRateInSecond);

		} else {
			spawnInSecond = 1f;//1f;
		}

		Invoke ("spawnEnemy", spawnInSecond);
	}

	//Increase difficulty of the game
	void IncreaseSpawnRate() {

		if (maxSpawnRateInSecond > 1f) {
			maxSpawnRateInSecond = maxSpawnRateInSecond - 0.4f; //enemy all the time  //maxSpawnRateInSecond--
		}

		if (maxSpawnRateInSecond == 1f) {
			CancelInvoke ("spawnEnemy");
			//maxSpawnRateInSecond = 5f;
		}

	}

	//function to start enemy spawner
	public void ScheduleEnemySpawner(){
		maxSpawnRateInSecond = 5f;
		Invoke ("spawnEnemy", 3f);

		//Increase spawn rate every 30s
		InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
	}


	//function to stop enemy spawner
	public void UnscheduleEnemySpawner(){
		CancelInvoke ("spawnEnemy");
		CancelInvoke ("IncreaseSpawnRate");
	}
}
