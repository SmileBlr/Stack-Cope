using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseController : MonoBehaviour
{
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private GameObject firstPart;
    [SerializeField] private GameObject secondPart;

    public Record record;

    internal void Lose()
    {
        if (firstPart.activeInHierarchy)
        {
            firstPart.SetActive(false);
        }

        if (secondPart.activeInHierarchy)
        {
            secondPart.SetActive(false);
        }
        SaveRecord();

        loseCanvas.gameObject.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
    void SaveRecord()
    {
        if (record.bestScore < Score.score)
        {
            record.bestScore = Score.score;
        }
    }
    
}
