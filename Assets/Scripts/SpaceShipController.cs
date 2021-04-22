using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{

    public float maxHoverSpeed, maxDirectSpeed, maxStrafeSpeed;
    private float activeHoverSpeed, activeForwardSpeed, activeStrafeSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2.5f, hoverAcceleration = 2.5f;
    
    private Rigidbody ship;

    private float lookSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;
    
    
    public float zeroToSixty = 0.2f; // The interval with which thrust increases every sec;
    private float currentThrust; // Current force being applied
    public float maxThrust = 2; // Max Force Engine is capable of
    private float timeUp, timeDown, timeRight, timeLeft;

    private KeyCode forward = KeyCode.W;
    private KeyCode back = KeyCode.S;
    private KeyCode right = KeyCode.D;
    private KeyCode left = KeyCode.A;
    private KeyCode up = KeyCode.Space;
    private KeyCode down = KeyCode.LeftControl;
    public float rotationspeed = 20 ;
    public int timeInterval;

    private float activeRotationY, activeRotationX, activeRotationZ;
    float rotationYAcc = 2f, rotationXAcc = 2f;

    public float brakeStrength;


    
    Vector3 angleVelocity;
    void Start()
    {
        Physics.gravity = new Vector3(0, 0, 0);
        ship = GetComponent<Rigidbody>();
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;
        
        

    }

   
    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.x;
        
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
        
        
    }

    private void FixedUpdate()
    {
        /*Vector3 velocityCarry = new Vector3(strafeSpeed * activeStrafeSpeed, hoverSpeed * activeHoverSpeed,
            forwardSpeed * activeForwardSpeed) * Time.deltaTime;
        ship.velocity  = velocityCarry;
        */
        applyRotation();
        
        applyMovement();

        
        
    }

    private void applyRotation()
    {

        activeRotationZ =0;
        activeRotationX = Mathf.Lerp(ship.rotation.x,  (mouseDistance.y * -1f)  * rotationspeed, rotationXAcc * Time.fixedDeltaTime);
        activeRotationY = Mathf.Lerp(ship.rotation.y, mouseDistance.x* rotationspeed, rotationYAcc * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.E))
        {
            activeRotationZ =  -1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            activeRotationZ = 1;
        }
        
        angleVelocity = new Vector3(  activeRotationX * rotationspeed,  activeRotationY* rotationspeed, activeRotationZ*rotationspeed );

        Quaternion deltaRotation = Quaternion.Euler(angleVelocity * Time.fixedDeltaTime);
        ship.MoveRotation(ship.rotation * deltaRotation);
    }
    private void applyMovement()
    {
       
            if (Input.GetKeyDown(forward))
            {
                timeUp = Time.time;
            }

            if (Input.GetKey(forward))
            {
                if ((Time.time - timeUp > timeInterval) && currentThrust < maxThrust)
                {
                    ship.velocity = new Vector3(ship.velocity.x, ship.velocity.y , ship.velocity.z + (transform.forward.z * currentThrust));
                    currentThrust = +zeroToSixty;
                }
                else
                {
                    if (currentThrust > 0)
                    {
                        currentThrust = 0;
                    }
                }

            }

            if (Input.GetKeyDown(back))
            {
                timeUp = Time.time;
            }

            if (Input.GetKey(back))
            {
                if (Time.time - timeUp > (timeInterval) && currentThrust < maxThrust)
                {
                    ship.velocity = new Vector3(ship.velocity.x, ship.velocity.y, ship.velocity.z + ((-1f * transform.forward.z) * currentThrust) );
                    currentThrust = +zeroToSixty;
                }
                else
                {
                    if (currentThrust > 0)
                    {
                        currentThrust = 0;
                    }
                }

            }
        
       

      
            if (Input.GetKeyDown(right))
            {
                timeUp = Time.time;
            }

            if (Input.GetKey(right))
            {
                if (Time.time - timeUp > (timeInterval) && currentThrust < maxThrust)
                {
                    ship.velocity = new Vector3(ship.velocity.x + (transform.right.x * currentThrust), ship.velocity.y, ship.velocity.z);
                    currentThrust = +zeroToSixty;
                }
                else
                {
                    if (currentThrust > 0)
                    {
                        currentThrust = 0;
                    }
                }
            }
        
            if (Input.GetKeyDown(left))
            {
                timeUp = Time.time;
            }

            if (Input.GetKey(left))
            {
                if (Time.time - timeUp > (timeInterval) && currentThrust < maxThrust)
                {
                    ship.velocity = new Vector3(ship.velocity.x + ((-1f * transform.right.x) * currentThrust), ship.velocity.y, ship.velocity.z);
                    currentThrust =+ zeroToSixty;
                }
                else
                {
                    if (currentThrust > 0)
                    {
                        currentThrust = 0;
                    }
                }
            }
        

        
            if (Input.GetKey(up))
            {
                if (Time.time - timeUp > (timeInterval) && currentThrust < maxThrust)
                {
                    ship.velocity = new Vector3(ship.velocity.x , ship.velocity.y + (transform.up.y * currentThrust), ship.velocity.z);
                    currentThrust =+ zeroToSixty;
                }
                else
                {
                    if (currentThrust > 0)
                    {
                        currentThrust = 0;
                    }
                }
            }
        
            if (Input.GetKey(down))
            {
                if (Time.time - timeUp > (timeInterval) && currentThrust < maxThrust)
                {
                    ship.velocity = new Vector3(ship.velocity.x , ship.velocity.y + ((-1f * transform.up.y) * currentThrust), ship.velocity.z);
                    currentThrust =+ zeroToSixty;
                }
                else
                {
                    if (currentThrust > 0)
                    {
                        currentThrust = 0;
                    }
                }
            }
            

            float speed = Vector3.Magnitude (ship.velocity);  // test current object speed
      
            if (speed > maxDirectSpeed)
 
            {
                float brakeSpeed = speed - maxDirectSpeed;  // calculate the speed decrease
 
                Vector3 normalisedVelocity = ship.velocity.normalized;
                Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value
 
                ship.AddForce(-brakeVelocity);  // apply opposing brake force
            }

            if (Input.GetKey(KeyCode.X))
            {
                float brakeSpeed = brakeStrength;
               
                Vector3 normalisedVelocity = ship.velocity.normalized;
                Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value
 
                ship.AddForce(-brakeVelocity);  // apply opposing brake force
                
                
            }

       
        
        
    }

    bool isPositive(float num)
    {
        return (num > 0);
    }
    float clampValue(float value,float max)
    {
        if (value <= max)
        {
            return value;
        }
        else
        {
            return max;

        }
    }
}
