using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTextUpdater : ObjectModel
{
    public static ScreenTextUpdater Instance => instance;
    [SerializeField] Text[] moneyTxts;
    [SerializeField] Text[] levelTxts;
    [SerializeField] Text earnedMoneyTxt;
    private static ScreenTextUpdater instance;

    public override void Initialize()
    {
        base.Initialize();
        if (instance == null)
            instance = this;
        UpdateTexts();
    }

    public void UpdateEarnedMoneyTxt(int earned)
    {
        earnedMoneyTxt.DOCounter(0, earned, 1f);
    }

    public void UpdateTexts()
    {
        UpdateMoneyTxts();
        foreach (var item in levelTxts)
        {
            DOTween.Sequence()
                .Append(item.rectTransform.DOScale(1.2f, .2f))
                .OnComplete(() => item.text = "LEVEL " + PlayerDataModel.Data.Level.ToString())
                .Append(item.rectTransform.DOScale(1f, .2f));
        }
    }

    public void UpdateMoneyTxts()
    {
        foreach (var item in moneyTxts)
        {
            DOTween.Sequence()
                .Append(item.rectTransform.DOScale(1.2f, .2f))
                .OnComplete(() => item.text = PlayerDataModel.Data.Money.ToCoinValues() + "$")
                .Append(item.rectTransform.DOScale(1f, .2f));
        }
    }

}
