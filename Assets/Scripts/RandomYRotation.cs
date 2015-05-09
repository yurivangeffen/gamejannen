using UnityEngine;
using System.Collections;

public class RandomYRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Rotate(Vector3.up, 360 * Random.value);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
