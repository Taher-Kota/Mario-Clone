using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator anim;
    public GameObject Stone;
    public Transform StonePos;
    private bool dead=false;
    private int score = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(Attack());
    }

    void IdleAnim()
    {
        anim.Play("Idle");
    }

    void ThrowStone()
    {
        GameObject stone = Instantiate(Stone, StonePos.position, Quaternion.identity);
        stone.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700f), 15f));
    }

    IEnumerator Attack()
    {
        if (!dead)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            anim.Play("Attack");
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            score++;
            if (score == 10)
            {
                dead = true;
                anim.Play("Dead");
                StartCoroutine(Dead(1f));
            }
        }
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

}
