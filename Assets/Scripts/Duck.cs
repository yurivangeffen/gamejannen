using UnityEngine;
using System.Collections;

public class Duck : Shootable 
{

    private float flySpeed = 0.4f;
    private float flySpeedUp = 0.4f;
    private float fallSpeed = 2f;
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
    private float newMovementTimer = 0;
    private float timeBeforeNewMovement = 1f;

    private const int amountOfSprites = 8;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shootableRadius = 0.05f;
    }

	void Start () 
    {
        color = 1;

        myContainer = new GameObject();
        myContainer.name = "GRP_" + transform.gameObject.name;
        myContainer.transform.position = new Vector3(0, 0, 0);
        transform.parent = myContainer.transform;
        currentDirection = direction.Right;
	}
	
	void Update () 
    {
        Move();

        animationTimer -= Time.deltaTime;
        if (animationTimer < 0)
        {
            AnimateDuck();
            animationTimer = frameRate;
        }

        newMovementTimer -= Time.deltaTime;
        if (newMovementTimer < 0)
        {
            if (currentDirection != direction.JustHit && currentDirection != direction.Falling)
            {
                ChooseRandomMovement();
            }
            newMovementTimer = timeBeforeNewMovement - 0.5f + Random.value;
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

    override protected void OnHit()
    {
        if (currentDirection == direction.Falling || currentDirection == direction.JustHit)
            return;
        Camera.main.GetComponent<CameraMovement>().OnHit();
        currentDirection = direction.JustHit;

        spriteRenderer.sprite = sprites[color * amountOfSprites + 6];
    }

    private void ChooseRandomMovement()
    {
        float random = Random.value;
        if (random < 0.25f)
        {
            currentDirection = direction.Left;
            return;
        }
        if (random < 0.5f)
        {
            currentDirection = direction.Right;
            return;
        }
        if (random < 0.75f)
        {
            currentDirection = direction.LeftUp;
            return;
        }
        currentDirection = direction.RightUp;
    }

    private void Move()
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
