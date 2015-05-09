using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public Text rotationScore;

    public GameObject explosionObject;

    float rotationY = 0F;

    //360 noscope variabelen #yolo
    float previousRotationX = 0;
    float cumulativeRotationX = 0;
    
    //Deze moeten nog getweakt worden
    float rotationThreshhold = 7;
    float timeAfterRotating = 2f;//De tijd die de speler nog heeft na het roteren om werkelijk te schieten (in seconden).

    float rotationBeforeTimer = 0;

    //Deze zijn om de speler even de tijd te geven om te richten na het draaien
    bool isRotating = false;
    bool timerRunning = false;
    float timerTime = 0;

	int score;

	ScoreKeeper scoreUI;
    public Text timeText;
    private float time = 5;//tijd voor het level in seconden

    void Start()
	{
		scoreUI = GameObject.FindGameObjectWithTag ("ScoreUI").GetComponent<ScoreKeeper>();
        Cursor.lockState = CursorLockMode.Locked;//Uncomment dit voor de final versie
    }

    private void TimeUp()
    {
        float yRot = -Quaternion.ToEulerAngles(transform.rotation).y + .5f*Mathf.PI;
        //float radYRot = yRot / (180 * Mathf.PI);
        Debug.Log(yRot);
        float distance = 4;
        float x = Mathf.Cos(yRot) * distance;
        float z = Mathf.Sin(yRot) * distance;
        Vector3 position = new Vector3(x, 0, z);
        Instantiate(explosionObject, position, Quaternion.identity);
        

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
            TimeUp();


        time -= Time.deltaTime;
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        if (seconds >= 10)
            timeText.text = minutes + ":" + seconds;
        else
            timeText.text = minutes + ":0" + seconds;

        if (minutes < 1 && seconds < 1)
        {
            TimeUp();
            time = 300000;
        }


        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;//Getal tussen 0-360, staat voor huidige rotatie om Y as van speler object

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);


        float addedRotation = Mathf.Abs(GetRotationChange(previousRotationX, rotationX));

        if (addedRotation > rotationThreshhold)
        {
            cumulativeRotationX += addedRotation;
            
            isRotating = true;
        }
        else
        {
            if (isRotating)
            {
                if (!timerRunning || (timerRunning && rotationBeforeTimer < cumulativeRotationX))
                {
                    Debug.Log("Wow een " + cumulativeRotationX + " graden noscope. Wat ben jij een baas.");
                    rotationBeforeTimer = cumulativeRotationX;
                    timerRunning = true;
                }
            }
            else
            {
                cumulativeRotationX = 0;
            }
            isRotating = false;
        }

        if (timerRunning)
            timerTime += Time.deltaTime;
        if (timerTime > timeAfterRotating)
            RotationTimerElapsed();

		score = (int)Mathf.Max(cumulativeRotationX, rotationBeforeTimer);
		rotationScore.text = score.ToString();

        previousRotationX = rotationX;
    }

    private void RotationTimerElapsed()
    {
        Debug.Log("Tijd om te klikken voorbij.");
        timerRunning = false;
        timerTime = 0;
        rotationBeforeTimer = 0;
    }


    public void OnHit()
    {
        if (timerRunning || isRotating)//tijdens een noscope
        {
            Debug.Log("Je hebt een eend geschoten! Je nosocpe score is: " + rotationBeforeTimer + ".");
			scoreUI.setScore((int)rotationBeforeTimer);
        }
        else
        {//niet aan het draaien
            Debug.Log("Je hebt een eend geschoten! Je was niet aaan het noscopen.");
        }
    }

    private float GetRotationChange(float rotation1, float rotation2)
    {
        return Mathf.DeltaAngle(rotation1, rotation2);
    }

}
