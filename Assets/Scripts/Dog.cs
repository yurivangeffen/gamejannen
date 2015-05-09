using UnityEngine;
using System.Collections;

public class Dog : Shootable 
{
    public Sprite[] sprites;
    public float jumpSpeed = 0.4f;

    private GameObject myContainer;
    private SpriteRenderer spriteRenderer;
    private int spriteIndex = 0;
    private float timer = 0;
    private float frameRate = 0.2f;

    private enum Mode {None, Walking, Jumping, Falling, InBushes };
    private Mode currentMode = Mode.Walking;

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
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();


        if (currentMode == Mode.Walking)
            WalkAround();
        else
            if (currentMode == Mode.Jumping)
                Jumping();
            else
                if (currentMode == Mode.Falling)
                    Falling();
                else
                    GrabDucks();
    }

    private void WalkAround()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        myContainer.transform.Rotate(Vector3.up, 0.4f);

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (spriteIndex < 3)
                spriteIndex++;
            else
                spriteIndex = 0;

            spriteRenderer.sprite = sprites[spriteIndex];
            timer = frameRate;
        }

    }

    public void Jump()
    {
        spriteRenderer.sprite = sprites[5];//surprised sprite
        timer = 3000;//make sure this sprite stays
        currentMode = Mode.None;
        Invoke("Jump2", 0.3f);
    }

    private void Jump2()
    {
        currentMode = Mode.Jumping;
        spriteIndex = 0;
        spriteRenderer.sprite = sprites[6];//jump sprite
        timer = 0.3f;
    }

    private void Jumping()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            currentMode = Mode.Falling;
            timer = 0.25f;
            spriteRenderer.sprite = sprites[7];//fall sprite
        }
        transform.localPosition += new Vector3(0, jumpSpeed, -0.5f) * Time.deltaTime;

    }

    private void Falling()
    {
        transform.localPosition += new Vector3(0, -jumpSpeed, -0.5f) * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            spriteRenderer.sprite = null;
            currentMode = Mode.InBushes;
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
