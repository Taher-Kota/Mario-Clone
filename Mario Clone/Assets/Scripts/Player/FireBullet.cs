using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public GameObject fireBullet,BulletPosition;
   

    
    void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject bullet = Instantiate(fireBullet,BulletPosition.transform.position, Quaternion.identity);
            bullet.gameObject.GetComponent<BulletBehaviour>().Speed *= transform.localScale.x;
        }
    }
}
