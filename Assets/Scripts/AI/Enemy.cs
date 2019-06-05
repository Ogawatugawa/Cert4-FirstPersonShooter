using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public Transform healthBarParent;
    public Transform healthBarPoint;
    public GameObject healthBarUIPrefab;

    private int curHealth;
    private Slider healthSlider;
    private Renderer rend;


    void Start()
    {
        // Spawn a new Health Bar in HealthBarParent
        GameObject clone = Instantiate(healthBarUIPrefab, healthBarParent);
        // Get Slider component from new Health Bar;
        healthSlider = clone.GetComponent<Slider>();
        // Set health to maxHealth;
        curHealth = maxHealth;
        // Get Renderer component from enemy
        rend = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        if (rend.isVisible)
        {
            // Activate the HealthBar
            healthSlider.gameObject.SetActive(true);
            // Update position of healthbar with enemy's transform    
            Vector3 screenPos = Camera.main.WorldToScreenPoint(healthBarPoint.position);
            healthSlider.transform.position = screenPos;
        }

        else
        {
            healthSlider.gameObject.SetActive(false);
        }

        /// You can do this way quicker by turning this if statement into:
        /// healthSlider.gameObject.SetActive(rend.isVisible);
        /// But it wastes processing because it processes the health bar's position regardless of whether it needs to show it

        healthSlider.value = (float)curHealth / (float)maxHealth;
    }

    public void TakeDamage (int damage)
    {
        // Reduce health with damage
        curHealth -= damage;
        // If health is reduced to zero
        if (curHealth <= 0)
        {
            // Destroy the slider
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (healthSlider)
        {
            Destroy(healthSlider.gameObject); 
        }
    }
}
