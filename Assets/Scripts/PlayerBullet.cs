using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int speed;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        transform.Rotate(Vector3.forward,Time.deltaTime * 500);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Boss"){
            Boss script = target.GetComponent<Boss>();
            script.dealDamage(1);
            Destroy(gameObject);
        }
        else if (target.tag == "Enemy"){
            // Boss script = target.GetComponent<Boss>();
            // script.dealDamage(1);
            // Destroy(gameObject);
        }
    }
}
