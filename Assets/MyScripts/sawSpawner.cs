using UnityEngine;
using System.Collections;

public class sawSpawner : MonoBehaviour {

	public GameObject sawGameObject;//prefab
	float maxSpawnRateInSecond  =90f;

	//Method to Spawn the bomb
	void spawnSaw(){

		//Bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

		//Top - right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		//Instantiate the protection
		GameObject bullet = (GameObject)Instantiate(sawGameObject);
		bullet.transform.position = new Vector2 (Random.Range (min.x + 0.5f, max.x - 0.5f), max.y);

		ScheduleNextSaw ();
	}

	void ScheduleNextSaw(){
		float spawnInSecond;
		if (maxSpawnRateInSecond > 30f) {
			spawnInSecond = Random.Range (30f, maxSpawnRateInSecond);

		} else {
			spawnInSecond = 30f;//1f;
		}

		Invoke ("spawnSaw", spawnInSecond);
	}

	//Increase difficulty of the game
	void IncreaseSpawnRate() {

		if (maxSpawnRateInSecond > 1f) {
			maxSpawnRateInSecond = maxSpawnRateInSecond - 0.4f; //enemy all the time  //maxSpawnRateInSecond--
		}

		if (maxSpawnRateInSecond == 1f) {
			CancelInvoke ("spawnSaw");
			//maxSpawnRateInSecond = 5f;
		}

	}

	//function to start bomb spawner
	public void ScheduleSawSpawner(){
		Invoke ("spawnSaw", 20f);

		//Increase spawn rate every 30s
		InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
	}


	//function to stop bomb spawner
	public void UnscheduleSawSpawner(){
		CancelInvoke ("spawnSaw");
		CancelInvoke ("IncreaseSpawnRate");
	}
}
