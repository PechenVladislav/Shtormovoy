using System.Collections;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private LayerMask objectsLayer;
    [SerializeField]
    private float movementTime;

    private static Action<Vector2> animationEvent;

    private BoxCollider2D PlayerCol;
    private static PlayerMovement instance;
    private bool canMove = true;
    private bool axisInUse = false;
    private bool holdTwoButtons = false;


    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        PlayerCol = GetComponent<BoxCollider2D>();
    }

    private void Move(int x, int y)
    {
        animationEvent.Invoke(new Vector2(x, y));

        Vector3 direction = new Vector3(x, y, 0f);

        RaycastHit2D hit = Physics2D.Linecast(transform.position + direction, transform.position + direction * 1.5f, objectsLayer);

        if(hit)
        {
            //print(hit.transform.gameObject.tag);
            if (hit.transform.gameObject.tag == Assets.Floor.Tag)
            {
                StartCoroutine(MoveCoroutine(direction, false));
            }
            else if (hit.transform.gameObject.tag == Assets.JumpPlatform.Tag)
            {
                if(BeatsCounter.InTakt)
                {
                    StartCoroutine(MoveCoroutine(direction * 2, false));
                }
                else
                {
                    StartCoroutine(MoveCoroutine(direction, true));
                }

            }
            else if (hit.transform.gameObject.tag == Assets.Water.Tag)
            {
                StartCoroutine(MoveCoroutine(direction, true));
            }
        }
    }

    private IEnumerator MoveCoroutine(Vector3 direction, bool dead)
    {
        Vector3 endPosition = transform.position + direction;
        canMove = false;

        yield return new WaitForSeconds(movementTime);

        transform.position = endPosition;

        canMove = true;
        if (dead)
        {
            StartCoroutine(PlayerBehaviuor.Instance.PlayerDeath());
        }
    }

    // Update is called once per frame
    void Update()
    {
        int horInp = (int)Input.GetAxisRaw("Horizontal");
        int vertInp = (int)Input.GetAxisRaw("Vertical");

        if ((horInp != 0 || vertInp != 0) && canMove && !axisInUse)
        {
            if (horInp != 0)
                Move(horInp, 0);
            else
                Move(0, vertInp);
        }

        CheckPressedButtons(horInp, vertInp);

    }

    private void CheckPressedButtons(int horInp, int vertInp)
    {
        if (horInp != 0 && vertInp != 0)
        {
            holdTwoButtons = true;
        }
        else if (horInp != 0 || vertInp != 0)
        {
            axisInUse = true;
            if (holdTwoButtons)
            {
                axisInUse = false;
            }
            holdTwoButtons = false;
        }
        else
        {
            axisInUse = false;
            holdTwoButtons = false;
        }
    }

    public static PlayerMovement Instance
    {
        get { return instance; }
    }

    public static Action<Vector2> AnimationEvent
    {
        get { return animationEvent; }
        set { animationEvent = value; }
    }
}
