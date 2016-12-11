using UnityEngine;
using System.Collections;

public class playerBullet : MonoBehaviour {

	float speed;

	// Use this for initialization
	void Start () {

		speed = 8f;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Bullet current possision
		Vector2 posision = transform.position;

		//Compute bullet's new possision
		posision = new Vector2(posision.x, posision.y + speed * Time.deltaTime);

		//Update the bullet's position
		transform.position = posision;

		//Top - right point of the screen
		Vector2 maxPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		if (transform.position.y > maxPosition.y) {
			Destroy (gameObject);
		}
			
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//Detect collision of the player bullet with enemy
		if ((coll.tag == "EnemyTag") || (coll.tag == "AsteroidTag") || (coll.tag == "RocketTag")) {
			Destroy (gameObject);
		}
	}
}
