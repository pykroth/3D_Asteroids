using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    //Instance Variables
    //Movement Variables
    public float moveSpeed = 32.0f; //Units per second
    public float turnSpeed = 300.0f; //Degrees per second
    [SerializeField] private bool useMouseControls = false;
    [SerializeField] private bool usePhysicsMovement = false;
    [SerializeField] private float maxSpeed = 10.0f;
    public GameObject explosion;
    private LevelManager manager;

    //Useful Variables
    private Rigidbody ship;
    private Light engineLight;
    private ParticleSystem engineParticles;
    private float lastParticleTime;
    private float particleRate = 1.0f / 30f;

    //Keyboard inputs
    private float inputV = 0.0f;
    private float inputH = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        //Link the components to variables
        //Attaches the component to the variable
        ship = GetComponent<Rigidbody>();
        
        //engineLight = GameObject.Find("Engine Light").GetComponent<Light>();
     engineLight = GetComponentInChildren<Light>();
        engineParticles = GetComponentInChildren<ParticleSystem>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelManager>();
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        //Get Keyboard input
        inputV = Input.GetAxis("Vertical"); //-1.0 to 1.0
        inputH = Input.GetAxis("Horizontal"); //-1.0 to 1.0
        if(inputV != 0.0f)
        {
            //If the liught is off, turn it off.
            if(engineLight.enabled == false)
            {
                engineLight.enabled = true;
            }

            //Limit the emission of particles to the variable particleRate
            float timeSinceLastParticle = Time.time - lastParticleTime;
            if (Time.time - lastParticleTime > particleRate)
            {
                engineParticles.Emit(1);
                lastParticleTime = Time.time;
            }
        }
        else
        {
            //if we're not pushing forward or backward
            //turns on light if off
            if (engineLight.enabled == true)
            {
                engineLight.enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //************
        //Movement
        //************
        //Trigger this if the keys are pressed
        if (!usePhysicsMovement && inputV != 0.0f)
        {
            Vector3 currentPos = transform.position;
            Vector3 moveDirection = transform.forward * inputV * moveSpeed * Time.deltaTime;

            ship.MovePosition(currentPos + moveDirection);
            //Maybe
            //inputV = 0;
        }
        else if (usePhysicsMovement && inputV > 0.0f) //Pressing Back
        {
            //Make a force vector to apply to the ship.
            //Lower the moveSpeed multiplier to make reverse slower
            Vector3 forwardForce = transform.forward * inputV * moveSpeed*50 * Time.deltaTime;

            Vector3 localVelocity = transform.InverseTransformDirection(ship.velocity);
         //   Debug.Log(ship.velocity.magnitude);
           // Debug.Log(localVelocity.z);

            if (localVelocity.z <= maxSpeed* .5)//Caps reverse at -5 velocity.
            ship.AddForce(forwardForce, ForceMode.Acceleration);


            
        }


        //************
        //Rotation
        //************
        if (!useMouseControls && inputH != 0.0f)
        {
            Quaternion rotationAmt = Quaternion.Euler(0f, inputH * turnSpeed * Time.deltaTime, 0f);
            Quaternion currentRotation = ship.rotation;

            ship.MoveRotation(currentRotation * rotationAmt);
            //inputH = 0;
        }
        else if(useMouseControls)
        {
            //Get Mouse Location
            Vector3 mousePos = Input.mousePosition;
            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,  mousePos.y, Camera.main.nearClipPlane));

            //Compute the direction
            target.y = transform.position.y;
            //Compute the direction
           
            Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
            //Actually rotate, but with a smooth amount of rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);


            //Other usefull comands
            int pixelWidth = Screen.width;
            int pixelLength = Screen.height;
        }//end if mouse controls

        Boundary.checkBoundary(this.gameObject);

    }//end FixedUpdate()

    private void OnTriggerEnter(Collider other)
    {
        //Check for an asteroid
        if(other.gameObject.tag.Equals("Asteroid"))
        {
            //Save the last direction the player was facing
            manager.setLastPlayerRotation(ship.rotation);
            //Delete player and spawn explosion
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);

            //Respawn the player if possible
            manager.spawnPlayer();
        }
    }
}//end playerShip Class
