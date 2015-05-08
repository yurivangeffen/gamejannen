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
    
    //Deze moet nog getweakt worden
    float rotationThreshhold = 7;

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
                    Debug.Log("Cumulative rotation: " + cumulativeRotationX + " degrees. Timer started.");
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
        if (timerTime > 1)
            RotationTimerElapsed();

        previousRotationX = rotationX;
    }

    private void RotationTimerElapsed()
    {
        Debug.Log("Timer elapsed");
        timerRunning = false;
        timerTime = 0;
    }

    private float GetRotationChange(float rotation1, float rotation2)
    {
        return Mathf.DeltaAngle(rotation1, rotation2);
    }

}
