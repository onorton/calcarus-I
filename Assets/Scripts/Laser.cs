using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public Vector3 Direction { get; set; }

    [SerializeField]
    private float _speed;

    private void Update()
    {
        transform.Translate(Direction * _speed * Time.deltaTime);
    }
}
