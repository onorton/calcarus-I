using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    private Image _image;

    [SerializeField]
    private float _timeToFadeInSeconds;


    private void Start()
    {
        _image = GetComponent<Image>();
        _image.CrossFadeAlpha(0.0f, 0.0f, false);
    }
    public void Run()
    {
        _image.CrossFadeAlpha(1.0f, 0.0f, false);
        _image.CrossFadeAlpha(0.0f, _timeToFadeInSeconds, false);

    }
}
