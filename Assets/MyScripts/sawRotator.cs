using UnityEngine;
using System.Collections;

public class sawRotator : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, -20) * Time.deltaTime * 20);
	}
}
