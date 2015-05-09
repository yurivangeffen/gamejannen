using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dog : Shootable 
{

    public Sprite[] sprites;
    public float jumpSpeed = 0.4f;
	public AudioClip hitSound = null;

    private GameObject myContainer;
    private SpriteRenderer spriteRenderer;
    private int spriteIndex = 0;
    private float timer = 0;
    private float frameRate = 0.2f;

    private List<Vector3> ducksToGet;

    private enum Mode {None, Walking, Jumping, Falling, InBushesWaiting, InBushesUp, InBushesDown };
    private Mode currentMode = Mode.Walking;



    void Awake()
    {
        ducksToGet = new List<Vector3>();
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

        if (Input.GetKeyDown(KeyCode.A))
            ducksToGet.Add(new Vector3(0, -0.3f, -1.6f));

        switch (currentMode)
        {
            case Mode.Walking:
                WalkAround();
                break;
            case Mode.Jumping:
                Jumping();
                break;
            case Mode.Falling:
                Falling();
                break;
            case Mode.InBushesWaiting:
                WaitAndGrabDucks();
                break;
            case Mode.InBushesUp:
                ShowDuckUp();
                break;
            case Mode.InBushesDown:
                ShowDuckDown();
                break;
        }

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
        if (currentMode != Mode.Walking)
            return;
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
            currentMode = Mode.InBushesWaiting;
        }
    }

    public void GetDuck(Vector3 position)
    {
        if (currentMode == Mode.Walking)
            return;
        ducksToGet.Add(position);
    }

    private void WaitAndGrabDucks()
    {
        if (ducksToGet.Count > 0)
        {
            currentMode = Mode.InBushesUp;
            transform.position = ducksToGet[0];
            transform.LookAt(Camera.main.transform);
            ducksToGet.RemoveAt(0);
            timer = 0.3f;
            spriteRenderer.sprite = sprites[8];
        }
    }

    private void ShowDuckUp()
    {
        transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0.3f;
            currentMode = Mode.None;
            Invoke("GoDown", 0.5f);
        }
    }

    private void GoDown()
    {
        currentMode = Mode.InBushesDown;
    }

    private void ShowDuckDown()
    {
        transform.position += new Vector3(0, -1, 0) * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            spriteRenderer.sprite = null;
            currentMode = Mode.InBushesWaiting;
        }
    }


    override protected void OnHit()
    {
		if (hitSound != null)
			AudioSource.PlayClipAtPoint (hitSound, transform.position, 3f);
    }
}
