using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
    private Game Game;

    public AudioSource soundHitWall;
    public AudioSource soundHitRacket;
    public AudioSource soundScored;

    public const int DEFAULT_SPEED = 30;
    public float speed = DEFAULT_SPEED;

    void Start() 
    {
        Game = GameObject.Find("Main Camera").GetComponent<Game>(); 
    }

    void Update() 
    {

    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        string name = col.gameObject.name;
        // col.transform.position is the objects's position
        // col.collider is the objects's collider

        if (name == "WallTop" || name == "WallBottom") 
            soundHitWall.Play(0);

        if (name == "WallRight") {
            soundScored.Play(0);
            Game.PlayerScored(0);
        }
        
        if (name == "WallLeft") {
            soundScored.Play(0);
            Game.PlayerScored(1);
        }
    
        if (name.Contains("Racket")) {
            soundHitRacket.Play(0);

            // Calculate direction, make length=1 via .normalized
            // Set Velocity with dir * speed
            if (name == "RacketLeft") {
                float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
                Vector2 dir = new Vector2(1, y).normalized;

                GetComponent<Rigidbody2D>().velocity = dir * speed;
            }

            if (name == "RacketRight") {
                float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
                Vector2 dir = new Vector2(-1, y).normalized;

                GetComponent<Rigidbody2D>().velocity = dir * speed;
            }
        }
    }

    private float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight) 
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    public void Go()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    public void Reset() 
    {
        Unfreeze();

        speed = DEFAULT_SPEED;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().position = new Vector2(0, 0);
    }

    public void Freeze()
    {
        GetComponent<Rigidbody2D>().constraints = 
            RigidbodyConstraints2D.FreezePositionX | 
            RigidbodyConstraints2D.FreezePositionY |
            RigidbodyConstraints2D.FreezeRotation;
    }
    
    public void Unfreeze()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void IncreaseSpeed()
    {
        Debug.Log("New Speed: " + speed.ToString());
        speed = speed * 1.2f;
    }
}