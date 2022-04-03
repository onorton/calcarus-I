using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _music;
    private int _currentTrack;

    private AudioSource _musicSource;
    private AudioSource _soundSource;

    [SerializeField]
    private AudioClip _successSound;

    [SerializeField]
    private AudioClip _failureSound;

    [SerializeField]
    private AudioClip _warDeclaredSound;

    [SerializeField]
    private Sprite _mutedImage;
    [SerializeField]
    private Sprite _unmutedImage;
    private Image _muteIcon;

    private bool _muted;


    private void Start()
    {
        _musicSource = transform.Find("Music Source").GetComponent<AudioSource>();
        _soundSource = transform.Find("Sound Source").GetComponent<AudioSource>();
        _currentTrack = 0;
        _musicSource.clip = _music[_currentTrack];
        _musicSource.PlayDelayed(1);
        _muteIcon = GameObject.Find("Canvas/Mute Icon").GetComponent<Image>();
    }

    private void Update()
    {
        if (!_musicSource.isPlaying)
        {
            _currentTrack = (_currentTrack + 1) % _music.Count;
            _musicSource.clip = _music[_currentTrack];
            _musicSource.PlayDelayed(1);
        }
    }

    public void ChangeInHappiness(float newValue, float change)
    {
        if (change >= 0)
        {
            StartCoroutine(PlaySoundNotOverlapping(_soundSource, _successSound));
            _soundSource.PlayOneShot(_successSound);
        }
        else
        {
            StartCoroutine(PlaySoundNotOverlapping(_soundSource, _failureSound));
        }

    }

    public void WarDeclared()
    {
        _musicSource.mute = true;
        StartCoroutine(PlaySoundNotOverlapping(_soundSource, _warDeclaredSound));

    }

    private IEnumerator PlaySoundNotOverlapping(AudioSource audioSource, AudioClip audioClip)
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        audioSource.PlayOneShot(audioClip);
    }

    public void ToggleMute()
    {
        _muted = !_muted;
        _soundSource.mute = _muted;
        _musicSource.mute = _muted;
        if (_muted)
        {
            _muteIcon.sprite = _mutedImage;
        }
        else
        {
            _muteIcon.sprite = _unmutedImage;

        }
    }
}
