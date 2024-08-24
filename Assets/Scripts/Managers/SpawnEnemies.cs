using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Enemies;
    private int posZ;
    private bool invert;
    private bool finished = true;
    private float delay;
    void Start(){
        posZ = Random.Range(-10, 4);
        invert = Random.value > 0.5f;
        delay = Random.Range(0.5f, 2f);
    }
    void Update(){
        if(finished & TimeManager.tenSeconds <= 400 & TimeManager.tenSeconds >= 50){
            StartCoroutine(spawnEnemy());
            posZ = Random.Range(-10, 4);
            invert = Random.value > 0.5f;
            delay = Random.Range(0.5f, 2f);
        }
    }

    private IEnumerator spawnEnemy(){
        finished = false;
        if (invert){
            GameObject temp = Instantiate(Enemy, new Vector3(-30,0,posZ), Quaternion.Euler(0,0,180));
            temp.transform.SetParent(Enemies.transform);
        }else{
            GameObject temp = Instantiate(Enemy, new Vector3(30,0,posZ), Quaternion.identity);
            temp.transform.SetParent(Enemies.transform);
        }
        yield return new WaitForSeconds(delay);
        finished = true;
    }
}
