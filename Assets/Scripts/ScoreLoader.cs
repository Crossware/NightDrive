using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLoader : MonoBehaviour
{
    public TextMeshProUGUI scoreInfo;

    // Start is called before the first frame update
    void Start()
    {
        string distanceTravelled = PlayerPrefs.GetFloat("score").ToString("F0");
        scoreInfo.text = "You've travelled a decent bit: " + distanceTravelled + "!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
