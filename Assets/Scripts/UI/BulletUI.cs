using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletUI : MonoBehaviour
{
    private GameObject[] getCount;
    public TextMeshProUGUI bulletCount;

    // Update is called once per frame
    void Update()
    {
        getCount = GameObject.FindGameObjectsWithTag ("Bullet");
        int count = getCount.Length;
        bulletCount.text = $"Bullet count: {count}";
    }
}
