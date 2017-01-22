using System.Collections;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private LayerMask objectsLayer;
    [SerializeField]
    private float movementTime;
    [SerializeField]
    private AudioClip moveClip;
    [SerializeField]
    private AudioClip jumpClip;
    [SerializeField]
    private AudioClip passClip;

    private static Action<Vector2, bool> animationEvent;        //<dir, isJumpFloor>
    private static Action cheapPlatformAction;

    private BoxCollider2D PlayerCol;
    private static PlayerMovement instance;
    private bool canMove = true;
    private bool axisInUse = false;
    private bool holdTwoButtons = false;
    private AudioSource audioSource;


    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayerCol = GetComponent<BoxCollider2D>();
    }

    private void Move(int x, int y)
    {
        Vector3 direction = new Vector3(x, y, 0f);

        RaycastHit2D hit = Physics2D.Linecast(transform.position + direction, transform.position + direction * 1.5f, objectsLayer);

        if(hit)
        {
            //print(hit.transform.gameObject.tag);
            if (hit.transform.gameObject.tag == Assets.Floor.Tag)
            {
                if(BeatsCounter.InBeat)
                {
                    animationEvent.Invoke(new Vector2(x, y), false);
                    StartCoroutine(MoveCoroutine(direction, false));
                    audioSource.clip = moveClip;
                    audioSource.Play();
                }
                else
                {
                    hit.transform.gameObject.GetComponent<Assets.Floor>().missPlatform();
                    audioSource.clip = passClip;
                    audioSource.Play();
                }
            }
            else if (hit.transform.gameObject.tag == Assets.JumpPlatform.Tag)
            {
                if(BeatsCounter.InTakt)
                {
                    animationEvent.Invoke(new Vector2(x, y), true);
                    StartCoroutine(MoveCoroutine(direction * 2, false));
                    audioSource.clip = jumpClip;
                    audioSource.Play();
                }
                else
                {
                    animationEvent.Invoke(new Vector2(x, y), true);     //
                    StartCoroutine(MoveCoroutine(direction, true));
                    audioSource.clip = passClip;
                    audioSource.Play();
                }

            }
            else if (hit.transform.gameObject.tag == Assets.Water.Tag)
            {
                animationEvent.Invoke(new Vector2(x, y), false);
                StartCoroutine(MoveCoroutine(direction, true));
                audioSource.clip = passClip;
                audioSource.Play();
            }
            else if (hit.transform.gameObject.tag == "CheapPlatform")
            {
                if (BeatsCounter.InBeat)
                {
                    animationEvent.Invoke(new Vector2(x, y), false);
                    hit.transform.gameObject.GetComponent<CheapPlatform>().StartDestroy();
                    //cheapPlatformAction.Invoke();
                    StartCoroutine(MoveCoroutine(direction, false));
                    audioSource.clip = moveClip;
                    audioSource.Play();
                }
                else
                {
                    hit.transform.gameObject.GetComponent<Assets.Floor>().missPlatform();
                    audioSource.clip = passClip;
                    audioSource.Play();
                }
            }
            else if (hit.transform.gameObject.tag == "Finish")
            {
                if (BeatsCounter.InBeat)
                {
                    StartCoroutine(MoveCoroutine(direction, false));
                    StartCoroutine(PlayerBehaviuor.Instance.Finish());
                    audioSource.clip = moveClip;
                    audioSource.Play();
                }
                else
                {
                    hit.transform.gameObject.GetComponent<Assets.Floor>().missPlatform();
                    audioSource.clip = passClip;
                    audioSource.Play();
                }
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

    public static Action<Vector2, bool> AnimationEvent
    {
        get { return animationEvent; }
        set { animationEvent = value; }
    }

    public static Action CheapPlatformAction
    {
        get { return cheapPlatformAction; }
        set { cheapPlatformAction = value; }
    }
}
