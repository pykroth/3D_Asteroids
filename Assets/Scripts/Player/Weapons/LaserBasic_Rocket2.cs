using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBasic_Rocket2 : MonoBehaviour
{
    public float speed = 8.0f;
    public int damage = 60;
    public bool allowScreenWrap = false;

    public float existDuration = 6f;
    public float existTimer = 0.0f;
    public int critChance = 5;
    public GameObject spark;
    private Rigidbody laser;
    public float turnSpeed = 300.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Link the component to the variable
        laser = GetComponent<Rigidbody>();

        //set a static speed of the laser
     
    }

    // Update is called once per frame
    void Update()
    {
        if (allowScreenWrap == true)
            Boundary.checkBoundary(gameObject);


        //Increase the duration timer
        existTimer += Time.deltaTime;
        Vector3 mousePos = Input.mousePosition;
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        target.y = 0;
        //    Debug.Log(target);
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
        //Actually rotate, but with a smooth amount of rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        laser.velocity = transform.forward * speed;
        //  Debug.Log("Laser Time: " + existTimer + " of  " + existDuration);
        //delete the laser if it has been alive oo long
        //this must be the last thing in update
        if (existTimer > existDuration)
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
