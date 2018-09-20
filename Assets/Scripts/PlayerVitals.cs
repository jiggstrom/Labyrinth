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

    public Text goldAmountText;

    // Use this for initialization
	void Start () {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        energySlider.maxValue = maxEnergy;
        energySlider.value = maxEnergy/8;
	}
	
	// Update is called once per frame
	void Update () {
		if(energySlider.value <= 0)
        {
            energySlider.value = 0;
            healthSlider.value -= Time.deltaTime * healthFallRate;
        }
        else
        {
            energySlider.value -= Time.deltaTime * energyFallRate;
        }

        if(healthSlider.value <= 0)
        {
            healthSlider.value = 0;
            Die();
        }

        if(energySlider.value > (maxEnergy *.90) && healthSlider.value < maxHealth)
        {
            healthSlider.value = Mathf.Clamp(healthSlider.value + Time.deltaTime * healthFallRate,0,maxHealth);
        }
	}
    private void Die()
    {
        var gm = FindObjectOfType<GameManager>();
        if (gm != null) gm.PlayerDeath();
    }

    public void TakeDamage(int DamageAmount)
    {
        healthSlider.value -= DamageAmount;
    }

    public void GetEnergy(int Energy)
    {
        energySlider.value = Mathf.Clamp(energySlider.value + Energy, 0, maxEnergy);
    }

    public void GetHealth(int Health)
    {
        healthSlider.value = Mathf.Clamp(healthSlider.value + Health,0,maxHealth);
    }

    public void GetGold(int Amount)
    {
        var tmpRes = Amount;
        if (int.TryParse(goldAmountText.text, out tmpRes))
        {
            tmpRes += Amount;
        }
        goldAmountText.text = tmpRes.ToString();
            
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            TakeDamage(enemy.Damage);
            Debug.Log("Attacked by " + enemy.Name);
        }
    }
}
