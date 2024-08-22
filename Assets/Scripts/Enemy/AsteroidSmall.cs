using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSmall : MonoBehaviour
{
    //Instance Variables
    //Rotation Variables

    public float tumbleMin = 3.5f;
    public float tumbleMax = 6.5f;

    //Movement Variables
    public float moveSpeedMin = 2.0f;
    public float moveSpeedMax = 6.0f;
    private Vector3 moveDirection;// Randomly generated direction of motion
    public float speed; //The actually randomly generated speed

    //Components
    private Rigidbody body;
    private Vector3 spin;
    private Vector3 spin2;
    //Gameplay Variables
    public GameObject explosion;
   // public GameObject smallAsteroidPrefab;
 //   public int numSmallAsteroids = 3;
    public int maxHP = 20;
    private int hp;
    public int scoreValue = 150;

    private LevelManager manager;
    public bool timers = false;
    public float attackCoolDown2 = 15.0f;
    public float attackExist = 5.0f;
    public float attackCool = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        //Set up Asteroid "Tumbling" (Rotation speed)
        float tumbleSpeed = Random.Range(tumbleMin, tumbleMax);


        //Apply rotation to the Rigidbody
        body.angularVelocity = Random.onUnitSphere * tumbleSpeed;
       
        spin = body.angularVelocity;

        spin2 = body.angularVelocity;
        //Set up Asteroid "Movespeed" 
        speed = Random.Range(moveSpeedMin, moveSpeedMax);
        moveDirection = Random.onUnitSphere;


        moveDirection.y = 0;
        moveDirection = moveDirection.normalized;

        //TODO: Gameplay Stuff
        hp = maxHP;
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelManager>();
        manager.changeEnemyCount(1); //Add a new asteroid to count
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Gameplay Stuff
      if(hp<= 0)
        {
            //Spawn the Explosion
            Instantiate(explosion, transform.position, transform.rotation);

            
            //Destroy the Asteroid
            Destroy(gameObject);


            //TODO: Add score & Decreasing enemy count
            manager.changeScore(scoreValue);
            manager.changeEnemyCount(-1);
           
        }
    }//endUpdate()

    private void FixedUpdate()
    {
        if (attackCoolDown2 >= 0)
            attackCoolDown2 -= Time.deltaTime;



        if (Input.GetKeyDown("t"))
        {
            timers = true;
            attackExist = 5.0f;
        }
        if (timers == true && attackExist > 0 && attackCoolDown2 <= 0)
        {
            attackExist -= Time.deltaTime;
            transform.Translate(moveDirection * speed * Time.deltaTime * 0, Space.World);
            spin = new Vector3(0, 0, 0);


        }
        else
        {
            //Manual. Non-Physics Movement
            transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
            spin = spin2;

        }
        if (attackExist <= 0)
        {
            timers = false;
        }
        if (attackExist <= 0 && timers == false)
        {
            attackCoolDown2 = attackCool;
        }

        Boundary.checkBoundary(gameObject);
    }//end FixedUpdate()


    private void OnTriggerEnter(Collider other)
    {
      //Hit by a player weapon
        if (other.gameObject.tag.Equals("PlayerProjectile")) 
        {
           //   Debug.Log("Collision with " + other.gameObject.name + "!");
        
            //Take Damage 
           hp -= other.gameObject.GetComponent<LaserBasic>().getDamage();

            //Spawn the Spark
            GameObject sparkToSpawn = other.gameObject.GetComponent<LaserBasic>().spark;

            GameObject theSpark = Instantiate(sparkToSpawn, other.transform.position, other.transform.rotation);
            theSpark.transform.Rotate(0, 180, 0);

            //Play hit effect.
            GetComponent<AudioSource>().Play();

                //TODO: Damage Numbers?

            //Delete the laser
            Destroy(other.gameObject);
        }
        //Bumps into another Asteroid
        if(other.gameObject.tag.Equals("Asteroid"))
        {
            //Vector3 rotation by 180 on y
            moveDirection = Quaternion.Euler(0, 180, 0) * moveDirection;
        }
        if (other.gameObject.tag.Equals("PlayerRocket1"))
        {
            // Debug.Log("Collision with " + other.gameObject.name + "!");

            //Take Damage 
            hp -= other.gameObject.GetComponent<LaserBasic_Rocket>().getDamage();

            //Spawn the Spark
            GameObject sparkToSpawn = other.gameObject.GetComponent<LaserBasic_Rocket>().spark;

            GameObject theSpark = Instantiate(sparkToSpawn, other.transform.position, other.transform.rotation);
            theSpark.transform.Rotate(0, 180, 0);



            //TODO: Damage Numbers?

            //Delete the laser
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag.Equals("PlayerRocket2"))
        {
            // Debug.Log("Collision with " + other.gameObject.name + "!");

            //Take Damage 
            hp -= other.gameObject.GetComponent<LaserBasic_Rocket2>().getDamage();

            //Spawn the Spark
            GameObject sparkToSpawn = other.gameObject.GetComponent<LaserBasic_Rocket2>().spark;

            GameObject theSpark = Instantiate(sparkToSpawn, other.transform.position, other.transform.rotation);
            theSpark.transform.Rotate(0, 180, 0);



            //TODO: Damage Numbers?

           
        }
    }

}


