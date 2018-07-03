using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVitals : MonoBehaviour {

    public Slider healthSlider;
    public int maxHealth;
    public int healthFallRate;

    public Slider energySlider;
    public int maxEnergy;
    public int energyFallRate;

    // Use this for initialization
	void Start () {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        energySlider.maxValue = maxEnergy;
        energySlider.value = maxEnergy;
	}
	
	// Update is called once per frame
	void Update () {
		if(energySlider.value <= 0)
        {
            healthSlider.value -= Time.deltaTime * healthFallRate;
        }
        else
        {
            energySlider.value -= Time.deltaTime * energyFallRate;
        }

        if(healthSlider.value <= 0)
        {
            Die();
        }

	}
    private void Die()
    {
        var gm = FindObjectOfType<GameManager>();
        if (gm != null) gm.PlayerDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        healthSlider.value -= 10;
        Debug.Log("onTriggerEnter");
    }
}
