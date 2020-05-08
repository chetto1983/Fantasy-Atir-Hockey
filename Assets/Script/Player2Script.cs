using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;

public class Player2Script : MonoBehaviour
{

    Transformer transformer;

    float forceDir;
    
    public float strikerSpeed;
    float smashCance;
    GameObject ball;
    Rigidbody strikerRB;



    public static Player2Script current;


    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Striker");
        strikerRB = GameObject.FindGameObjectWithTag("Striker").GetComponent<Rigidbody>();
        transformer = GetComponent<Transformer>();
    }

    private void Update()
    {
        if (!GameManagerScript.current.isgoal)
        {

            if (transform.position.z < 0f)
            {
                transformer.enabled = false;
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

            }

            if (transform.position.z > 4.45f)
            {
                transformer.enabled = false;
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 4.45f);

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


    void OnCollisionEnter(Collision c)
    {
        //if (c.gameObject.tag == "Striker")
        //{
        //    //counter = 0f; //on hitting reset the wait time...
        //    forceDir = (int)Random.Range(0, 10);//for hitting to the left or right
        //    smashCance = (int)Random.Range(0, 10); //chance that it will smash the striker
        //                                           //hit the striker with force
        //    if (forceDir <= 5)
        //        strikerRB.velocity = new Vector3(-strikerSpeed, strikerRB.velocity.y, -strikerSpeed);
        //    else
        //        strikerRB.velocity = new Vector3(strikerSpeed, strikerRB.velocity.y, -strikerSpeed);

        //}

    }



    public void StopMoving()
    {
        transformer.enabled = false;

    }


    public void StartMoving()
    {
        transformer.enabled = true;

    }


}
