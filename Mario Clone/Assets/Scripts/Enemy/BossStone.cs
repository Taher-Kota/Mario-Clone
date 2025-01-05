using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStone : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(Disable());
    }
    void Update()
    {
        
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
