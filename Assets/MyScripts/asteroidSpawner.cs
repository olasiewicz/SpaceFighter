using UnityEngine;
using System.Collections;

public class asteroidSpawner : MonoBehaviour {

	public GameObject asteroidGameObject;//asteroid prefab
	float maxSpawnRateInSecond  =10f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	//Method to Spawn the enemy
	void spawnAsteroid(){

		//Bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

		//Top - right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		//Instantiate the enemy
		GameObject enemy = (GameObject)Instantiate(asteroidGameObject);
		enemy.transform.position = new Vector2 (Random.Range (min.x + 0.4f, max.x - 0.4f), max.y);

		Invoke ("spawnAsteroid", maxSpawnRateInSecond);
	}

	//function to start asteroid spawner
	public void ScheduleAsteroidSpawner(){
		maxSpawnRateInSecond  =10f;
		Invoke ("spawnAsteroid", maxSpawnRateInSecond);
	}


	//function to stop enemy spawner
	public void UnscheduleAsteroidSpawner(){
		CancelInvoke ("spawnAsteroid");
	}
		
}
