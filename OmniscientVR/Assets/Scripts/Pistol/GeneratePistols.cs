using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePistols : MonoBehaviour
{
    public GameObject _Pistol;

    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-3, 3), 5, Random.Range(-5, 5));
            Instantiate(_Pistol, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnWait);
        }
    }
}
