using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Text numberOfPointsText = null;
    public Text numberOfRoundsText = null;

    public const int MINIMUM_POINTS = 1;
    public const int MINIMUM_ROUNDS = 1;

    public int numberOfPoints = 2;
    public int numberOfRounds = 2;

    void Start()
    {
        setText();
    }

    void Update()
    {
        setText();
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("maxNumberOfPoints", numberOfPoints);
        PlayerPrefs.SetInt("maxNumberOfRounds", numberOfRounds);
    }

    public void decreaseNumberOfPoints() 
    {
        numberOfPoints = (numberOfPoints - 1 < MINIMUM_POINTS) ? MINIMUM_POINTS : numberOfPoints - 1;
    }

    public void increaseNumberOfPoints() 
    {
        numberOfPoints = numberOfPoints + 1;
    }

    public void decreaseNumberOfRounds() 
    {
        numberOfRounds = (numberOfRounds - 1 < MINIMUM_ROUNDS) ? MINIMUM_ROUNDS : numberOfRounds - 1;
    }

    public void increaseNumberOfRounds() 
    {
        numberOfRounds = numberOfRounds + 1;
    }

    private void setText() 
    {
        numberOfPointsText.text = numberOfPoints.ToString();
        numberOfRoundsText.text = numberOfRounds.ToString();
    }
}
