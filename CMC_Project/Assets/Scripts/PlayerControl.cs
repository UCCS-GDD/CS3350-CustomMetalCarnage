﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public int health = 1;
    GameObject pause_text;
    private int maxHealth;
    private SpriteRenderer damageFlash;
    public float damageFlashDecay;
    public float redHealthDecay;
    private RectTransform healthFill;
    private RectTransform healthFillRed;

    public GameObject numberPrefab;
    private GameObject numberObject;
    private GameObject uiCanvas;

    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevel > 1)
        {
            damageFlash = GameObject.FindGameObjectWithTag("DamageFlash").GetComponent<SpriteRenderer>();
            maxHealth = health;
            healthFill = GameObject.FindGameObjectWithTag("HealthFill").GetComponent<RectTransform>();
            healthFillRed = GameObject.FindGameObjectWithTag("HealthFillRed").GetComponent<RectTransform>();
            uiCanvas = GameObject.FindGameObjectWithTag("Canvas");
            pause_text = GameObject.FindGameObjectWithTag("PauseTxt");
        }
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void TakeDamage(int incomingDamage)
    {
        health -= incomingDamage;
        health = (health < 0) ? 0 : health;
		health = (health > maxHealth) ? maxHealth : health;
        numberObject = Instantiate(numberPrefab, transform.position, Quaternion.identity) as GameObject;
        numberObject.transform.localPosition = numberPrefab.transform.localPosition;
        numberObject.transform.SetParent(uiCanvas.transform, false);
		if(incomingDamage > 0)
		{
        	numberObject.GetComponent<UnityEngine.UI.Text>().text = incomingDamage.ToString();
        	numberObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = incomingDamage.ToString();
			numberObject.transform.GetChild(0).GetComponent<ChangeColor>().targetColor = Color.red;
		}
		else if(incomingDamage < 0)
		{
			numberObject.GetComponent<UnityEngine.UI.Text>().text = incomingDamage.ToString().Replace("-", "+");
			numberObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = incomingDamage.ToString().Replace("-", "+");
			numberObject.transform.GetChild(0).GetComponent<ChangeColor>().targetColor = Color.green;
		}

        if (healthFill != null)
        {
            healthFill.localScale = new Vector3(((float)health / (float)maxHealth), healthFill.localScale.y, healthFill.localScale.z);
            StopCoroutine("HealthDecrease");
            StartCoroutine("HealthDecrease");
        }
        if ((damageFlash != null) && (incomingDamage > 0))
        {
            damageFlash.color = new Color(damageFlash.color.r, damageFlash.color.g, damageFlash.color.b, 1f);
            StopCoroutine("DamageFlash");
            StartCoroutine("DamageFlash");
        }




        if (health == 0)
        {
            // Player dies
            //Debug.Log("Player died.");



            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerControl>().PlayerDeath();
        }
    }


    IEnumerator DamageFlash()
    {
        //Debug.Log(damageFlash.color.a);
        while (damageFlash.color.a > 0f)
        {
            if ((damageFlash.color.a - damageFlashDecay) > 0)
            {
                damageFlash.color = new Color(damageFlash.color.r, damageFlash.color.g, damageFlash.color.b, damageFlash.color.a - damageFlashDecay);
            }
            else
            {
                damageFlash.color = new Color(damageFlash.color.r, damageFlash.color.g, damageFlash.color.b, 0f);
            }
            yield return null;
        }
    }


    IEnumerator HealthDecrease()
    {
        while (healthFillRed.localScale.x > healthFill.localScale.x)
        {
            healthFillRed.localScale = new Vector3(healthFillRed.localScale.x - redHealthDecay, healthFillRed.localScale.y, healthFillRed.localScale.z);
            if (healthFillRed.localScale.x < healthFill.localScale.x)
            {
                healthFillRed.localScale = healthFill.localScale;
            }

            yield return null;
        }
    }



}
