using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool CanMove = true;
    private float speed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    public Transform GroundPosition;
    public LayerMask layerMask;
    private bool  IsGrounded,isWatered;
    private float JumpPower = 11;
    [SerializeField]
    private AudioSource jumpAudio;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
    
    }

    
    void Update()
    {
        PlayerJump();
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            float Xaxix = Input.GetAxisRaw("Horizontal");
            if (Xaxix > 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                ChangeDirection(1);
            }
            else if (Xaxix < 0)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                ChangeDirection(-1);
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }

            anim.SetInteger("Speed", Mathf.Abs((int)rb.velocity.x));
        }
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempdirection = transform.localScale;
        tempdirection.x = direction;
        transform.localScale = tempdirection;
    }

    bool GroundCheck()
    {
        IsGrounded = Physics2D.Raycast(GroundPosition.position, Vector2.down, 0.1f, layerMask);    
        return IsGrounded;
    }

    void PlayerJump()
    {
        if (GroundCheck())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);
                anim.SetBool("Jump", true);
                jumpAudio.Play();
            }
            else
            {
                anim.SetBool("Jump", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "water")
        {
            CanMove = false;
            rb.isKinematic = true;
            rb.velocity = new Vector2(0, -.4f);
            StartCoroutine(Dead(3f));
        }
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
