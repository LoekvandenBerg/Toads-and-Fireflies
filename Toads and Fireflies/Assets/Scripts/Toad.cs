using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toad : MonoBehaviour
{
    public int player;
    [SerializeField]
    private Transform[] jumpPathPoints;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private Tongue tongue;
    [SerializeField]
    private Sprite[] toadSprites;
    private bool grounded, reachedTargetPoint;
    private int positionIndex;
    private Vector2 jumpDir;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        grounded = true;

    }

    // Update is called once per frame
    void Update()
    {
        //if grounded AND jump button pressed: Jump!
        if (Input.GetButtonDown("Action" + player) && grounded)
        {
            jumpDir = positionIndex == 0 ? Vector2.right : Vector2.left;
            spriteRenderer.sprite = toadSprites[1];
            grounded = false;
        }
        // if not grounded and jump button pressed: Attack!
        else if (Input.GetButtonDown("Action" + player))
        {
            StartCoroutine(Attack());
        }
        Move();
    }

    IEnumerator Attack()
    {
        tongue.gameObject.SetActive(true);
        yield return new WaitForSeconds(.7f); 
        tongue.gameObject.SetActive(false);
    }

    void Move()
    {
        //if close enough to target point, return true
        if (!grounded)
        {
            reachedTargetPoint = Vector2.Distance(transform.position, jumpPathPoints[positionIndex].position) <= .1f;
            // jump to the right pad
            if (jumpDir == Vector2.right)
            {
                if (reachedTargetPoint)
                {
                    positionIndex++;
                }
                if (ReachedLandingPad())
                {
                    positionIndex = 2;
                }
            }
            // jump to the left pad
            else
            {
                if (reachedTargetPoint)
                {
                    positionIndex--;
                }
                if (ReachedLandingPad())
                {
                    positionIndex = 0 ;
                }
            }
        }
        //move towards target path point
        transform.position = Vector2.MoveTowards(transform.position, jumpPathPoints[positionIndex].position, jumpSpeed);
    }

    bool ReachedLandingPad()
    {
        if (jumpDir == Vector2.right && positionIndex == jumpPathPoints.Length || jumpDir == Vector2.left &&  positionIndex == -1)
        {
            grounded = true;
            transform.Rotate(Vector3.up, 180);
            spriteRenderer.sprite = toadSprites[0];
            return true;
        }
        return false;
    }
}
