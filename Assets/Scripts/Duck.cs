using UnityEngine;
using System.Collections;

public class Duck : MonoBehaviour 
{

    private float flySpeed = 0.4f;
    private float flySpeedUp = 0.4f;
    private float fallSpeed = 1f;
    private GameObject myContainer;
    private SpriteRenderer spriteRenderer;

    private int color;//0,1 of 2. Staat voor de kleur van de eend.
    
    public Sprite[] sprites;
    private Sprite currentSprite;
    private int spriteNumber = 0;
    private float animationTimer = 0;
    private float frameRate = 0.2f;
    private int hitFrameAmount = 0;


    private enum direction {Left, Right, LeftUp, RightUp, JustHit, Falling};
    private direction currentDirection;

    private const int amountOfSprites = 8;
	void Start () 
    {
        color = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();

        myContainer = new GameObject();
        myContainer.name = "GRP_" + transform.gameObject.name;
        myContainer.transform.position = new Vector3(0, 0, 0);
        transform.parent = myContainer.transform;
        currentDirection = direction.Right;
	}
	
	void Update () 
    {
        switch (currentDirection)
        {
            case direction.Right:
                MoveRight();
                break;
            case direction.Left:
                MoveLeft();
                break;
            case direction.RightUp:
                MoveRightUp();
                break;
            case direction.LeftUp:
                MoveLeftUp();
                break;
            case direction.Falling:
                MoveDown();
                break;
        }

        animationTimer -= Time.deltaTime;
        if (animationTimer < 0)
        {
            AnimateDuck();
            animationTimer = frameRate;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
            //Debug.Log("Pos: "+screenPosition);
            if (screenPosition.x > 0.45f && screenPosition.x < 0.55f && screenPosition.y > 0.45f && screenPosition.y < 0.55f)
            {
                OnHit();
            }
        }
	}

    private void AnimateDuck()
    {
        int spriteIndex;
        
        if (spriteNumber == 0)
        {
            spriteIndex = color * amountOfSprites + 1;
            spriteNumber = 1;
        }
        else
        {
            if (spriteNumber == 1)
            {
                spriteIndex = color * amountOfSprites + 2;
                spriteNumber = 2;
            }
            else
            {
                spriteIndex = color * amountOfSprites;
                spriteNumber = 0;
            }
        }
        if (currentDirection == direction.LeftUp || currentDirection == direction.RightUp)
            spriteIndex += 3;

        if (currentDirection == direction.JustHit)
        {
            spriteIndex = color * amountOfSprites + 6;
            hitFrameAmount++;
            if (hitFrameAmount > 2)//Groter is langer het justhit frame in beeld
            {
                hitFrameAmount = 0;
                currentDirection = direction.Falling;
            }
        }

        if (currentDirection == direction.Falling)
        {
            spriteIndex = color * amountOfSprites + 7;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnHit()
    {
        Camera.main.GetComponent<CameraMovement>().OnHit();
        currentDirection = direction.JustHit;

        spriteRenderer.sprite = sprites[color * amountOfSprites + 6];
    }

    private void MoveDown()
    {
        transform.position += new Vector3(0, -fallSpeed, 0) * Time.deltaTime;
    }

    private void MoveRight()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        myContainer.transform.Rotate(Vector3.up, flySpeed);
        currentDirection = direction.Right;
    }

    private void MoveLeft()
    {
        transform.localScale = new Vector3(1, 1, 1);
        myContainer.transform.Rotate(Vector3.up, -flySpeed);
        currentDirection = direction.Left;
    }

    private void MoveRightUp()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        transform.position += new Vector3(0, flySpeedUp, 0) * Time.deltaTime;
        myContainer.transform.Rotate(Vector3.up, flySpeed);
        currentDirection = direction.RightUp;
    }

    private void MoveLeftUp()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position += new Vector3(0, flySpeedUp, 0) * Time.deltaTime;
        myContainer.transform.Rotate(Vector3.up, -flySpeed);
        currentDirection = direction.LeftUp;
    }
}
