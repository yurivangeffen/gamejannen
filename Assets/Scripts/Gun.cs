using UnityEngine;
using System.Collections.Generic;

public class Gun : MonoBehaviour {

	public string name = "Unknown";
	public List<AudioClip> shootSounds;
	public List<AudioClip> reloadSounds;
	public AudioClip emptySound;
	public float shotTime = 0.2f;
	public float reloadTime = 0.5f;
	public int shotsUntilReload = 1;
	public float spread = 0.05f;

	private int shot = 0;
	private float passedTime = 0f;
	private float passedReloadTime = 0f;
	private bool reloading = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		passedTime += Time.deltaTime;

		if (reloading) {
			passedReloadTime += Time.deltaTime;
			if(passedReloadTime > reloadTime)
				doEndReload();
		}

	}
	
	public void doShoot() {
		shot++;
		passedTime = 0f;
		
		Shootable[] shootables = FindObjectsOfType(typeof(Shootable)) as Shootable[];
		foreach (Shootable shootable in shootables) {
			shootable.OnShoot(spread);
		}

		AudioSource.PlayClipAtPoint (shootSounds [Random.Range(0, shootSounds.Count)], transform.position);
	}
	
	public void doStartReload() {
		reloading = true;
		passedReloadTime = 0f;
		
		AudioSource.PlayClipAtPoint (reloadSounds [Random.Range(0, reloadSounds.Count)], transform.position);
	}

	public void doEndReload() {
		shot = 0;
		reloading = false;
	}

	public void doEmpty() {
		if(!reloading)
			AudioSource.PlayClipAtPoint (emptySound, transform.position);
	}

	public bool canShoot() {
		return !reloading && shot < shotsUntilReload && passedTime > shotTime;
	}

	public bool canReload() {
		return !reloading & passedTime > shotTime;
	}

	public bool isEmpty() {
		return shot >= shotsUntilReload;
	}

	public override string ToString ()
	{
		return string.Format ("[Gun] {0}", name);
	}
}
