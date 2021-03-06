﻿using UnityEngine;
using System.Collections;

public class enemyGun : MonoBehaviour {

	public GameObject EnemyBulletGameObject; //bullet's prefab
	private bool isFire = false;

	public bool IsFire {
		get {
			return isFire;
		}
	}

	// Use this for initialization
	void Start () {
		isFire = false;
		//fire enemy bullet randomly
		Invoke("FireEnemyBullet", 1f);

	}

	// Update is called once per frame
	void Update () {

	}

	//fire enemy bullet
	void FireEnemyBullet() {
		isFire = true;
		//get player reference
		GameObject playerShip = GameObject.Find("PlayerGameObject");

		//if player is still life
		if (playerShip != null) {

			//instantiate enemy bullet
			GameObject bullet = (GameObject)Instantiate(EnemyBulletGameObject);

			//set bullet position
			bullet.transform.position = transform.position;

			//compute the bullet's direction
			Vector2 direction = playerShip.transform.position - bullet.transform.position;

			//set bullet's direction
			bullet.GetComponent<enemyBullet>().setBulletDirection(direction);

		}
	}
}

