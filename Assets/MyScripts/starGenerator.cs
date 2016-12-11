using UnityEngine;
using System.Collections;

public class starGenerator : MonoBehaviour {

	public GameObject starGameObject;// starGameObject prefab
	public int maxStars; //number of stars

	//Aray of colors
	Color[] starColors = {
		new Color(0.5f, 0.5f, 1f), //blue
		new Color(0, 1f, 1f), //green
		new Color(1f, 1f, 0), //yellow
	};

	// Use this for initialization
	void Start () {

		//bottom - left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2(0, 0));

		//top - right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2(1, 1));

		//loop to create the stars

		for(int i = 0; i < maxStars; i++) {
			GameObject star = (GameObject)Instantiate (starGameObject);

			//set the star color
			star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

			//set random position of the star
			star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

			//star size
			float starSize = Random.Range (0.1f, 0.5f);
			star.transform.localScale = new Vector2 (starSize, starSize);


			//set random speed for the star
			star.GetComponent<star>().speed = -(1f * Random.Range(0.1f,0.7f));

			//make the star a child of the starGeneratorGameObject
			star.transform.parent = transform;


		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
