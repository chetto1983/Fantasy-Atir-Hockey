using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{


    public static GameManagerScript current;

    public int modality;// 1 AI, 2 giocatori
    private int fsm;
    private bool isStarted,ballCreated,isWin,isGameover,isReplayed;
    public bool isgoal;
    public float difficulties;

    public Text scorePlayer1Txt, scorePlayer2Txt,modeTxt,difficulty;
    public int scorePlayer1, scorePlayer2;
    public GameObject panelStartObj, easyBt, normalBt, hardBt,ballObj, player1Obj, player2Obj,aiObj,set3Obj, set7Obj, set15Obj;
    public Rigidbody ballRb;
    public GameObject goalPlayer1, goalPlayer2,winPlayer1, winPlayer2,gameOverPanel;
    public int goal; //1,2;
    public int maxScore;


    private void Awake()
    {
        current = this;
    }


    // Start is called before the first frame update
    void Start()
    {


        fsm = 0;
        isStarted = false;
        isgoal = false;
        goal = 0;




    }

    // Update is called once per frame
    void Update()
    {

        switch (fsm)
        {
            case 0:
                // tappa 0 faccio partire il pannello di start

                panelStartObj.SetActive(true);
                scorePlayer1 = 0;
                scorePlayer2 = 0;
                Difficulties();
                Modality();
                MaxScore();
                fsm = 1;
                break;

            case 1:
                if (isStarted)
                {
                    PlayerPrefs.SetInt("modality", modality);
                    PlayerPrefs.SetInt("maxScore", maxScore);
                    PlayerPrefs.SetFloat("difficulties", difficulties);
                    panelStartObj.SetActive(false);
                    fsm = 2;
                }

                break;
            case 2:

                // gioco partito istanzio gli oggetti
                ballObj =  Instantiate(ballObj, new Vector3(0f, 0.6f, 0f), ballObj.transform.rotation) as GameObject;
                ballRb = ballObj.GetComponent<Rigidbody>();
                player1Obj = Instantiate(player1Obj, new Vector3(0f, 0.7f, -3.45f), player1Obj.transform.rotation);
                if (modality == 1)
                    Instantiate(aiObj, new Vector3(0f, 0.7f, 3.45f), aiObj.transform.rotation) ;
                else
                    player2Obj= Instantiate(player2Obj, new Vector3(0f, 0.7f, 3.45f), player2Obj.transform.rotation) as GameObject ;
                fsm = 3;

                break;


            case 3:
                // aspetto il goal
               // goal = 0;
                if (isgoal)
                {

                    ballObj.SetActive(false);
                    Player1Script.current.StopMoving();
                    if (modality == 2)
                    {
                        Player2Script.current.StopMoving();
                    }

                    fsm = 4;
                }

                break;


            case 4:

                // Goal!!!!!!

                scorePlayer1Txt.text = scorePlayer1.ToString();
                scorePlayer2Txt.text = scorePlayer2.ToString();
                if (goal == 1)
                    goalPlayer1.SetActive(true);
                else
                    goalPlayer2.SetActive(true);
                    

                if (scorePlayer1 < maxScore  && scorePlayer2 < maxScore)
                {
                    
                    Invoke("CreateBall", 2f);
                    fsm = 5;

                }

                else
                {
                    Invoke("Win", 2f);
                    fsm = 6;
                }
                

                break;

            case 5:

                
                if (ballCreated)
                {
                   // goal = 0;
                    goalPlayer1.SetActive(false);
                    goalPlayer2.SetActive(false);
                    ballCreated = false;
                    fsm = 3;


                }
                

                break;



            case 6:
                if (isWin)
                    fsm = 7;

                    break;


            case 7:
                isWin = false;
                winPlayer1.SetActive(false);
                winPlayer1.SetActive(false);
                Invoke("GameOver", 2f);
                fsm = 8;
                break;



            case 8:
                if (isGameover)
                {
                    isGameover = false;
                    fsm = 9;
                    
                }

                break;


            case 9:

                if (isReplayed)
                {
                    isReplayed = false;
                    fsm = 3;
                    
                }
                    


                break;



        }



        
    }


    #region setting
    public void Set1Player()
    {

        modality = 1;
        modeTxt.text = "1 Player";
        easyBt.SetActive(true);
        normalBt.SetActive(true);
        hardBt.SetActive(true);
        difficulty.enabled = true;
       
    }


    public void Set2Player()
    {
        modality = 2;
       

        modeTxt.text = "2 Player";
        easyBt.SetActive(false);
        normalBt.SetActive(false);
        hardBt.SetActive(false);
        difficulty.enabled = false;
       

    }

    public void SetEasy()
    {
        difficulties = 0.2f;
        easyBt.GetComponent<Image>().color = Color.green;
        normalBt.GetComponent<Image>().color = Color.white;
        hardBt.GetComponent<Image>().color = Color.white;
       

    }

    public void SetNormal()
    {

        difficulties = 0.7f;
        easyBt.GetComponent<Image>().color = Color.white;
        normalBt.GetComponent<Image>().color = Color.green;
        hardBt.GetComponent<Image>().color = Color.white;
       


    }

    public void SetHard()
    {

        difficulties = 1f;
        easyBt.GetComponent<Image>().color = Color.white;
        normalBt.GetComponent<Image>().color = Color.white;
        hardBt.GetComponent<Image>().color = Color.green;
        

    }
    


    public void SetMaxScore3()
    {

        maxScore = 3;
        set3Obj.GetComponent<Image>().color = Color.green;
        set7Obj.GetComponent<Image>().color = Color.white;
        set15Obj.GetComponent<Image>().color = Color.white;
    }

    public void SetMaxScore7()
    {

        maxScore = 7;
        set3Obj.GetComponent<Image>().color = Color.white;
        set7Obj.GetComponent<Image>().color = Color.green;
        set15Obj.GetComponent<Image>().color = Color.white;
    }

    public void SetMaxScore15()
    {

        maxScore = 15;
        set3Obj.GetComponent<Image>().color = Color.white;
        set7Obj.GetComponent<Image>().color = Color.white;
        set15Obj.GetComponent<Image>().color = Color.green;
    }


    void Difficulties()
    {
        if (PlayerPrefs.HasKey("difficulties"))
            difficulties = PlayerPrefs.GetFloat("difficulties");
        else
            difficulties = 0.2f;
            PlayerPrefs.SetFloat("difficulties", difficulties);


        if (difficulties == 0.2f)
            SetEasy();
        else
            if (difficulties == 0.7f)
            SetNormal();
        else
            if (difficulties == 1f)
            SetHard();
    }

    void Modality()
    {
        if (PlayerPrefs.HasKey("modality"))
            modality = PlayerPrefs.GetInt("modality");
        else
            modality = 1;
            PlayerPrefs.SetInt("modality", modality);


        if (modality == 1)
            Set1Player();
        else
            Set2Player();
        
    }

    void MaxScore()
    {
        if (PlayerPrefs.HasKey("maxScore"))
            maxScore = PlayerPrefs.GetInt("maxScore");
        else
            maxScore = 7;
        PlayerPrefs.SetInt("maxScore", maxScore);


        if (maxScore == 3)
            SetMaxScore3();
        else
            if (maxScore == 7)
            SetMaxScore7();
        else
            if (maxScore == 15)
            SetMaxScore15();
    }

    #endregion




    void CreateBall()
    {

        ballObj.SetActive(true);
        ballRb.velocity = Vector3.zero;
        if (goal == 1)
            ballObj.transform.position = new Vector3(0f, 0.6f, 1.95f);
        else 
            if (goal ==2)
            ballObj.transform.position = new Vector3(0f, 0.6f, -1.95f);
        else
            ballObj.transform.position = new Vector3(0f, 0.6f, 0f);



        player1Obj.transform.position = new Vector3(0f, 0.7f, -3.45f);

        if (modality == 2)
            player2Obj.transform.position = new Vector3(0f, 0.7f, 3.45f);

        ballCreated = true;
        isgoal = false;
        BallScript.current.isCollided = false;

    }


    void Win()
    {
        goalPlayer1.SetActive(false);
        goalPlayer2.SetActive(false);
        if (scorePlayer1 >= maxScore)
            winPlayer1.SetActive(true);
        else
            winPlayer2.SetActive(true);
        isWin = true;
    }


    public void StartGame()
    {
        if(modality>0 && difficulties > 0f && maxScore >0)
            isStarted = true;
    }

   void GameOver()
    {
        winPlayer1.SetActive(false);
        winPlayer2.SetActive(false);
        gameOverPanel.SetActive(true);
        isGameover = true;
    }


    public void Replay()
    {
        gameOverPanel.SetActive(false);
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        goal = 0;
        isgoal = false;
        isWin = false;
        scorePlayer1Txt.text = scorePlayer1.ToString();
        scorePlayer2Txt.text = scorePlayer2.ToString();
        CreateBall();
        
        isReplayed = true;
    }

    public void Settings()
    {
        SceneManager.LoadScene(0);

    }


      

}
