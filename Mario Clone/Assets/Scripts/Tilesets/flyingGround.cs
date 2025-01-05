using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class flyingGround : MonoBehaviour
{
    private Vector3 MovingDirection = Vector3.right;
    private float speed = 2.5f;

    void Start()
    {
        StartCoroutine(ChangeDirection(2.3f));
    }

    
    void Update()
    {
        transform.Translate(speed * Time.smoothDeltaTime * MovingDirection);
    }

    IEnumerator ChangeDirection(float timer)
    {
        yield return new WaitForSeconds(timer);
        if (MovingDirection == Vector3.right)
        {
            MovingDirection = Vector3.left;
        }
        else
        {
            MovingDirection = Vector3.right;
        }
        StartCoroutine(ChangeDirection(timer));
    }

}
