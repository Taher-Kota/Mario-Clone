using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Turtle : MonoBehaviour
{
    private bool CanMove;
    private float MoveSpeed = 2f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool MoveLeft;
    public LayerMask PlayerMask;
    public Transform RightColision, LeftColision, TopColision, BottomColision;
    private Vector3 leftcolpos, rightcolpos;
    private bool Stunned;
    private int stunnedTimes=-1;
    public LayerMask GroundLayer;

    private void Awake()
    {
        Stunned = false;
        MoveLeft = true;
        CanMove = true;
        rb = GetComponent<Rigidbody2D>();
        leftcolpos = RightColision.localPosition;
        rightcolpos = LeftColision.localPosition;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        TurtleMove(CanMove);
        CheckColision();
    }

    void TurtleMove(bool CanMove)
    {
        if (CanMove)
        {
            if (MoveLeft)
            {
                rb.velocity = new Vector2(-MoveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(MoveSpeed, rb.velocity.y);
            }
        }
    }

    void CheckColision()
    {
        if (!Physics2D.Raycast(BottomColision.position, Vector2.down, 0.1f,GroundLayer))
        {
            ChangeDirection();
        }


        Collider2D tophit = Physics2D.OverlapCircle(TopColision.position, .25f, PlayerMask);
        if (tophit != null)
        {
            if (!Stunned)
            {
                tophit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(tophit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 10f);
                CanMove = false;
                anim.Play("Stunned");
                Stunned = true;
                rb.velocity = new Vector2(0, 0);
            }
            else
            {
                stunnedTimes++;                
                StartCoroutine(Dead(0.1f));
            }
            if (tag == "beetle")
            {
                StartCoroutine(Dead(.5f));
            }

        }
        if (MoveLeft)
        {
            RaycastHit2D lefthit = Physics2D.Raycast(LeftColision.position, Vector2.left, .2f, PlayerMask);
            if (lefthit)
            {
                lefthitchecker(lefthit,15f);
            }
        }
        if (!MoveLeft)
        {
            RaycastHit2D lefthit = Physics2D.Raycast(LeftColision.position, Vector2.right, .2f, PlayerMask);
            if (lefthit)
            {
                lefthitchecker(lefthit,-15f);
            }
        }

        if (MoveLeft)
        {
            RaycastHit2D righthit = Physics2D.Raycast(RightColision.position, Vector2.right, .2f, PlayerMask);
            if (righthit)
            {
                righthitchecker(righthit, -15f);
            }
        }
        if (!MoveLeft)
        {
            RaycastHit2D righthit = Physics2D.Raycast(RightColision.position, Vector2.left, .2f, PlayerMask);
            if (righthit)
            {
                righthitchecker(righthit, 15f);
            }
        }
    }

    void ChangeDirection()
    {
        MoveLeft = !MoveLeft;
        Vector3 tempscale = transform.localScale;
        if (MoveLeft)
        {
            tempscale.x = Mathf.Abs(tempscale.x);
            LeftColision.localPosition = rightcolpos;
            RightColision.localPosition = leftcolpos;          
        }
        else
        {
            tempscale.x = -Mathf.Abs(tempscale.x);
            LeftColision.localPosition = rightcolpos;
            RightColision.localPosition = leftcolpos;            
        }
        transform.localScale = tempscale;

    }

    void lefthitchecker(RaycastHit2D lefthit,float speed)
    {
        if (lefthit.collider.gameObject.tag == "Player")
        {
            if (!Stunned)
            {
                GameObject.Find("Player").GetComponent<PlayerDamage>().DealDamage();
            }
            else
            {
                if (tag == "turtle")
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                    StartCoroutine(Dead(2f));
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void righthitchecker(RaycastHit2D righthit,float speed)
    {
        if (righthit.collider.gameObject.tag == "Player")
        {
            if (!Stunned)
            {
                GameObject.Find("Player").GetComponent<PlayerDamage>().DealDamage();
            }
            else
            {
                if (tag == "turtle")
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                    StartCoroutine(Dead(2f));
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        if(gameObject.tag != "turtle")
        {
            gameObject.SetActive(false);
        }
        else if(stunnedTimes >= 1) 
        {
        gameObject.SetActive(false);
        stunnedTimes=-1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            if (tag == "beetle")
            {
                anim.Play("Stunned");
                CanMove = false;
                rb.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.3f));
            }
            else if (tag == "turtle")
            {
                if (!Stunned)
                {
                    Stunned = true;
                    CanMove = false;
                    anim.Play("Stunned");
                    rb.velocity = new Vector2(0, 0);
                }
                else
                {
                    StartCoroutine(Dead(.05f));
                }
            }
        }
    }
}
