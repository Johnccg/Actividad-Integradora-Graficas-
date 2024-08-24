using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject healthbar;
    void Start(){
        transform.position = new Vector3(0, 0, 16);
        healthbar.SetActive(false);
        Boss script = gameObject.GetComponent<Boss>();
        script.enabled = false;
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
        if (TimeManager.tenSeconds == 450)
        {
            StartCoroutine(Spawn_Boss());
        }else if (TimeManager.tenSeconds == 500){
            Boss script = gameObject.GetComponent<Boss>();
            script.enabled = true;
            healthbar.SetActive(true);
            this.enabled = false;
        }
    }

    private IEnumerator Spawn_Boss()
    {
        Vector3 targetPos = new Vector3(0,0,-9);
        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 5;

        while(timeElapsed < timeToMove){
            transform.position = Vector3.Lerp(currentPos,targetPos,timeElapsed/timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
