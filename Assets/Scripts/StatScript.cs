using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatScript {//no methods, so no need to be a MonoBehaviour. But since not a Monobehaviour, need to be made Serializable
    //This class is also not dragged into a gameobject like other scripts, instead it instantiates itself within another script
    [SerializeField]
    private BarScript bar;

    [SerializeField]
    private float maxVal;

    [SerializeField]
    private float currentVal;//current value of this stat

    public float CurrentVal {
        get {
            return currentVal;
        }
        set {
            this.currentVal = Mathf.Clamp(value, 0, MaxVal);
            bar.Value = currentVal;//calls the Value property of BarScript
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            bar.MaxValue = value;
            maxVal = value;
        }
    }
    /* The initial setting of the MaxVal and CurrentVal allows the StatScript.MaxVal to trigger the BarScript.MaxValue, 
    which sets the BarScript.Value, which updates the fillAmount properly for BarScript.HandleBar. 
    */
    public void Initialize() {        
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
