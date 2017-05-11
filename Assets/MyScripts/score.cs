using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class score : MonoBehaviour {

	Text scoreText;
	int scoreCount;
	Text livesText;
	int livesCount;
	int bestCount;

	public int BestCount {
		get {
			return bestCount;
		}
		set {
			bestCount = value;
			UpdateBestScoreText ();
		}
	}

	Text bestScoreCount;


	public int LivesCount {
		get {
			return livesCount;
		}
		set {
			livesCount = value;
			UpdateLivesText ();
		}
	}

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
		livesText = GetComponent<Text> ();
		bestScoreCount = GetComponent<Text> ();

	}

	//function to update score text
	void UpdateScoreText(){
		string scoreString = "" + scoreCount;
		scoreText.text = scoreString;
	}

	void UpdateLivesText(){
		string livesString = "" + livesCount;
		livesText.text = livesString;
	}

	public void UpdateBestScoreText() {
		if (bestScoreCount != null) {
			bestScoreCount.text = "YOUR BEST SCORE: " + bestCount;
		} else {
			Invoke ("UpdateBestScoreText", 0.1f);
		}
	}   

}
