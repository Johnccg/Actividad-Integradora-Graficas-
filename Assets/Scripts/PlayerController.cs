using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float horizontalInput;
    private float forwardInput;
    public HealthUI healthbar;
    private int health = 100;
    public GameObject PlayerBullet;
    public GameObject BulletHolder;
    public float cooldown;
    private bool canShoot = true;
    public static Action GameOver;
    // Start is called before the first frame update
    void Start()
    {
        healthbar.setMaxHealth(health);
    }

    void Update()
    {
        if(health <= 0){
            GameOver?.Invoke();
            Destroy(gameObject);
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        forwardInput = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)){
            speed /= 2;
        }else if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)){
            speed *= 2;
        }

        if(Input.GetKey(KeyCode.Space)){
            if(canShoot){
                StartCoroutine(shootCooldown());
                GameObject temp = Instantiate(PlayerBullet, transform.position, Quaternion.Euler(0,180,0));
                temp.transform.SetParent(BulletHolder.transform);
            }
        }
    }

    public void OnEnable(){
        EnemyBullet.damagePlayer += takeDamage;
        Boss.damagePlayer += takeDamage;
    }

    public void OnDisable(){
        EnemyBullet.damagePlayer -= takeDamage;
        Boss.damagePlayer -= takeDamage;
    }
    private IEnumerator shootCooldown(){
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
    
    private void takeDamage(){
        health -= 10;
        healthbar.setHealth(health);
    }
}
