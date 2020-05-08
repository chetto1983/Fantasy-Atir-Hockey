using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{
    public static BallScript current;
    public bool isCollided;
    Rigidbody rb;
    Behaviour halo;

    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float minSpeed;
    int forceDir;
    public AudioClip goalFx,collFx;
    
    bool change= true;

    public float strikerSpeed;
    int smashCance;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        isCollided = false;
        rb = GetComponent<Rigidbody>();
       //halo = GetComponent<Behaviour>(); //Be careful if you have more than one Behaviour on your 
       halo = (Behaviour)gameObject.GetComponent("Halo");
    }


    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        else
          if (rb.velocity.magnitude < minSpeed)
        {
            rb.velocity = rb.velocity.normalized * minSpeed;

        }



    }


    private void OnCollisionEnter(Collision collision)
    {
        AudioManagerScript.current.PlaySound(collFx, 0.12f);
        halo.enabled = true;
        Invoke("StopHalo", 0.2f);

    }


    void StopHalo() { halo.enabled = false; }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GoalPlayer1") && !isCollided)
        {

            
            GameManagerScript.current.scorePlayer2 ++;
            isCollided = true;
            AudioManagerScript.current.PlaySound(goalFx, 0.2f);
            GameManagerScript.current.goal = 2;
            GameManagerScript.current.isgoal = true;

        }


        if (other.gameObject.CompareTag("GoalPlayer2")&& !isCollided)
        {

           
            GameManagerScript.current.scorePlayer1 ++;
            isCollided = true;
            AudioManagerScript.current.PlaySound(goalFx, 0.2f);
            GameManagerScript.current.goal = 1;
            GameManagerScript.current.isgoal = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("TestSpeed") && ! change)
        {
            change = true;

            forceDir = (int)Random.Range(0, 10);//for hitting to the left or right
            smashCance = (int)Random.Range(0, 10); //chance that it will smash the striker
                                                   //hit the striker with force
            if (forceDir <= 5)
                rb.velocity = new Vector3(-strikerSpeed, rb.velocity.y, -strikerSpeed);
            else
                rb.velocity = new Vector3(strikerSpeed, rb.velocity.y, -strikerSpeed);


        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TestSpeed"))
            change = false;

    }

}
