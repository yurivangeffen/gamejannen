using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	
	int currentScore = 0;
	int toScore = 0;

	Text text;

	public AudioClip tick;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {


		if (currentScore < toScore) {
			int delta = toScore - currentScore;
			delta = delta / 5 + 1;

			currentScore += delta;

			if(tick != null)
				AudioSource.PlayClipAtPoint (tick, transform.position, delta);
		}

		text.text = currentScore.ToString ();
	}

	public void setScore(int toAdd) {
		toScore += toAdd;
	}
}
