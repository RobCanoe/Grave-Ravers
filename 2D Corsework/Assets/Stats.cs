using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Stats 
{
    [SerializeField]
    private BarScript bar;
    [SerializeField]
    private float maxVal;

   
    [SerializeField]
    private float currentVal;
    public float CurrentVal { 
        get => currentVal; 
        set 
        {
            this.currentVal = value;
            bar.Value = currentVal;
        }
}

    public float MaxVal 
    { get => maxVal;
        set
        {
            
            this.maxVal = value;
            bar.MaxValue = maxVal;
        }
    }
    public void Initalize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
