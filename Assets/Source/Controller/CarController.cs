using DG.Tweening;
using Dreamteck.Splines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : ControllerBaseModel
{
    [SerializeField] private SplineFollower follower;
    [SerializeField] private CarUIModel carUIModel;
    [SerializeField] private CarModel carModel;
    [SerializeField] private DragScreen dragScreen;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dragEffects;
    [SerializeField] private float mainSpeed;
    [SerializeField] private float speedPerLevel;
    [SerializeField] private float dragTime;
    [SerializeField] private float collectableSpeedBoost;
    [SerializeField] private int mainIncomeAmount;
    [SerializeField] private int incomePerLevel;
    [SerializeField] private int dragAdditionalSpeed;
    [SerializeField] private int dragMaxTapCount;
    [SerializeField] private int maxCrushCount;
    
    private float Speed => mainSpeed + (speedPerLevel * (PlayerDataModel.Data.SpeedLevel));
    private int IncomeAmount => mainIncomeAmount + (incomePerLevel * (PlayerDataModel.Data.IncomeLevel));
    
    private float dragTimer;
    private int dragTapCount;
    private int earnedMoney;
    private int crushCount;
    private bool onAnimation;
    private bool finishReached;
    private bool dragStarted;
    
    public void OnLevelStarted()
    {
        follower.enabled = true;
        follower.follow = true;
        follower.followSpeed = Speed;
        earnedMoney = 0;
        dragTimer = dragTime;
        dragTapCount = 0;
        crushCount = 0;
    }
    
    public override void ControllerUpdate(GameStates currentState)
    {
        if (currentState != GameStates.Game || onAnimation) return;
        base.ControllerUpdate(currentState);
        carModel.ModelUpdate();
        if (dragStarted)
        {
            dragTimer -= Time.deltaTime;
            if (dragTimer <= 0) EndDrag();
        }
    }

    private void OnCollidedWithEnemy()
    {
        if (++crushCount >= maxCrushCount)
        {
            EndLevel(false);
        }
        carUIModel.UpdateFillAmount(1f - ((float)crushCount / (float)maxCrushCount));
        follower.follow = false;
        animator.Play("Crush");
        ParticlesController.SetParticle(0, transform.position, VibrationTypes.RigidImpact);
        onAnimation = true;
        GetMoney(-IncomeAmount);
    }

    public void OnCrushAnimEnded()
    {
        follower.follow = true;
        onAnimation = false;
        carModel.OnCrushEnded();
    }

    private void OnCollect()
    {
        if (crushCount > 0) crushCount--;
        carUIModel.UpdateFillAmount(1f - ((float)crushCount / (float)maxCrushCount));
        follower.followSpeed += collectableSpeedBoost;
        GetMoney(IncomeAmount);
    }
    
    public void OnReachedFinishLine()
    {
        follower.enabled = false;
        finishReached = true;
        carModel.OnFinishLineReached();
        dragScreen.gameObject.SetActive(true);
        dragScreen.StartTimer(StartDrag);
    }

    private void StartDrag()
    {
        dragStarted = true;
        carModel.OnDragStarted();
    }

    public void WhenDragPointerDown()
    {
        if (!dragStarted) return;
        dragTapCount++;
        if (dragTapCount >= dragMaxTapCount)
        {
            Invoke("EndDrag", .5f);
        }
        if (dragScreen.isPointInGreen)
        {
            carModel.OnDragAddSpeed(dragAdditionalSpeed);
            GetMoney(IncomeAmount);
            dragEffects.SetActive(false);
            dragEffects.SetActive(true);
        }
        else
        {
            carModel.OnDragAddSpeed(-dragAdditionalSpeed);
        }
    }
   
    private void EndDrag()
    {
        carModel.OnDragEnd();
        EndLevel(true);

    }
   
    private void EndLevel(bool isSuccess)
    {
        if (!isSuccess)
        {
            follower.enabled = false;
            follower.follow = false;
        }
        GameController.IsPlayerWin = isSuccess;
        GameController.ChangeState(isSuccess ? GameStates.Win : GameStates.Lose);
        VibrateController.SetHaptic(isSuccess ? VibrationTypes.Succes : VibrationTypes.Fail);
        ScreenTextUpdater.Instance.UpdateTexts();
        ScreenTextUpdater.Instance.UpdateEarnedMoneyTxt(earnedMoney);
    }

    private void GetMoney(int amount)
    {
        earnedMoney += amount;
        PlayerDataModel.Data.Money += amount;
        
        if (earnedMoney < 0) earnedMoney = 0;
        if (PlayerDataModel.Data.Money < 0) PlayerDataModel.Data.Money = 0;

        PlayerDataModel.Data.Save();
        ScreenTextUpdater.Instance.UpdateMoneyTxts();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameController.CurrentState != GameStates.Game || finishReached) return;
        if (other.CompareTag("EnemyCar"))
        {
            var enemy = other.GetComponent<EnemyModel>();
            enemy.OnCollidedWithPlayer();
            OnCollidedWithEnemy();
        }
        else if (other.CompareTag("Collectable"))
        {
            var collectable = other.GetComponent<CollectableModel>();
            collectable.OnCollected();
            OnCollect();
        }

    }

}
