using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeViewModel : ObjectModel
{
    [SerializeField] private Text speedMoneyTxt;
    [SerializeField] private Text speedLvlTxt;
    [SerializeField] private Text incomeMoneyTxt;
    [SerializeField] private Text incomeLvlTxt;


    public void UpdateUpgradeView(string speedPrice, string incomePrice)
    {
        speedMoneyTxt.text = speedPrice;
        speedLvlTxt.text = "Level " + (PlayerDataModel.Data.SpeedLevel + 1).ToString();
        incomeMoneyTxt.text = incomePrice;
        incomeLvlTxt.text = "Level " + (PlayerDataModel.Data.IncomeLevel + 1).ToString();
    }

}
