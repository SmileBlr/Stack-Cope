using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private GameObject firstPart;
    [SerializeField] private GameObject secondPart;
    [SerializeField] private float perfectClickValue;

    [SerializeField] private CameraController camera;
    [SerializeField] private ColorController colorController;
    [SerializeField] private LoseController loseController;
    [SerializeField] private SoundController soundController;

    GameObject _currentPart;GameObject _lastPart;

    private float series;
    void Awake()
    {
        _lastPart = GameObject.Find("StartPart");
    }

    internal void Stop()
    {
        if (Score.score % 2 == 0)
        {
            _currentPart = firstPart;
            float distance = FixDistanceZ();
            float direction = FixDirection(distance);

            if (Mathf.Abs(distance) < perfectClickValue)
            {
                _currentPart.transform.position = new Vector3(_lastPart.transform.position.x, _currentPart.transform.position.y, _lastPart.transform.position.z);
                series += 0.1f;
                soundController.Click(series);
                if (series == 0.4f)
                {
                    Vector3 bonusUp = new Vector3(0.1f, 0, 0.1f);
                    _currentPart.transform.localScale += bonusUp;
                    series = 0;
                    Score.seriesScore = -1;
                }
                Score.seriesScore++;
                SpawnLastPart();
            }
            else if (Mathf.Abs(distance) > _lastPart.transform.localScale.z)
            {
                loseController.Lose();
            }
            else
            {
                soundController.Click();
                Score.seriesScore = 0;
                series = 0;
                CutZ(distance, direction);
            }
        }
        else
        {
            _currentPart = secondPart;
            float distance = FixDistanceX();
            float direction = FixDirection(distance);

            if (Mathf.Abs(distance) < perfectClickValue)
            {
                _currentPart.transform.position = new Vector3(_lastPart.transform.position.x, _currentPart.transform.position.y, _lastPart.transform.position.z);
                series += 0.1f;
                soundController.Click(series);
                if (series == 0.4f)
                {
                    Vector3 bonusUp = new Vector3(0.1f, 0, 0.1f);
                    _currentPart.transform.localScale += bonusUp;
                    series = 0;
                    Score.seriesScore = -1;
                }
                Score.seriesScore++;
                SpawnLastPart();
            }
            else if (Mathf.Abs(distance) > _lastPart.transform.localScale.x)
            {
                loseController.Lose();
            }
            else
            {
                soundController.Click();
                Score.seriesScore = 0;
                series = 0;
                CutX(distance, direction);
            }
        }
    }

    float FixDistanceZ()
    {
        float distance = _currentPart.transform.position.z - _lastPart.transform.position.z;
        if (Mathf.Abs(distance) > _lastPart.transform.localScale.z)
        {
            _currentPart.SetActive(false);
        }

        return distance;
    }
    float FixDistanceX()
    {
        float distance = _currentPart.transform.position.x - _lastPart.transform.position.x;
        if (Mathf.Abs(distance) > _lastPart.transform.localScale.x)
        {
            _currentPart.SetActive(false);
        }

        return distance;
    }
    float FixDirection(float distance)
    {
        float direction = distance > 0 ? 1f : -1f;
        return direction;
    }
    void CutZ(float distance, float direction)
    {
        float newZSize = _lastPart.transform.localScale.x - Mathf.Abs(distance);
        float fallingPartSize = _currentPart.transform.localScale.x - newZSize;

        float newZposition = _lastPart.transform.position.z + (distance / 2f);
        _currentPart.transform.localScale = new Vector3(_currentPart.transform.localScale.x,_currentPart.transform.localScale.y,newZSize);
        _currentPart.transform.position = new Vector3(_currentPart.transform.position.x,_currentPart.transform.position.y,newZposition);

        float partEdge = _currentPart.transform.position.z + (newZSize / 2f * direction);
        float fallingPartZPosition = partEdge + fallingPartSize / 2f * direction;

        SpawnFallingPart(fallingPartZPosition, fallingPartSize);
        SpawnLastPart();
    }
    void CutX(float distance, float direction)
    {
        float newXSize = _lastPart.transform.localScale.x - Mathf.Abs(distance);
        float fallingPartSize = _currentPart.transform.localScale.x - newXSize;

        float newXposition = _lastPart.transform.position.x + (distance / 2f);
        _currentPart.transform.localScale = new Vector3(_currentPart.transform.localScale.x, _currentPart.transform.localScale.y, newXSize);
        _currentPart.transform.position = new Vector3(newXposition, _currentPart.transform.position.y, _currentPart.transform.position.z);

        float partEdge = _currentPart.transform.position.x + (newXSize / 2f * direction);
        float fallingPartXPosition = partEdge + fallingPartSize / 2f * direction;

        SpawnFallingPart(fallingPartXPosition, fallingPartSize);
        SpawnLastPart();
    }

    void SpawnLastPart()
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

        obj.transform.localScale = _currentPart.transform.localScale;
        obj.transform.position = _currentPart.transform.position;

        //SetColor
        obj.GetComponent<Renderer>().material.color = _currentPart.GetComponent<Renderer>().material.color;

        if (secondPart.activeInHierarchy) obj.transform.rotation = Quaternion.Euler(0, -90f, 0);
        _lastPart = obj;

        Destroy(obj,70f);
        //cameraPosition
        camera.UpCamera(_lastPart.transform);

        GoStartPosition();
    }
    void SpawnFallingPart(float fallingPartPosition, float fallingPartSize)
    {
        var part = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //SetColor
        part.GetComponent<Renderer>().material.color = _currentPart.GetComponent<Renderer>().material.color;

        if (firstPart.activeInHierarchy)
        {
            part.transform.localScale = new Vector3(_currentPart.transform.localScale.x,
                _currentPart.transform.localScale.y, fallingPartSize);
            part.transform.position = new Vector3(_currentPart.transform.position.x, _currentPart.transform.position.y,
                fallingPartPosition);
        }
        else
        {
            part.transform.localScale = new Vector3(_currentPart.transform.localScale.x,
                _currentPart.transform.localScale.y, fallingPartSize);
            part.transform.position = new Vector3(fallingPartPosition, _currentPart.transform.position.y,
                _currentPart.transform.position.z);
            part.transform.rotation = Quaternion.Euler(0, -90f, 0);
        }

        part.AddComponent<Rigidbody>();
        Destroy(part, 2f);
    }
    void GoStartPosition()
    {
        Vector3 up = new Vector3(0, 0.2f, 0);
        if (firstPart.activeInHierarchy)
        {
            firstPart.SetActive(false);
            firstPart.transform.position += up;

            secondPart.transform.localScale = new Vector3(_lastPart.transform.localScale.z, _lastPart.transform.localScale.y, _lastPart.transform.localScale.x);
            secondPart.transform.position = new Vector3(2.8f,secondPart.transform.position.y,_lastPart.transform.position.z);
            secondPart.GetComponent<MovePart>().moveSpeed = 1f + (Score.score/60f)+series;

            //ChangeColor
            secondPart.GetComponent<Renderer>().material.color = colorController.ChangeColor();

            _currentPart = secondPart;
            _currentPart.SetActive(true);
            Score.score++;
        }

        else
        {
            secondPart.SetActive(false);
            secondPart.transform.position += up;

            firstPart.transform.localScale = new Vector3(_lastPart.transform.localScale.z, _lastPart.transform.localScale.y, _lastPart.transform.localScale.x);
            firstPart.transform.position = new Vector3(_lastPart.transform.position.x, firstPart.transform.position.y, -2.8f);
            firstPart.GetComponent<MovePart>().moveSpeed = 1f + (Score.score / 60f);

            //ChangeColor
            firstPart.GetComponent<Renderer>().material.color = colorController.ChangeColor();

            _currentPart = firstPart;
            _currentPart.SetActive(true);
            Score.score++;
        }
    }

}
