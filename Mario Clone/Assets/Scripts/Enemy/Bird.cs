using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour
{
    private float speed=2.5f;
    private bool CanMove;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 Movingdirection = Vector3.left;
    private Vector3 originPosition,finishingPosition;
    public GameObject BirdEgg;
    private bool Attacked;
    public LayerMask playerLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        CanMove = true;
        originPosition = transform.position;
        originPosition.x += 6f;

        finishingPosition = transform.position;
        finishingPosition.x -= 6f;
    }

    
    void Update()
    {
        if (CanMove)
        {
            if (transform.position.x >= originPosition.x) {
                Movingdirection = Vector3.left;
                ChangeDirection();
            }
            else if (transform.position.x <= finishingPosition.x)
            {
                Movingdirection = Vector3.right;
                ChangeDirection();
            }
              transform.Translate( speed * Time.smoothDeltaTime *Movingdirection);           
        }
        EggDrop();
    }

    void ChangeDirection()
    {
        Vector3 tempscale = transform.localScale;
        if (Movingdirection == Vector3.left)
        {
            tempscale.x = Mathf.Abs(transform.localScale.x);
            transform.localScale = tempscale;
        }
        else
        {
            tempscale.x = -Mathf.Abs(transform.localScale.x);
            transform.localScale = tempscale;
        }
    }

    void EggDrop()
    {
        if (!Attacked)
        {
           if(Physics2D.Raycast(transform.localPosition, Vector2.down, Mathf.Infinity, playerLayer))
            {
                GameObject egg = Instantiate(BirdEgg,new Vector3(transform.position.x,transform.position.y-1f,transform.position.z), Quaternion.identity);
                Attacked = true;
                anim.Play("IdleFly");
                StartCoroutine(NewEgg());
            }
        }
    }

    IEnumerator NewEgg()
    {
        yield return new WaitForSeconds(2f);
        Attacked = false;
        anim.Play("StoneFly");
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "bullet")
        {
            anim.Play("Dead");
            GetComponent<BoxCollider2D>().isTrigger = true;
            rb.bodyType = RigidbodyType2D.Dynamic;

            StartCoroutine(Dead(3f));
        }
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
   
}
