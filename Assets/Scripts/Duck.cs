using UnityEngine;
using System.Collections;

public class Duck : MonoBehaviour 
{

    private float flySpeed = 0.4f;
    private float flySpeedUp = 0.4f;
    private GameObject myContainer;
    private SpriteRenderer spriteRenderer;


    
    private int color;//0,1 of 2. Staat voor de kleur van de eend.
    
    public Sprite[] sprites;
    private Sprite currentSprite;
    private int spriteNumber = 0;
    private float animationTimer = 0;
    private float frameRate = 0.2f;

    private enum direction {Left, Right, LeftUp, RightUp};
    private direction currentDirection;

    private const int amountOfSprites = 6;
	void Start () 
    {
        color = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();

        myContainer = new GameObject();
        myContainer.name = "GRP_" + transform.gameObject.name;
        myContainer.transform.position = new Vector3(0, 0, 0);
        transform.parent = myContainer.transform;
	}
	
	void Update () 
    {
        MoveRightUp();

        animationTimer -= Time.deltaTime;
        if (animationTimer < 0)
        {
            AnimateDuck();
            animationTimer = frameRate;
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
        spriteRenderer.sprite = sprites[spriteIndex];
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
