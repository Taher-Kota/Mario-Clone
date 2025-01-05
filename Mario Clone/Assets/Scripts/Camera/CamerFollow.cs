using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    private GameObject player;
    public float cameraSpeed,OffSetX,OffSetY,OffSetZ;
    private Vector3 startPos, endPos;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void LateUpdate()
    {
        startPos = transform.position;
        endPos = player.transform.position;
        endPos.x += OffSetX;
        endPos.y = transform.position.y;
        endPos.z = -OffSetZ;

        transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed);
    }
}
