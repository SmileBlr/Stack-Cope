using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    //160-290
    //38
    //87
    private float _currentcolorH;
    private bool _reverseColorChange;
    private float _S;
    private float _V;

    [SerializeField] private Image image;

    void Start()
    {
        _S = 38f/100f;
        _V = 87f/100f;
        _currentcolorH = 160f;
        _reverseColorChange = false;
    }

    void Change()
    {
        if (_currentcolorH == 800) _reverseColorChange = true;
        else if (_currentcolorH == 0) _reverseColorChange = false;
        if (!_reverseColorChange) _currentcolorH += 40f;
        else if (_reverseColorChange) _currentcolorH -= 40f;
    }
    internal Color ChangeColor()
    {
       Change();
       Color color = Color.HSVToRGB((_currentcolorH/1000f),_S,_V);
       image.color = color;
       return color;
    }
}
