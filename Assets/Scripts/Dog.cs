using UnityEngine;
using System.Collections;

public class Dog : Shootable 
{
    public Sprite[] sprites;

    private GameObject myContainer;
    private SpriteRenderer spriteRenderer;
    private int spriteIndex = 0;
    private float animationTimer = 0;
    private float frameRate = 0.2f;

    private bool inBushes = false;

    void Awake()
    {
        shootableRadius = 0.1f;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Start () 
    {

        //Voor het roteren rond de speler
        myContainer = new GameObject();
        myContainer.name = "GRP_" + transform.gameObject.name;
        myContainer.transform.position = new Vector3(0, 0, 0);
        transform.parent = myContainer.transform;
	}

    void Update()
    {
        if (!inBushes)
        {
            WalkAround();
        }
        else
        {
            GrabDucks();
        }
    }

    private void WalkAround()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        myContainer.transform.Rotate(Vector3.up, 0.4f);

        animationTimer -= Time.deltaTime;
        if (animationTimer < 0)
        {
            if (spriteIndex < 3)
                spriteIndex++;
            else
                spriteIndex = 0;

            spriteRenderer.sprite = sprites[spriteIndex];
            animationTimer = frameRate;
        }

    }

    private void GrabDucks()
    {
        //TODO
    }

    override protected void OnHit()
    {

    }
}
