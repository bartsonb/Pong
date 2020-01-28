using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text playerOneText = null;
    public Text playerTwoText = null;

    public int[] points = new int[]{0, 0};

    void Start()
    {
        SetText();
    }

    void Update()
    {
        if (playerOneText.text != points[0].ToString() || playerTwoText.text != points[1].ToString()) 
            SetText();  
    }

    public void Increment(int index) 
    {
        if (index >= 0 && index <= points.Length - 1)
            points[index] = points[index] + 1;
    }

    private void SetText()
    {
        playerOneText.text = points[0].ToString();
        playerTwoText.text = points[1].ToString();
    }

    public bool maxPointsReached() 
    {
        return points[0] == GetComponent<Game>().maxNumberOfPoints || points[1] == GetComponent<Game>().maxNumberOfPoints;
    }
}
