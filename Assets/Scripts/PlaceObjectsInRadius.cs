using UnityEngine;
using System.Collections;

public class PlaceObjectsInRadius : MonoBehaviour {


	public GameObject toPlace;
	public float radius;
	public float objectWidth;

	// Use this for initialization
	void Start () {
		float totalObjectWidth = toPlace.transform.localScale.x * objectWidth;
		float circumference = Mathf.PI * radius * 2;
		int numberOfObjects = (int)(circumference / totalObjectWidth);
		float deltaAngle = (2 * Mathf.PI) / numberOfObjects;

		float x, z;
		for (int i = 0; i < numberOfObjects; ++i) {
			x = Mathf.Cos ((float)i*deltaAngle) * radius;
			z = Mathf.Sin ((float)i*deltaAngle) * radius;
			Instantiate(toPlace, transform.position + new Vector3(x,0,z), Quaternion.identity);
			//Instantiate(toPlace, transform.position + Quaternion.Euler(0,r/180,0) * (Vector3.forward * radius), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
