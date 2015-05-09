using UnityEngine;
using System.Collections;

public class RandomYRotation : MonoBehaviour 
{
	
	void Start () 
    {
        transform.Rotate(Vector3.up, 360 * Random.value, Space.World);
	}
	
	
	void Update () 
    {
	
	}
}
