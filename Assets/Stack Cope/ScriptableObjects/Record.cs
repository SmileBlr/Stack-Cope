using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
[CreateAssetMenu(menuName = "Score/BestScore",fileName = "BestRecord")]
public class Record : ScriptableObject
{
    public int bestScore;
}
