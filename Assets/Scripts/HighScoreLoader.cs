using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using TMPro;

public class HighScoreLoader : MonoBehaviour{
    public TextMeshProUGUI highScores;
    public TextMeshProUGUI scoreInfo;
    public TMP_InputField inputField;
    public GameObject baseScoreInfo;
    public GameObject highScorePanel;
    string fileName = "score.txt";
    private int maxChars = 40;
    private SortedList<int, string> scoreByName = new SortedList<int, string>(new DescendedScoreComparer());
    private int currentScore;
    private string playerName;

    //Int32.Parse(input)
    // Start is called before the first frame update
    void Start(){
        Cursor.lockState = CursorLockMode.None;
        loadBasicScore();
        loadHighScores();
        if (isHighScore())
        {
            baseScoreInfo.SetActive(false);
            highScorePanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update(){
        //Debug.Log("current score: " + currentScore);
    }

    private string createLine(int score, string name){
        string line = name;
        int scoreLength = (int)Math.Floor(Math.Log10(score)) + 1;
        int nameAndScoreLength = name.Length + scoreLength;
        for (int i = 0; i < maxChars - nameAndScoreLength; i++)
        {
            line += ".";
        }

        return line += score;
    }

    private bool isHighScore()
    {
        return scoreByName.Keys[0] < currentScore;
    }

    private void loadBasicScore()
    {
        string distanceTravelled = PlayerPrefs.GetFloat("score").ToString("F0");
        if (scoreInfo){
            scoreInfo.text = "You've travelled a decent bit: " + distanceTravelled + "!";
        }
        currentScore = Int32.Parse(distanceTravelled);
    }

    private void loadHighScores()
    {
        string scoreFile = Application.streamingAssetsPath + "/" + fileName;
        List<string> lines = File.ReadAllLines(scoreFile).ToList();
        foreach (string line in lines)
        {
            int position = line.IndexOf("=");
            int score = Int32.Parse(line.Substring(0, position));
            string name = line.Substring(position + 1);
            Debug.Log("score: " + score);
            Debug.Log("name: " + name);
            if (!scoreByName.ContainsKey(score))
            {
                scoreByName.Add(score, name);
            }
            //scoreByName.Add
            //Debug.Log("Name: " + line.Substring(0, position));
            //Debug.Log("Score: " + line.Substring(position + 1));
        }
        if (highScores)
        {
            foreach (int score in scoreByName.Keys)
            {
                highScores.text += createLine(score, scoreByName[score]) + "\n";
                //int score;
                //Debug.Log("Name: " + name);
                //Debug.Log("Score: " + scoreByName[name]);
            }
        }
    }

    public void submitHighScore()
    {
        playerName = inputField.text;
        StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + "/" + fileName, true);
        writer.WriteLine(currentScore + "=" + playerName);
        writer.Close();
    }

    class DescendedScoreComparer : IComparer<int>{
        public int Compare(int x, int y){
            // use the default comparer to do the original comparison for datetimes
            int ascendingResult = Comparer<int>.Default.Compare(x, y);

            // turn the result around
            return 0 - ascendingResult;
        }
    }
}
