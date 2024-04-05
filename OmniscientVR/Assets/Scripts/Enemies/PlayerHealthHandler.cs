using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthHandler : MonoBehaviour
{
    public float health;
    public LevelTwoPlayerController player;
    public GameObject body;

    // Start is called before the first frame update
    public void Start()
    {
        player = GetComponent<LevelTwoPlayerController>();
        health = player.getPlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            health -= 8f;
            player.setPlayerHealth(health);
        }
    }

}
