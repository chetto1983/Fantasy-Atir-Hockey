using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;

public class Player1Script : MonoBehaviour
{
    
    Transformer transformer;


    public static Player1Script current;
    float forceDir;
    float smashCance;
    GameObject ball;
    Rigidbody strikerRB;
    public float strikerSpeed;



    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        transformer = GetComponent<Transformer>();
        ball = GameObject.FindGameObjectWithTag("Striker");
        strikerRB = GameObject.FindGameObjectWithTag("Striker").GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (!GameManagerScript.current.isgoal)
        {

            if (transform.position.z > 0f)
            {
                transformer.enabled = false;
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

            }


            if (transform.position.z < -4.2f)
            {
                transformer.enabled = false;
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -4.2f);

            }


            if (transform.position.x < -1.95f)
            {
                transformer.enabled = false;
                gameObject.transform.position = new Vector3(-1.95f, transform.position.y, transform.position.z);

            }

            if (transform.position.x > 1.95f)
            {
                transformer.enabled = false;
                gameObject.transform.position = new Vector3(1.95f, transform.position.y, transform.position.z);

            }

            if (!transformer.enabled)
                transformer.enabled = true;

        }
    }



    public void StopMoving()
    {
        transformer.enabled = false;

    }


    public void StartMoving()
    {
        transformer.enabled = true;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManagerScript.current.isgoal)
        {

            if (collision.gameObject.CompareTag("Striker") && GameManagerScript.current.modality == 1)
                AiScript.current.counter = 0f;
        }
        //if (collision.gameObject.tag == "Striker")
        //{

        //    forceDir = (int)Random.Range(0, 10);//for hitting to the left or right
        //    smashCance = (int)Random.Range(0, 10); //chance that it will smash the striker
        //                                           //hit the striker with force
        //    if (forceDir <= 5)
        //        strikerRB.velocity = new Vector3(-strikerSpeed, strikerRB.velocity.y, -strikerSpeed);
        //    else
        //        strikerRB.velocity = new Vector3(strikerSpeed, strikerRB.velocity.y, -strikerSpeed);

        //}


    }
        
    

}

