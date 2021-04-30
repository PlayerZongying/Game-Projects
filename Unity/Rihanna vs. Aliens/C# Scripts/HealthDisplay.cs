using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] float baseHealth = 5;
    [SerializeField] int damage = 1;
    float health;
    Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth - 2 * PlayerPrefsController.GetDifficulty();
        healthText = GetComponent<Text>();
        UpdateDisplay();
    }

    // Update is called once per frame
    private void UpdateDisplay()
    {
        healthText.text = health.ToString();
    }

    public void takeHealth()
    {
        health -= damage;
        UpdateDisplay();

        if(health <= 0)
        {
            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
    }


}
