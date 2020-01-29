using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    private Game Game;

    public float speed = 30;
    public string axis = "Vertical";

    void Start()
    {
        Game = GameObject.Find("Main Camera").GetComponent<Game>(); 
    }

    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        float v = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;
    }

    public void Reset()
    {
        Unfreeze();
        
        if (axis == "Vertical") 
            GetComponent<Rigidbody2D>().position = new Vector2(-29, 0);

        if (axis == "Vertical2")
            GetComponent<Rigidbody2D>().position = new Vector2(29, 0);
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
}
