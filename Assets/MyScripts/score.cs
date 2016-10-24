using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class score : MonoBehaviour {

	Text scoreText;
	int scoreCount;

	public int ScheduleScore
	{
		get 
		{
			return this.scoreCount;
		}
		set 
		{
			this.scoreCount = value;
			UpdateScoreText ();
		}
	}

	void  Start(){
	
		scoreText = GetComponent<Text> ();

	}

	//function to update score text
	void UpdateScoreText(){
		string scoreString = "" + scoreCount;
		scoreText.text = scoreString;
	}

}
