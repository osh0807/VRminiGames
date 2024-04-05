using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    public AudioSource audioSource;

    private float rf;
    public float duration = 5f;

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();

    }
    // Start is called before the first frame update
    void Start()
    {
        rf = Random.Range(10f, 30f);
        audioSource.PlayDelayed(rf);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= rf + duration)
        {
            audioSource.Stop();
        }
    }
}