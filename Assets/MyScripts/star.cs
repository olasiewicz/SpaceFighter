using UnityEngine;
using System.Collections;

public class star : MonoBehaviour {


	public float speed; //speed of the star

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

		//get current possision of the star
		Vector2 position = transform.position;

		//compute star's new position
		position = new Vector2(position.x, position.y + speed * Time.deltaTime);

		//update the star's position
		transform.position = position;

		//bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2(0, 0));

		//top - right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2(1, 1));

		//when star goes outside bottom of the screen, then set randomly position on top of the screen
		if (transform.position.y < min.y) {
			transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
		}
	
	}
}
