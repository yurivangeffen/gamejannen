using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GunController : MonoBehaviour {

	public List<Gun> guns;

	private int currentIndex = 0;
	private Gun selected;
	private Text gunName;
	// Use this for initialization
	void Start () {
		selected = guns [0];
		gunName = GameObject.FindGameObjectWithTag ("GunName").GetComponent<Text>();
		gunName.text = guns[currentIndex].name;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetAxisRaw("Mouse ScrollWheel") != 0){
			if(Input.GetAxisRaw("Mouse ScrollWheel") > 0){
				currentIndex++;
			}
			if(Input.GetAxisRaw("Mouse ScrollWheel") < 0){
				currentIndex--;
			}

			currentIndex = Mathf.Abs(currentIndex % guns.Count);
			selected = guns[currentIndex];
			gunName.text = guns[currentIndex].name;
		}


		// Left mouse pressed
		if (Input.GetMouseButtonDown (0)) {
			if(selected.canShoot())
				selected.doShoot();
			else if(selected.isEmpty())
				selected.doEmpty();
		}
		
		// Left mouse pressed
		if (Input.GetMouseButtonDown (1)) {
			if(selected.canReload())
				selected.doStartReload();
		}
	}
}
