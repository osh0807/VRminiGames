using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    //public NavMeshAgent agent;
    //GameObject player;

    public float damage;
    public float health;

    public LevelTwoPlayerController levelTwo;
    public TextMeshProUGUI statusDisplayText;


    //public float sightRange;
    //public bool playerInSightRange;

    // Start is called before the first frame update

    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //agent = GetComponent<NavMeshAgent>();
        health = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(player.transform.position);
        
    }
    
    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject, 0.1f);
            Destroy(gameObject, 0.1f);

            levelTwo.Enemies.numEnemies--;
        }
    }
}
