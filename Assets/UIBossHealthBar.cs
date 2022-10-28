using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBossHealthBar : MonoBehaviour
{
    public TextMeshProUGUI BossName;
    Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        BossName = GetComponentInChildren<TextMeshProUGUI>();
        BunnyEventManager.Instance.RegisterEvent("BossOnDamage", this);
        Action<BunnyBrokerMessage<int>> onBossDamage = BunnySetBossCurrHealth;
        BunnyEventManager.Instance.OnEventRaised<int>("BossOnDamage", onBossDamage);
    }
    public void SetBossName(string name) 
    {
        BossName.text = name;
    }

    public void SetUIHealthBarToActive()
    {
        slider.gameObject.SetActive(true);
    }

    public void SetUIHealthBarInactive()
    {
        slider.gameObject.SetActive(false);
    }

    public void SetBossMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetBossCurrentHealth(int curr)
    {
        slider.value = curr;
        if(curr == 0)
        {
            slider.gameObject.SetActive(false);
            SetBossName("");
        }
    }

    public void BunnySetBossCurrHealth(BunnyBrokerMessage<int> msg)
    {
        SetBossCurrentHealth(msg.payload);
    }
}
