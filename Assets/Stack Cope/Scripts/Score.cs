using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score;
    public static int seriesScore;

    [SerializeField] private Text scoreTextTotal;
    [SerializeField] private Text series;

    [SerializeField] private Canvas seriesCanvas;

    public Record record;
    [SerializeField] private Text bestRecord;

    void Start()
    {
        seriesScore = 0;
        score = 0;
        bestRecord.text = record.bestScore.ToString();
    }

    void Update()
    {
        string scoreText = score.ToString();
        scoreTextTotal.text = scoreText;

        if (seriesScore != 0)
        {
            seriesCanvas.gameObject.SetActive(true);
            series.text = seriesScore.ToString();
        }
        else
        {
            seriesCanvas.gameObject.SetActive(false);
        }
    }
}
