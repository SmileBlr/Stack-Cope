using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MoveController currentControllet;
    public bool gameStart;

    public void GameStart(bool start)
    {
        gameStart = start;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && gameStart)
        {
            currentControllet.Stop();
        }
    }
}
