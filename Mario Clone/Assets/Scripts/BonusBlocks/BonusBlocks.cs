using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlocks : MonoBehaviour
{
    private Animator anim;
    private EdgeCollider2D edge;
    private bool Collected = true;
    private bool CanPlayAnim = false;
    private bool CanStopAnim = false;
    private Vector3 MoveDirection = Vector3.up;
    private Vector3 originpos,endpos;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        edge = GetComponent<EdgeCollider2D>();   
        originpos = transform.position;
        endpos = transform.position;
        endpos.y += .17f;
    }

    
    void Update()
    {
        UpDownAnim();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.collider.IsTouching(edge) && Collected)
        {
            Collected = false;
            CanPlayAnim = true;
            ScoreManager.instance.audioSource.Play();
            anim.Play("empty");
            ScoreManager.instance.score++;
            ScoreManager.instance.scoreText.text = "x" + ScoreManager.instance.score;
        }
    }

    void UpDownAnim()
    {
        if (CanPlayAnim)
        {
            if (!CanStopAnim)
            {
                transform.Translate(MoveDirection * Time.smoothDeltaTime);
                if (transform.position.y >= endpos.y)
                {
                    MoveDirection = Vector3.down;
                }
                else if (transform.position.y <= originpos.y)
                {
                    CanStopAnim = true;
                }
            }
        }
    }
}
