using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject stage;
    public int speed;
    // Update is called once per frame
    void Start(){
        stage = GameObject.FindGameObjectWithTag("Stage");
    }
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if(transform.position.z <= (stage.transform.position.z - 43.6)){
            Destroy(gameObject);
        }
    }
}
