using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText = null;
    
    public float gameTime { get; private set; }
    private float timeOnStart;
    private bool stopped = false;

    void Start()
    {
        timeOnStart = Time.time;
        setText();
    }

    void Update()
    {
        gameTime = (!stopped) ? Time.time - timeOnStart : gameTime;

        setText();
    }

    private void setText()
    {
        timerText.text = Mathf.Round(gameTime).ToString() + " Sec.";
    }

    public void Stop()
    {
        stopped = true;
    }
}
