using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : ControllerBaseModel
{
    [SerializeField] private int pricePerLvl;
    [SerializeField] private int mainPrice;
    [SerializeField] private int maxLevel;
    [SerializeField] private UpgradeViewModel upgradeView;
    private int speedPrice => mainPrice + (PlayerDataModel.Data.SpeedLevel * pricePerLvl);
    private int incomePrice => mainPrice + (PlayerDataModel.Data.IncomeLevel * pricePerLvl);

    public override void Initialize()
    {
        base.Initialize();
        UpdateView();
    }

    public void IncomeUpgrade()
    {
        if (PlayerDataModel.Data.Money < incomePrice) return;
        if (PlayerDataModel.Data.IncomeLevel +1 >= maxLevel) return;
        PlayerDataModel.Data.Money -= incomePrice;
        PlayerDataModel.Data.IncomeLevel++;
        PlayerDataModel.Data.Save();
        ScreenTextUpdater.Instance.UpdateMoneyTxts();
        UpdateView();
    }
    public void SpeedUpgrade()
    {
        if (PlayerDataModel.Data.Money < speedPrice) return;
        if (PlayerDataModel.Data.SpeedLevel + 1 >= maxLevel) return;
        PlayerDataModel.Data.Money -= speedPrice;
        PlayerDataModel.Data.SpeedLevel++;
        PlayerDataModel.Data.Save();
        ScreenTextUpdater.Instance.UpdateMoneyTxts();
        UpdateView();
    }

    private void UpdateView()
    {
        string speedPrc = PlayerDataModel.Data.SpeedLevel + 1 >= maxLevel ? "MAX" : speedPrice.ToCoinValues() + "$";
        string incomePrc = PlayerDataModel.Data.IncomeLevel + 1 >= maxLevel ? "MAX" : incomePrice.ToCoinValues() + "$";
        upgradeView.UpdateUpgradeView(speedPrc, incomePrc);
    }

}
