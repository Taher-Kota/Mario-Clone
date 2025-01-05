using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Frog : MonoBehaviour
{
    private Animator anim;
    private bool JumpedLeft = true;
    private int JumpTimes;
    private bool CanJump = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine("JumpFrog");
    }
  
    IEnumerator JumpFrog()
    {
        if (CanJump)
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));

            if (JumpedLeft)
            {
                JumpTimes++;
                anim.Play("FrogJumpLeft");
            }
            else
            {
                JumpTimes++;
                anim.Play("FrogJumpRight");
            }

            StartCoroutine("JumpFrog");
        }
    }
    void AnimationFinished()
    { 
        if (JumpedLeft)
        {
            anim.Play("FrogIdleLeft");
        }
        else
        {
            anim.Play("FrogIdleRight");
        }
        if (JumpTimes == 4)
        {
            if (JumpedLeft)
            {
                ChangeDirection(-1);
            }
            else
            {
                ChangeDirection(1);
            }
            JumpTimes = 0;
            JumpedLeft = !JumpedLeft;
        }
        transform.parent.position = transform.position;
        transform.localPosition = Vector3.zero;
    }
    IEnumerator FrogDead()
    {
        CanJump = false;
        anim.Play("Dead");
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }
    private void ChangeDirection(float direction)
    {
       Vector3 tempscale = transform.localScale;
       tempscale.x = direction;
       transform.localScale = tempscale;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "bullet")
        {
            StartCoroutine(FrogDead());
        }
        if(collision.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
