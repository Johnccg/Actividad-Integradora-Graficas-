using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject background;
    public GameObject stage;
    private GameObject[] getCount;

    void Update()
    {
        getCount = GameObject.FindGameObjectsWithTag ("BG");
        int count = getCount.Length;
        if (count < 2){
            GameObject temp = Instantiate(background,stage.transform.position + new Vector3(-0.1522508f, -7.5f, 70.4f), Quaternion.identity);
            temp.transform.SetParent(stage.transform);
        }
    }
}
