﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currentHealthBar;
    
    
    private void Start()
    {
        totalhealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
    
    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}