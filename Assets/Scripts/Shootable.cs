using UnityEngine;
using System.Collections;

public class Shootable : MonoBehaviour 
{
    protected float shootableRadius;

    protected void OnShoot(float shotRadius)
    {
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        float totalRadius = shotRadius + shootableRadius;
        if (screenPosition.x > 0.5f - totalRadius && screenPosition.x < 0.5f + totalRadius && screenPosition.y > 0.5f - totalRadius && screenPosition.y < 0.5f + totalRadius)
        {
            OnHit();
        }
    }

    virtual protected void OnHit()
    {
    }

}
