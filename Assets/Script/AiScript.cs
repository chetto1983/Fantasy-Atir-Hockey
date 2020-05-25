using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiScript : MonoBehaviour
{

    public static AiScript current;
        public float speed;
        GameObject ball;
        Rigidbody strikerRB;
        float forceDir;
        public float counter;
        public float strikerSpeed;
        Vector3 basePoint;
        public float difficulty;
        float smashCance;

        void Awake()
        {
        current = this;
            difficulty = GameManagerScript.current.difficulties;

        }

        // Use this for initialization
        void Start()
        {
            ball = GameObject.FindGameObjectWithTag("Striker");
            strikerRB = GameObject.FindGameObjectWithTag("Striker").GetComponent<Rigidbody>();


            //set difficulty
            //based on the value the more the difficulty the more behind or close to the goal the ai is
            //it plays defensive and gets extra time to think and play in high difficulty
            //ideally difficuly = 1 must be invincible, if there are no game glitches

            if (difficulty < 0.45f)
            { //easy
                basePoint = new Vector3(0, transform.position.y, 0.85f);
                difficulty = 0.2f;
            }
            else if (difficulty >= 0.45f && difficulty < 1f)
            {
                basePoint = new Vector3(0, transform.position.y, 1.5f);
                difficulty = 0.5f;
            }
            else if (difficulty == 1f)
            {
                basePoint = new Vector3(0, transform.position.y, 3.8f);
            }

        }

        // Update is called once per frame
        void Update()
        {

        if (!GameManagerScript.current.isgoal)
        {


            counter += 1f * Time.deltaTime; //acts like a timer
           // Debug.Log(counter);
            if (ball.transform.position.z >= -0.2f)
            { //if striker is in its half
                if (counter >= 1f)
                { //wait for one second to see if the stiker comes to you or stops, if it does not come then :
                    Vector3 newPos = new Vector3(ball.transform.position.x - 0.2f, ball.transform.position.y, ball.transform.position.z + 0.2f);
                    //move towards the striker to its position
                    transform.position = Vector3.MoveTowards(transform.position, newPos, 4f * Time.deltaTime);
                    //4f * Time.deltaTime states time to transit the two positions

                }
                else //if less than 1 second change your x-position based on difficulty, i.e. try to move closer to striker
                    transform.position = new Vector3(ball.transform.position.x * difficulty, transform.position.y , transform.position.z);
            }
            else //if in other half then move towards base position
                transform.position = Vector3.MoveTowards(transform.position, basePoint, 2f * Time.deltaTime);

        }

        else
        {
            transform.position = Vector3.zero;
            transform.position = new Vector3(0f, 0.7f, 3.45f);
            counter = 0;
        }

        }

        void OnCollisionEnter(Collision c)
        {
        if (c.gameObject.tag == "Striker")
        {
            counter = 0f; //on hitting reset the wait time...
            forceDir = (int)Random.Range(0, 10);//for hitting to the left or right
            smashCance = (int)Random.Range(0, 10); //chance that it will smash the striker
                                                   //hit the striker with force
            if (forceDir <= 5)
                strikerRB.velocity = new Vector3(-strikerSpeed, strikerRB.velocity.y, -strikerSpeed);
            else
                strikerRB.velocity = new Vector3(strikerSpeed, strikerRB.velocity.y, -strikerSpeed);

        }

        }




}

