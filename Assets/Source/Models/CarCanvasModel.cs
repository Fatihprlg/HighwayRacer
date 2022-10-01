using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarCanvasModel : ObjectModel
{
    [SerializeField] private Image fillBar;

    public void UpdateFillAmount(float amount)
    {
        fillBar.fillAmount = amount;
        fillBar.color = new Color(1 - amount, amount, 0, 1);
    }
    
}
