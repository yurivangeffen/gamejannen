using UnityEngine;
using System.Collections;

public class Duck : MonoBehaviour 
{

    private float flySpeed = 0.4f;
    private GameObject myContainer;
    private SpriteRenderer spriteRenderer;

    public Sprite sprite1, sprite2, sprite3;
    private Sprite currentSprite;
    private float animationTimer = 0;
    private float frameRate = 0.2f;

	void Start () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        myContainer = new GameObject();
        myContainer.name = "GRP_" + transform.gameObject.name;
        myContainer.transform.position = new Vector3(0, 0, 0);
        transform.parent = myContainer.transform;
	}
	
	void Update () 
    {
        MoveRight();

        animationTimer -= Time.deltaTime;
        if (animationTimer < 0)
        {
            AnimateDuck();
            animationTimer = frameRate;
        }
	}

    private void AnimateDuck()
    {
        if (currentSprite==sprite1)
        {
            currentSprite = sprite2;
        }
        else
        {
            if (currentSprite==sprite2)
            {
                currentSprite = sprite3;
            }
            else
            {
                currentSprite = sprite1;
            }
        }
        spriteRenderer.sprite = currentSprite;
    }

    private void MoveRight()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        myContainer.transform.Rotate(Vector3.up, flySpeed);
    }

    private void MoveLeft()
    {
        transform.localScale = new Vector3(1, 1, 1);
        myContainer.transform.Rotate(Vector3.up, -flySpeed);
    }
}
