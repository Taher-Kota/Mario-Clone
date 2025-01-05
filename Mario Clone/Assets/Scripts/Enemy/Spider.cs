using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public bool CanMove;
    private Vector3 MovingDirection = Vector3.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        CanMove = true;
        StartCoroutine("ChangeDirection");
    }

    
    void Update()
    {
        if (CanMove)
        {
            transform.Translate(MovingDirection * Time.smoothDeltaTime);
        }
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));
        if (MovingDirection == Vector3.down)
        {
            MovingDirection = Vector3.up;
        }
        else
        {
            MovingDirection = Vector3.down;
        }
        StartCoroutine("ChangeDirection");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "bullet")
        {
            CanMove = false;
            anim.Play("Dead");
            rb.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<BoxCollider2D>().isTrigger = true;
            StopCoroutine("ChangeDirection");
        }
        if(collision.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
