using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Boss : MonoBehaviour
{
    public GameObject BossBullet;
    public GameObject BulletHolder;
    public int speed;
    private bool updateSpeed;
    private int time = 500;
    private int second = 100000;
    private Vector3 target;
    private bool stay;
    public static Action damagePlayer;
    public int health = 60;
    private int startingHealth;
    public HealthUI healthbar;
    public static Action win;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GetRandomPosition();
        healthbar.setMaxHealth(health);
        startingHealth = health;
        animator.speed = 0f;
    }

    void Update()
    {
        if(health <= 0){
            win?.Invoke();
            Destroy(gameObject);
        }else if (health <= (startingHealth / 2) & !updateSpeed){
            speed *= 2;
            updateSpeed = true;
        }

        if (Vector3.Distance(transform.position, target) < 0.001f){
            target = GetRandomPosition();
        }
        if (!stay){
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider target)
    {
        if(target.name == "Player"){
            damagePlayer?.Invoke();
        }
    }

    public void OnEnable()
    {
        TimeManager.Tick += TimeCheck;
    }

    public void OnDisable()
    {
        TimeManager.Tick -= TimeCheck;

    }

    private void TimeCheck()
    {
        if (TimeManager.tenSeconds == (second + 10)){
            if (health <= (startingHealth / 2)){
                StartCoroutine(radiusShoot(189, 20, 3));
            }else{
                StartCoroutine(radiusShoot(189, 20, 1));
            }
        }

        if (TimeManager.tenSeconds >= time){
            stay = false;
            int choice = UnityEngine.Random.Range(1, 101);
            if(choice > 1 & choice <= 25){
                if (health <= (startingHealth / 2)){
                    time += 70;
                    stay = true;
                    StartCoroutine(Curve(10, 4));
                }else{
                    time += 50;
                    StartCoroutine(Cicles(3, 20));
                }
            }else if (choice > 25 & choice <= 75){
                time += 50;
                StartCoroutine(Cicles(3, 20));
            }else if (choice > 75 & choice <= 100){
                time += 50;
                stay = true;
                second = TimeManager.tenSeconds;
                if (health <= (startingHealth / 2)){
                    StartCoroutine(radiusShoot(180, 20, 3));
                }else{
                    StartCoroutine(radiusShoot(180, 20, 1));
                }
            }
        }
    }

    private IEnumerator Cicles(int repeat, int streams){
        animator.Play("BossShoot");
        int offsetMag = 360 / streams / 2;
        int offset = 0;
        for (int i = 0; i < repeat; i++){
            CircleShoot(streams, offset);
            offset += offsetMag;
            yield return new WaitForSeconds(0.8f);
        }
        animator.Play("Boss");
    }

    private IEnumerator radiusShoot(int offset, int advanceDeg, int streams){
        animator.Play("BossShoot");
        for (int i = 0; i < 360; i += advanceDeg){
            CircleShoot(streams, i + offset);
            yield return new WaitForSeconds(0.1f);
        }
        animator.Play("Boss");
    }

    private IEnumerator Curve(int bullets, int streams){
        animator.Play("BossShoot");
        for (int i = 0; i < bullets; i ++){
            CircleShoot(streams, true, true);
            CircleShoot(streams, true, false);
            yield return new WaitForSeconds(0.5f);
        }
        animator.Play("Boss");
    }
    private void CircleShoot(int streams, int offset)
    {
        int interval = 360 / streams;
        for (int i = 0; i < 360; i += interval){
            GameObject temp = Instantiate(BossBullet, transform.position, Quaternion.Euler(0,i + offset,0));
            temp.transform.SetParent(BulletHolder.transform);
            if(health <= (startingHealth / 2)){
                EnemyBullet script = temp.GetComponent<EnemyBullet>();
                script.setSpeed(script.getSpeed() * 2);
            }
        }
    }

    private void CircleShoot(int streams, bool rotate, bool invert)
    {
        int interval = 360 / streams;
        for (int i = 0; i < 360; i += interval){
            GameObject temp = Instantiate(BossBullet, transform.position, Quaternion.Euler(0,i,0));
            EnemyBullet script = temp.GetComponent<EnemyBullet>();
            script.setRotate(rotate);
            script.setReverse(invert);
            temp.transform.SetParent(BulletHolder.transform);
            if(health <= (startingHealth / 2)){
                script.setSpeed(script.getSpeed() * 2);
            }
        }
    }

    private Vector3 GetRandomPosition(){
        float x = UnityEngine.Random.Range(-24, 25);
        float y = transform.position.y;
        float z = UnityEngine.Random.Range(-9, 1);
        return new Vector3(x,y,z);
    }

    public void dealDamage(int damage){
        health -= damage;
        healthbar.setHealth(health);
    }

    private void avanzarUnFrame()
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        float newTime = currentState.normalizedTime + (1f / animator.GetCurrentAnimatorClipInfo(0).Length);
        animator.Play(currentState.fullPathHash, -1, newTime);
    }
}
