using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot2 : MonoBehaviour
{
    //Instance Variables
    public int damage3 = 70;
    public int damage2 = 25;
    public int damage = 5;
    public float attackCoolDown = 15.0f;
    private float attackTimer = 0.0f;
    public float turnSpeed = 300.0f;
    public float speed = 8.0f;
    public GameObject laser;
    public GameObject shotSpawnPoint;
    public float charge = 0.0f;
    public float existDuration = 15f;
    public GameObject laser2;
    public GameObject laser3;
    public float charge2 = 0;
    
    
  

    public float chargeSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCoolDown > 0)
            attackCoolDown -= Time.deltaTime;

        if(Input.GetKey("f"))
        {
            charge += Time.deltaTime;
        }
        getCharge(charge);
        //  Debug.Log(charge);


        if (Input.GetKeyUp("f")  && charge >= 0.0f && charge <= 5.0f)
        {
        
           GameObject tempLaser = Instantiate(laser, shotSpawnPoint.transform.position, shotSpawnPoint.transform.rotation);



          
            //Set the laser's damage
            tempLaser.GetComponent<LaserBasic>().damage = this.damage;

            //Set the attackTimer cooldown
            attackTimer = attackCoolDown;

          
          charge = 0;
        }
      else  if (Input.GetKeyUp("f")  && charge > 5.0f && charge <= 10.0f)
        {

            GameObject tempLaser = Instantiate(laser2, shotSpawnPoint.transform.position, shotSpawnPoint.transform.rotation);

            //Set the laser's damage
            tempLaser.GetComponent<LaserBasic_Rocket>().damage = this.damage2;

            //Set the attackTimer cooldown
            attackTimer = attackCoolDown;

        

       
            charge = 0;
        }
        else if (Input.GetKeyUp("f") && charge > 10.0f)
        {

            GameObject tempLaser = Instantiate(laser3, shotSpawnPoint.transform.position, shotSpawnPoint.transform.rotation);

         
            //Compute the direction
           
           // transform.rotation(target);
            //Set the laser's damage
            tempLaser.GetComponent<LaserBasic_Rocket2>().damage = this.damage3;

          
            //Set the attackTimer cooldown
            attackTimer = attackCoolDown;

       
         
            charge = 0;
        }
        // Debug.Log(charge);
     

    }
    public void getCharge(float temp)
    {
        charge2 = temp;
        
    }
    public float getCharges()
    {
        return charge2;
    }
}
