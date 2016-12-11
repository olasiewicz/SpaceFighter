using UnityEngine;
using System.Collections;

public class bombSpawner : MonoBehaviour {

	public GameObject bombGameObject;//prefab
	float maxSpawnRateInSecond  =50f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	//Method to Spawn the bomb
	void spawnBomb(){

		//Bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

		//Top - right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		//Instantiate the bomb
		GameObject bomb = (GameObject)Instantiate(bombGameObject);
		bomb.transform.position = new Vector2 (Random.Range (min.x + 0.3f, max.x - 0.3f), max.y);

		ScheduleNextBomb ();
	}

	void ScheduleNextBomb(){
		float spawnInSecond;
		if (maxSpawnRateInSecond > 30f) {
			spawnInSecond = Random.Range (30f, maxSpawnRateInSecond);

		} else {
			spawnInSecond = 30f;//1f;
		}

		Invoke ("spawnBomb", spawnInSecond);
	}

	//Increase difficulty of the game
	void IncreaseSpawnRate() {

		if (maxSpawnRateInSecond > 1f) {
			maxSpawnRateInSecond = maxSpawnRateInSecond - 0.4f; //enemy all the time  //maxSpawnRateInSecond--
		}

		if (maxSpawnRateInSecond == 1f) {
			CancelInvoke ("spawnBomb");
			//maxSpawnRateInSecond = 5f;
		}

	}

	//function to start bomb spawner
	public void ScheduleBombSpawner(){
		maxSpawnRateInSecond = 5f;
		Invoke ("spawnBomb", maxSpawnRateInSecond);

		//Increase spawn rate every 30s
		InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
	}


	//function to stop bomb spawner
	public void UnscheduleBombSpawner(){
		CancelInvoke ("spawnBomb");
		CancelInvoke ("IncreaseSpawnRate");
	}
}
