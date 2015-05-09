using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    //360 noscope variabelen #yolo
    float previousRotationX = 0;
    float cumulativeRotationX = 0;
    
    //Deze moeten nog getweakt worden
    float rotationThreshhold = 7;
    float timeAfterRotating = 0.8f;//De tijd die de speler nog heeft na het roteren om werkelijk te schieten (in seconden).

    float rotationBeforeTimer = 0;

    //Deze zijn om de speler even de tijd te geven om te richten na het draaien
    bool isRotating = false;
    bool timerRunning = false;
    float timerTime = 0;

    void Start()
    {
        
    }

    void Update()
    {
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
                if (!timerRunning)
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
