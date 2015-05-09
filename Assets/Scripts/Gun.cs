using UnityEngine;
using System.Collections.Generic;

public class Gun : MonoBehaviour {
	
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

		// Left mouse pressed
		if (Input.GetMouseButtonDown (0)) {
			if(canShoot())
				doShoot();
			else if(shot >= shotsUntilReload)
				doEmpty();
		}

		// Left mouse pressed
		if (Input.GetMouseButtonDown (1)) {
			if(canReload())
				doStartReload();
		}

	}
	
	void doShoot() {
		shot++;
		passedTime = 0f;
		
		AudioSource.PlayClipAtPoint (shootSounds [Random.Range(0, shootSounds.Count)], transform.position);
	}
	
	void doStartReload() {
		reloading = true;
		passedReloadTime = 0f;
		
		AudioSource.PlayClipAtPoint (reloadSounds [Random.Range(0, reloadSounds.Count)], transform.position);
	}

	void doEndReload() {
		shot = 0;
		reloading = false;
	}

	void doEmpty() {
		if(!reloading)
			AudioSource.PlayClipAtPoint (emptySound, transform.position);
	}

	bool canShoot() {
		return !reloading && shot < shotsUntilReload && passedTime > shotTime;
	}

	bool canReload() {
		return !reloading & passedTime > shotTime;
	}
}
