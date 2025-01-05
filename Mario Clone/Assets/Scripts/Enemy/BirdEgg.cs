using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEgg : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDamage>().DealDamage();
        }
        else
        {
           gameObject.SetActive(false);
        }
    }
}
