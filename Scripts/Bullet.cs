using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _lastPos;
    private const float BulletLiveTime = 5f;
    void Start()
    {
        _lastPos = transform.position;
        Destroy(gameObject, BulletLiveTime);
    }
    
    void Update()
    {
        var pos = transform.position;
        Debug.DrawLine(_lastPos, pos, Color.yellow, BulletLiveTime);
        _lastPos = pos;
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), other.GetContact(0).point, Quaternion.identity);
        Destroy(gameObject);
    }
}
