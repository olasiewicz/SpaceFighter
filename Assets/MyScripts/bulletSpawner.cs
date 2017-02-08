using UnityEngine;
using System.Collections;

public class bulletSpawner : MonoBehaviour {

	public GameObject bulletGameObject;//prefab
	float maxSpawnRateInSecond  =90f;

	//Method to Spawn the bomb
	void spawnBullet(){

		//Bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

		//Top - right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		//Instantiate the protection
		GameObject bullet = (GameObject)Instantiate(bulletGameObject);
		bullet.transform.position = new Vector2 (Random.Range (min.x + 0.3f, max.x - 0.3f), max.y);

		ScheduleNextBullet ();
	}

	void ScheduleNextBullet(){
		float spawnInSecond;
		if (maxSpawnRateInSecond > 30f) {
			spawnInSecond = Random.Range (30f, maxSpawnRateInSecond);

		} else {
			spawnInSecond = 30f;//1f;
		}

		Invoke ("spawnBullet", spawnInSecond);
	}

	//Increase difficulty of the game
	void IncreaseSpawnRate() {

		if (maxSpawnRateInSecond > 1f) {
			maxSpawnRateInSecond = maxSpawnRateInSecond - 0.4f; //enemy all the time  //maxSpawnRateInSecond--
		}

		if (maxSpawnRateInSecond == 1f) {
			CancelInvoke ("spawnBullet");
			//maxSpawnRateInSecond = 5f;
		}

	}

	//function to start bomb spawner
	public void ScheduleBulletSpawner(){
		Invoke ("spawnBullet", 40f);

		//Increase spawn rate every 30s
		InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
	}


	//function to stop bomb spawner
	public void UnscheduleBulletSpawner(){
		CancelInvoke ("spawnBullet");
		CancelInvoke ("IncreaseSpawnRate");
	}
}
