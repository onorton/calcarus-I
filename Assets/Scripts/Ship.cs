using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ship : MonoBehaviour
{

    [SerializeField]
    private GameObject _laser;

    private List<Transform> _laserOrigins;

    [SerializeField]
    private float _firingRate = 1f;

    [SerializeField]
    private float _movementSpeed = 0.2f;

    [SerializeField]
    private Vector3 _laserDirection;


    [SerializeField]
    private float _movementRange = 2f;

    [SerializeField]
    private AudioClip _laserSound;
    private AudioSource _audioSource;

    private float _top;
    private float _bottom;
    private float _current;

    private List<GameObject> _firedLasers;



    // Start is called before the first frame update
    void Start()
    {
        _laserOrigins = transform.Find("Laser Origins").GetComponentsInChildren<Transform>().Where(t => t.name != "Laser Origins").ToList();
        _top = transform.position.y + Random.Range(_movementRange * 0.5f, _movementRange);
        _bottom = transform.position.y - Random.Range(_movementRange * 0.5f, _movementRange);
        _current = Random.Range(0f, 1.0f) < 0.5f ? _top : _bottom;
        _firedLasers = new List<GameObject>();
        _audioSource = FindObjectOfType<AudioSource>();
    }

    private void Update()
    {
        var epsilon = 0.1f;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _current, transform.position.z), Time.deltaTime * _movementSpeed);

        if (Mathf.Abs(_current - transform.position.y) < epsilon)
        {
            if (_current == _bottom)
            {
                _current = _top;
            }
            else
            {
                _current = _bottom;
            }
        }

        foreach (var laser in _firedLasers.Where(laser => laser.transform.position.x >= 25f || laser.transform.position.x <= -25))
        {
            Destroy(laser);
        }

        _firedLasers.RemoveAll(laser => laser.transform.position.x >= 25f || laser.transform.position.x <= -25);

    }

    // Update is called once per frame
    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(Random.Range(0f, 2.0f));

        while (true)
        {
            var spawning = _laserOrigins[Random.Range(0, _laserOrigins.Count)];

            var laser = Instantiate(_laser, spawning.position, Quaternion.identity);
            laser.GetComponent<Laser>().Direction = _laserDirection;
            _firedLasers.Add(laser);
            _audioSource.PlayOneShot(_laserSound);

            yield return new WaitForSeconds(_firingRate);
        }

    }
}
