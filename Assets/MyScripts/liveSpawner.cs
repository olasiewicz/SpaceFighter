using UnityEngine;
using System.Collections;

public class liveSpawner : MonoBehaviour {

	public GameObject liveGameObject;//prefab
	float maxSpawnRateInSecond  =70f;

	//Method to Spawn the lives
	void spawnLive(){

		//Bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

		//Top - right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		//Instantiate the protection
		GameObject live = (GameObject)Instantiate(liveGameObject);
		live.transform.position = new Vector2 ((max.x + 0.5f), Random.Range (max.y - 4f, max.y - 0.3f));

		ScheduleNextLive ();
	}

	void ScheduleNextLive(){
		float spawnInSecond;
		if (maxSpawnRateInSecond > 30f) {
			spawnInSecond = Random.Range (40f, maxSpawnRateInSecond);

		} else {
			spawnInSecond = 30f;//1f;
		}

		Invoke ("spawnLive", spawnInSecond);
	}

	//Increase difficulty of the game
	void IncreaseSpawnRate() {

		if (maxSpawnRateInSecond > 1f) {
			maxSpawnRateInSecond = maxSpawnRateInSecond - 0.4f; //enemy all the time  //maxSpawnRateInSecond--
		}

		if (maxSpawnRateInSecond == 1f) {
			CancelInvoke ("spawnLive");
			//maxSpawnRateInSecond = 5f;
		}

	}

	//function to start bomb spawner
	public void ScheduleLiveSpawner(){
		Invoke ("spawnLive", 50f);

		//Increase spawn rate every 30s
		InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
	}


	//function to stop bomb spawner
	public void UnscheduleLiveSpawner(){
		CancelInvoke ("spawnLive");
		CancelInvoke ("IncreaseSpawnRate");
	}
}
