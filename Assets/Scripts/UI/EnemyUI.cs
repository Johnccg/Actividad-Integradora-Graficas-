using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    private GameObject[] getCount;
    public TextMeshProUGUI enemyCount;

    // Update is called once per frame
    void Update()
    {
        getCount = GameObject.FindGameObjectsWithTag ("Enemy");
        int count = getCount.Length;
        enemyCount.text = $"Enemy count: {count}";
    }
}
