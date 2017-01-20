using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private Vector3 startPosition = Vector3.zero;
    [SerializeField]
    private LayerMask objectsLayer;
    [SerializeField]
    private float movementTime;

    private BoxCollider2D PlayerCol;

    private bool canMove = true;
    private bool axisInUse = false;
    private bool holdTwoButtons = false;


    // Use this for initialization
    void Start()
    { 
        PlayerCol = GetComponent<BoxCollider2D>();

        transform.position = startPosition;
    }

    private void Move(int x, int y)
    {
        Vector3 direction = new Vector3(x, y, 0f);
        Vector3 endPosition;

        RaycastHit2D hit = Physics2D.Linecast(transform.position + direction, transform.position + direction * 1.5f, objectsLayer);

        if(hit)
        {
            print(hit.transform.gameObject.tag);
            if (hit.transform.gameObject.tag == Assets.Floor.Tag)
            {
                endPosition = transform.position + direction;
                StartCoroutine(MoveCoroutine(endPosition));
            }
            else if (hit.transform.gameObject.tag == Assets.JumpPlatform.Tag)
            {
                endPosition = transform.position + direction * 2;
                StartCoroutine(MoveCoroutine(endPosition));
            }
            else if (hit.transform.gameObject.tag == Assets.Water.Tag)
            {
                endPosition = transform.position + direction;
                StartCoroutine(MoveCoroutine(endPosition));
            }
        }
    }

    private IEnumerator MoveCoroutine(Vector3 endPosition)
    {
        canMove = false;

        yield return new WaitForSeconds(movementTime);

        transform.position = endPosition;

        canMove = true;
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
}
