using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    // Public Outlets
    public AudioSource backgroundAudioSource;
    public AudioSource randomAudioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();

    // Check if random sound is playing and if whole process should continue
    private bool _isPlayingRandomSound;
    private bool _toContinue;
    private float _timer;
    private float _randomizedTimer;

    private bool _isStarted;

    // Wait time
    private float _randomWaitTime;
    private float _waitMin = 5.0f;
    private float _waitMax = 10.0f;
    private float _minAudioLength = 5.0f;
    private float _maxAudioLength = 7.0f;


    // Start is called before the first frame update
    void Start()
    {
        _isStarted = false;
        _toContinue = false;
        _isPlayingRandomSound = false;
        _timer = 0.0f;
        _randomizedTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _randomWaitTime = Random.Range(_waitMin, _waitMax);
        _timer += Time.deltaTime;

        // starting the randomization process if it has not started yet
        if (_toContinue && !_isStarted)
        {
            _isStarted = true;
            _randomizedTimer = (_timer + _randomWaitTime);
            backgroundAudioSource.Play();
        }
        else if (_toContinue && _isStarted)
        {
            // it is time to play random sound
            if (_timer > _randomizedTimer && !_isPlayingRandomSound)
            {
                int index = Random.Range(0, audioClips.Count);
                randomAudioSource.clip = audioClips[Mathf.Abs(index)];
                float playLength = Random.Range(_minAudioLength, _maxAudioLength);
                _randomizedTimer += playLength;

                backgroundAudioSource.Pause();
                _isPlayingRandomSound = true;
                randomAudioSource.Play();
            }
            else if (_timer > _randomizedTimer && _isPlayingRandomSound)
            {
                randomAudioSource.Stop();
                _isPlayingRandomSound = false;
                backgroundAudioSource.UnPause();
                _randomizedTimer += _randomWaitTime;
                backgroundAudioSource.UnPause();
            }
        }
    }

    public void StartRandomSoundRoutine()
    {
        if (_isStarted == false)
        {
            _isPlayingRandomSound = false;
            _toContinue = true;
        }
    }

    public void StopRandomSoundRoutine()
    {
        if (_isStarted)
        {
            _toContinue = false;
            _isPlayingRandomSound = false;
            _isStarted = false;
            randomAudioSource.Stop();
            backgroundAudioSource.Stop();
        }
    }

    public bool GetRandomSoundStatus()
    {
        return _isPlayingRandomSound;
    }
}