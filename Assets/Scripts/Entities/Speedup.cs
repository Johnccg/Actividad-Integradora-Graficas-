using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedup : MonoBehaviour
{
    public int speed;
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.name == "Player"){
            PlayerController script = target.GetComponent<PlayerController>();
            script.speedUp();
            Destroy(gameObject);
        }
    }
}
