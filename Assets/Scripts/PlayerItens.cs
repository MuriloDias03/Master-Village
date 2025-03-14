﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItens : MonoBehaviour
{
    [Header("Amounts")]
    private int totalWood;
    public int TotalWood;
   
    public int carrots;
    public float currentWater;
    public int fishes;

    [Header("Limits")]
    public float waterLimit = 50f;
    public float carrotLimit = 10f;
    public float woodLimit = 9f;
    public float fishesLimit = 5f;

    public void WaterLimit(float water)
    {
        if(currentWater < waterLimit)
        {
        currentWater += water;
        }
    }
}
