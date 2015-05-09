using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour 
{

	public void OnClick()
    {
        Application.LoadLevel("main_scene");
    }
}
