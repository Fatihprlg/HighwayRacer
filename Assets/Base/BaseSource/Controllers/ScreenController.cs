using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : ControllerBaseModel
{
    public ScreenElement ActiveScreen;
    [SerializeField] private List<ScreenElement> screens;
    [SerializeField] private bool autoChangeScreens;
    private static ScreenController instance;

    public override void Initialize()
    {
        base.Initialize();
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;

        foreach (var item in screens)
        {
            item.Initialize();
            item.SetActiveGameObject(false);
        }

        ActiveScreen.Show();

        if (autoChangeScreens)
            GameController.Instance.OnStateChange.AddListener(onStateChange);
    }

    private void onStateChange(GameStates currentState)
    {
        switch (currentState)
        {
            case GameStates.Loading:
                ChangeScreen(false, 0);
                break;
            case GameStates.Main:
                ChangeScreen(true, 1);
                break;
            case GameStates.Game:
                ChangeScreen(true, 2);
                break;
            case GameStates.Win:
                ChangeScreen(true, 3);
                break;
            case GameStates.Lose:
                ChangeScreen(true, 4);
                break;
            default:
                break;
        }
    }

    public static void ChangeScreen(bool showAfterHide, int index)
    {
        ScreenElement nextScreen = GetScreen<ScreenElement>(index);

        if (showAfterHide)
        {
            if (instance.ActiveScreen != null)
            {
                instance.ActiveScreen.Hide(nextScreen.Show);
                instance.ActiveScreen = nextScreen;
            }
            else
            {
                instance.ActiveScreen = nextScreen;
                instance.ActiveScreen.Show();
            }
        }
        else
        {
            instance.ActiveScreen.Hide();
            instance.ActiveScreen = nextScreen;
            instance.ActiveScreen.Show();
        }

    }

    public static T GetScreen<T>()
    {
        return (T)(object)instance.screens.Find(x => x.GetType() == typeof(T));
    }

    public static T GetScreen<T>(int index)
    {
        return (T)(object)instance.screens[index];
    }
}
