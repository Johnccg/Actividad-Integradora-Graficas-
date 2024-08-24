using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private int speed;
    public GameObject EnemyBullet;
    private GameObject BulletHolder;
    private float delay;
    private bool finished = true;
    private int health = 2;
    public static Action damagePlayer;
    public GameObject multishot;
    public GameObject speedup;
    void Start() {
        BulletHolder = GameObject.FindGameObjectWithTag("Holder");
        delay = UnityEngine.Random.Range(0.8f, 2f);
        speed = UnityEngine.Random.Range(5, 10);
    }

    void Update() {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if(health <= 0){
            if (UnityEngine.Random.value > 0.5f){
                if (UnityEngine.Random.value > 0.6f){
                    Instantiate(speedup, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
                }else{
                    Instantiate(multishot, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
        if (finished) {
            StartCoroutine(Shoot(delay));
            delay = UnityEngine.Random.Range(0.3f, 1.5f);
        }
        if (transform.position.x > 31 || transform.position.x < -31) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider target)
    {
        if(target.name == "Player"){
            damagePlayer?.Invoke();
        }
    }

    private IEnumerator Shoot(float delay)
    {
        finished = false;
        GameObject temp = Instantiate(EnemyBullet, transform.position, Quaternion.identity);
        temp.transform.SetParent(BulletHolder.transform);
        yield return new WaitForSeconds(delay);
        finished = true;
    }

    public void dealDamage(int damage){
        health -= damage;
    }
}
