using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreeze : MonoBehaviour
{

    public float attackCoolDown2 = 15.0f;
    public float attackExist = 5.0f;
    public float attackCool = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
   public float findattackCoolDown()
    {
        if (attackCoolDown2 >= 0)
        {
            attackCoolDown2 -= Time.deltaTime;
            attackExist = 5.0f;
        }
            if(Input.GetKeyDown("t") && attackCoolDown2 <= 0 && attackExist > 0)
        {
            attackExist -= Time.deltaTime;
        }
     
  
        if (attackExist <= 0)
        {
            attackCoolDown2 = attackCool;
        }
        return attackCoolDown2;
    }
}
