using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class clockController : MonoBehaviour {

	public Text clockText;
	private float myTimer;
	private bool clockIsVisible = true;

	public bool ClockIsVisible {
		get {
			return clockIsVisible;
		}
		set {
			clockIsVisible = value;
		}
	}
		

	// Use this for initialization
	void Start () {
		
			clockText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ClockIsVisible) {
			myTimer -= Time.deltaTime;
			clockText.text = myTimer.ToString ("f0");
			if (myTimer <= 0) {
				myTimer = 0;
				gameObject.SetActive (false);
			}
		}else {
			gameObject.SetActive (false);
		}
	}

	public void InitClock(float timer){
		this.myTimer = timer;
		gameObject.SetActive (true);
	}


}
