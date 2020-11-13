using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    float speed = 3f; // 쌓는 속도


    float time = 0f; 

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if(time > speed)
        {
            time = 0;

        }
    }
}
