using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject ball;
    public GameObject racketLeft;
    public GameObject racketRight;
    public GameObject hint;
    public GameObject winnerMessage;

    public Text roundsPlayedText;
    public Text maxNumberOfPointsText;
    public Text winnerMessageText;

    public AudioSource soundGameWon;

    public ParticleSystem particleSystem;

    private bool roundInProgress = false;
    public bool isInputEnabled = true;

    public int maxNumberOfPoints;
    public int maxNumberOfRounds;

    public int currentRound = 1;

    void Start()
    {   
        particleSystem = GameObject.Find("Particle System").GetComponent<ParticleSystem>();

        maxNumberOfPoints = PlayerPrefs.GetInt("maxNumberOfPoints");
        maxNumberOfRounds = PlayerPrefs.GetInt("maxNumberOfRounds");

        maxNumberOfPointsText.text = "First to reach " + maxNumberOfPoints + " points wins";
        roundsPlayedText.text = currentRound.ToString() + " / " + maxNumberOfRounds;

        hint.SetActive(true);
        winnerMessage.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !roundInProgress)
            StartRound();
    }

    public void StartRound() 
    {
        roundInProgress = true;
        
        hint.SetActive(false);
        ball.GetComponent<Ball>().Go();

        InvokeRepeating("IncreaseBallSpeed", 5.0f, 5.0f);
    }

    public void PlayerScored(int index) 
    {
        isInputEnabled = false;

        GetComponent<Score>().Increment(index);
        currentRound++;

        shootParticles(
            ball.GetComponent<Rigidbody2D>().position, 
            index == 0 ? "left" : "right");

        ball.GetComponent<Ball>().Freeze();
        racketLeft.GetComponent<Racket>().Freeze();
        racketRight.GetComponent<Racket>().Freeze();

        CancelInvoke("IncreaseBallSpeed");

        if (!maxRoundsReached() && !GetComponent<Score>().maxPointsReached()) {
            // Start next round
            StartCoroutine(resetPlayfield());
        } else {
            EndGame();
        }
    }

    IEnumerator resetPlayfield()
    {
        yield return new WaitForSeconds(2);

        roundInProgress = false;
        isInputEnabled = true;

        hint.SetActive(true);

        roundsPlayedText.text = currentRound.ToString() + " / " + maxNumberOfRounds;

        ball.GetComponent<Ball>().Reset();
        racketLeft.GetComponent<Racket>().Reset();
        racketRight.GetComponent<Racket>().Reset();    
    }

    private void EndGame()
    {
        GetComponent<Timer>().Stop();

        winnerMessage.SetActive(true);

        string text = "";
        switch(GetComponent<Score>().GetLeadingPlayer()) {
            case 0:
                text = "Player 1 Wins";
                break;

            case 1:
                text = "Player 2 Wins";
                break;
            
            case -1:
                text = "Draw";
                break;
        }
        
        winnerMessageText.text = text;

        shootParticles(new Vector2(0, 0), "up");
    }

    private void IncreaseBallSpeed()
    {
        ball.GetComponent<Ball>().IncreaseSpeed();
    }

    private bool maxRoundsReached() 
    {
        return currentRound > maxNumberOfRounds;
    }

    private void shootParticles(Vector2 position, string direction)
    {
        particleSystem.transform.position = position;

        switch (direction) {
            case "left":
                particleSystem.transform.rotation = Quaternion.Euler(180.0f, 90.0f, 0.0f);
                break;

            case "right":
                particleSystem.transform.rotation = Quaternion.Euler(180.0f, -90.0f, 0.0f);
                break;

            case "up":
                particleSystem.transform.rotation = Quaternion.Euler(-90.0f, 90.0f, 0.0f);
                break;
        }

        particleSystem.Play();
    }
}
