using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    //Instance Variables
    //Gameplay Variables
    public int damage = 10; //10 damage per laster
    //Rate of Fire(Time inbetween Attacks)
    public float attackCoolDown = 0.2f;
    //50dps
    private float attackTimer = 0.0f;

   

    //PREFAB LINKS
    //**You need to set this in the Unity Editor
    public GameObject laser; //The laser prefab to spawn

    //Component Variable
    public GameObject shotSpawnPoint;
    private AudioSource sound;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //ways to link "Laser Hardpoint" child object to the variable
        //Can crash
        //shotSpawnPoint = GameObject.Find("Laser Hardpoint");

        sound = shotSpawnPoint.GetComponent<AudioSource>();

    
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        //CoolDown the player's attack
       if(attackTimer> 0)
        {
            attackTimer -= Time.deltaTime;

        }
        //Fire the laser
        //Input.GetButton("Fire1") returns true contiuously while the Fire1 button is held
        //Input.GetButtonDown("Fire1") Returns true ONCE the first frame Fire1 is down
        //Input.GetButtonUp("Fire1")  Returns true ONCE the frame Fire1 is released

        //How a laser will work: GetButton("Fire1") -> attack by holding down the mouse
        //                       GetButtonUp("Fire1") -> Attack by tapping the mouse.
        if (Input.GetButton("Fire1") && attackTimer <= 0.0f)
        {
            //Spawn the laser
            GameObject tempLaser = Instantiate(laser, shotSpawnPoint.transform.position, shotSpawnPoint.transform.rotation);

            //Set the laser's damage
            tempLaser.GetComponent<LaserBasic>().damage = this.damage;

            //Set the attackTimer cooldown
            attackTimer = attackCoolDown;

            //Play the sound effect
            sound.Play();


        }
    }

}
