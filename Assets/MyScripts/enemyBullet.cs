using UnityEngine;
using System.Collections;

public class enemyBullet : MonoBehaviour {

	float speed = 5;//bullet speed
	Vector2 bulletDirection; //bullet direction
	bool isDirectionSet = false; // is bullet direction set

	
	// Update is called once per frame
	void Update () {
		if (isDirectionSet) {
			//get bullet's current position
			Vector2 position = transform.position;

			//compute bullet's new position
			position += bulletDirection * speed *Time.deltaTime;

			//update bullet's position
			transform.position = position;

			//remove bullet when is out of the screen

			//bottom-left point of the screen
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

			//top-right point of the screen
			Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

			if ((transform.position.x < min.x) || (transform.position.x > max.x) || (transform.position.y < min.y) || (transform.position.y > max.y)) {
				Destroy (gameObject);
			}
		}
	}

	//this method set bullet's direction
	public void setBulletDirection(Vector2 direction) {
		bulletDirection = direction.normalized;
		isDirectionSet = true;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//Detect collision of the enemy bullet with player
		if (coll.tag == "PlayerTag") {
			Destroy (gameObject);
		}
	}
}
