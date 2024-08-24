using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int speed;
    private bool rotate;
    private bool reverse;
    public static Action damagePlayer;

    void Update()
    {
        if (!rotate){
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            transform.Rotate(Vector3.forward,Time.deltaTime * 500);
        }else{
            if (reverse){
                transform.Translate(Vector3.back * speed * Time.deltaTime);
                transform.Rotate(Vector3.down,Time.deltaTime * 20, Space.World);
                transform.Rotate(Vector3.forward,Time.deltaTime * 500);
            }else{
                transform.Translate(Vector3.back * speed * Time.deltaTime);
                transform.Rotate(Vector3.up,Time.deltaTime * 20, Space.World);
                transform.Rotate(Vector3.forward,Time.deltaTime * 500);
            }
            
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider target)
    {
        if(target.name == "Player"){
            damagePlayer?.Invoke();
            Destroy(gameObject);
        }
    }

    public void setRotate(bool _rotate){
        rotate = _rotate;
    }

    public void setReverse(bool _reverse){
        reverse = _reverse;
    }

    public int getSpeed(){
        return speed;
    }

    public void setSpeed(int _speed){
        speed = _speed;
    }
}
