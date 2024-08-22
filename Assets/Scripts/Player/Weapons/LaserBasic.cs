using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBasic : MonoBehaviour
{
    //Instance Variables:
    public float speed = 10.0f;
    public int damage = 5;
    public int critChance = 5;
    public bool allowScreenWrap = false;

    public float existDuration = 1.5f; //How long the laser will stay in the game for
    private float existTimer = 0.0f;

    private Rigidbody laser;
    public GameObject spark;

 

    // Start is called before the first frame update
    void Start()
    {
        //Link the component to the variable
        laser = GetComponent<Rigidbody>();

        //set a static speed of the laser
        laser.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if( allowScreenWrap == true)
            Boundary.checkBoundary(gameObject);
        

        //Increase the duration timer
        existTimer += Time.deltaTime;

      //  Debug.Log("Laser Time: " + existTimer + " of  " + existDuration);
        //delete the laser if it has been alive oo long
        //this must be the last thing in update
        if(existTimer>existDuration)
        {
            Destroy(this.gameObject);
        }
    }
    public int getDamage()
    {
        bool isCritical;
        
        //Generate a random number from 1 to 100
        int rand = Random.Range(0, 100);
        //If that random number is above 100-critChance
        if (rand >= 100 - critChance)
        {
            isCritical = true;
        }
        else
            isCritical = false; 

        //Calculate Damage
        int actualDamage = damage;
        if(isCritical)
        {
            actualDamage = (int)(actualDamage * 2.0);
        }
        Color textColor = new Color(255, 157, 61);
        Vector3 textSpawnLocation = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);

        //Create the damage popup
        DamagePopup.Create(textSpawnLocation, actualDamage, textColor, isCritical);

        return actualDamage;
       
    }

}
