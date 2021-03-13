using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreChecker : MonoBehaviour
{
    private float currentScore;
    public GameObject scorePanel;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = PlayerPrefs.GetFloat("score");
        if (true)
        {
            scorePanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
