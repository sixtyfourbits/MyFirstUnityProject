using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ememy : MonoBehaviour
{

    [SerializeField] int damage;

    [SerializeField] int health;
    int oriHealth;
    [SerializeField] Text healthText;
    [SerializeField] RectTransform healthBar;


    private void Start()
    {
        oriHealth = health;
        SetStatus();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.GetComponent<player>().GetDamage(damage);
            WaveSpawner.waveSpawner.WaveEndCheck(this.transform);
            Destroy(this.gameObject);
        }
    }

    public void GetDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            WaveSpawner.waveSpawner.WaveEndCheck(this.transform);
            Destroy(this.gameObject);
        }
 
        SetStatus();
    }

    public void SetStatus()
    {
        healthText.text = health.ToString() + " / " + oriHealth.ToString() + "HP";
        healthBar.localScale = new Vector3((float)health / (float)oriHealth, 1, 1);

    }
}
