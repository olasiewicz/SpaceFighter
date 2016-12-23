using UnityEngine;
using System.Collections;

public class protectorSpawner : MonoBehaviour {

	public GameObject protectorGameObject;//prefab
	float maxSpawnRateInSecond  =90f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	//Method to Spawn the bomb
	void spawnProtection(){

		//Bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

		//Top - right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		//Instantiate the protection
		GameObject protector = (GameObject)Instantiate(protectorGameObject);
		protector.transform.position = new Vector2 (Random.Range (min.x + 0.3f, max.x - 0.3f), max.y);

		ScheduleNextProtection ();
	}

	void ScheduleNextProtection(){
		float spawnInSecond;
		if (maxSpawnRateInSecond > 30f) {
			spawnInSecond = Random.Range (30f, maxSpawnRateInSecond);

		} else {
			spawnInSecond = 30f;//1f;
		}

		Invoke ("spawnProtection", spawnInSecond);
	}

	//Increase difficulty of the game
	void IncreaseSpawnRate() {

		if (maxSpawnRateInSecond > 1f) {
			maxSpawnRateInSecond = maxSpawnRateInSecond - 0.4f; //enemy all the time  //maxSpawnRateInSecond--
		}

		if (maxSpawnRateInSecond == 1f) {
			CancelInvoke ("spawnProtection");
			//maxSpawnRateInSecond = 5f;
		}

	}

	//function to start bomb spawner
	public void ScheduleProtectionSpawner(){
		maxSpawnRateInSecond = 5f;
		Invoke ("spawnProtection", maxSpawnRateInSecond);

		//Increase spawn rate every 30s
		InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
	}


	//function to stop bomb spawner
	public void UnscheduleProtectionSpawner(){
		CancelInvoke ("spawnProtection");
		CancelInvoke ("IncreaseSpawnRate");
	}
}

