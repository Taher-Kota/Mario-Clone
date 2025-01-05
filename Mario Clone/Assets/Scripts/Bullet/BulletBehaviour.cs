using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private bool CanMove;
    private float speed = 10f;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    void Start()
    {
        CanMove = true;
    }

    void Update()
    {
        if (CanMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
            StartCoroutine(DisableBullet(4f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "beetle" ||  collision.tag == "turtle" || collision.tag == "spider" || collision.tag == "frog" || collision.tag == "boss")
        {
            anim.Play("Explode");
            CanMove = false;
            StartCoroutine(DisableBullet(.2f));
        }
    }

    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
