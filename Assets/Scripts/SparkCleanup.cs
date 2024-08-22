using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkCleanup : MonoBehaviour
{

    public float existDuration = 1.2f; //Stay alive for x seconds
    private float existTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        existTimer += Time.deltaTime;
        if (existTimer > existDuration)
            Destroy(gameObject);
    }
}
