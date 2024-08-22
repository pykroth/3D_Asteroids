using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBasic_Rocket : MonoBehaviour
{
    //Instance Variables 
    public float speed = 8.0f;
    public int damage = 20;
    public bool allowScreenWrap = false;

    public float existDuration = 15f;
    public float existTimer = 0.0f;
    public int critChance = 5;
    public GameObject spark;
   

    private Rigidbody laser;
    // Start is called before the first frame update
    void Start()
    {
        //Link the component to a variable
        laser = GetComponent<Rigidbody>();

        //Set a static speed of laser
        laser.velocity = transform.forward * speed;

       
}

    // Update is called once per frame
    void Update()
    {
        //Increases duration time
        existTimer += Time.deltaTime;
      
      
        
        //destroys the object when time passes
        if (existTimer >existDuration)
        {
            Destroy(this.gameObject);
        }
    }//end update
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
        if (isCritical)
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
