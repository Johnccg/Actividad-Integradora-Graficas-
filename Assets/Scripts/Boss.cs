using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject BossBullet;
    public int StreamNo;
    private int time = 10000;
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
        if ((TimeManager.tenSeconds % 50) == 0){
            StartCoroutine(Cicles(3, StreamNo));
        }

        if ((TimeManager.tenSeconds % 100) == 0 || TimeManager.tenSeconds == 0){
            time = TimeManager.tenSeconds;
            StartCoroutine(radiusShoot(180, 20));
        }else if (TimeManager.tenSeconds == (time + 10)){
            StartCoroutine(radiusShoot(189, 20));
        }
    }

    private IEnumerator Cicles(int repeat, int streams){
        int offsetMag = 360 / streams / 2;
        int offset = 0;
        for (int i = 0; i < repeat; i++){
            CircleShoot(streams, offset);
            offset += offsetMag;
            yield return new WaitForSeconds(0.7f);
        }
    }
    void CircleShoot(int streams, int offset)
    {
        int interval = 360 / streams;
        for (int i = 0; i < 360; i += interval){
            GameObject temp = Instantiate(BossBullet, transform.position, Quaternion.Euler(0,i + offset,0));
            temp.transform.SetParent(transform);
        }
    }

    private IEnumerator radiusShoot(int offset, int streams){
        for (int i = 0; i < 360; i += streams){
            GameObject temp = Instantiate(BossBullet, transform.position, Quaternion.Euler(0,i+offset,0));
            temp.transform.SetParent(transform);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
