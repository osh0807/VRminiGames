using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image _HealthBar;
    public float currentHealth;
    private float maxHealth = 100f;
    LevelTwoPlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        _HealthBar = GetComponent<Image>();
        player = FindObjectOfType<LevelTwoPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = player.getPlayerHealth();
        _HealthBar.fillAmount = currentHealth / maxHealth;
    }
}
