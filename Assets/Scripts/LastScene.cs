using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LastScene : MonoBehaviour
{

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float _maxZoom;

    [SerializeField]
    private float _timeToZoomInSeconds;

    private float _initialZoom;

    private int _velocity;

    private List<Ship> _ships;


    private void Start()
    {
        _initialZoom = _camera.orthographicSize;
        _ships = FindObjectsOfType<Ship>().ToList();
        gameObject.SetActive(false);
    }

    public void ZoomOutCamera()
    {
        gameObject.SetActive(true);
        var fadeIn = FindObjectOfType<FadeIn>();
        fadeIn.Run();
        foreach (var ship in _ships)
        {
            ship.StartCoroutine("Fire");
        }
        StartCoroutine(MoveCamera());
    }

    public void GameOver()
    {
        Debug.Log("Shutting down game");
        Application.Quit();
    }

    private IEnumerator MoveCamera()
    {
        float elapsedTime = 0;

        while (elapsedTime < _timeToZoomInSeconds)
        {
            _camera.orthographicSize = Mathf.Lerp(_initialZoom, _maxZoom, elapsedTime / _timeToZoomInSeconds);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        var gameOver = GameObject.Find("Dialogue System/Canvas/Game Over");
        Debug.Log(gameOver);
        gameOver.SetActive(true);


    }

}
