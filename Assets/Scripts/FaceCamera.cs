//FaceCamera.cs v02
//by Neil Carter (NCarter)
//modified by Juan Castaneda (juanelo)
//
//added in-between GRP object to perform rotations on
//added auto-find main camera
//added un-initialized state, where script will do nothing
using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

	public Camera m_Camera;
	GameObject myContainer;	
	
	void Start(){
		m_Camera = Camera.main;
		
		myContainer = new GameObject();
		myContainer.name = "GRP_"+transform.gameObject.name;
		myContainer.transform.position = transform.position;
		transform.parent = myContainer.transform;

		
		myContainer.transform.LookAt(m_Camera.transform, Vector3.up);
	}
	
	
	void Update(){
		myContainer.transform.LookAt(m_Camera.transform, Vector3.up);
	}
}
