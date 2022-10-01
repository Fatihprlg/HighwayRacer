using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : ControllerBaseModel
{
    public static GameController Instance;
    public static bool IsPlayerWin;
    public GameStateEventModel OnStateChange;
    public static GameStates CurrentState
    {
        get
        {
            return Instance.currentState;
        }
    }

    [SerializeField] private ControllerBaseModel[] controllers;
    private GameStates currentState;

    public override void Initialize()
    {
        base.Initialize();

        if (Instance == null)
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = -1;
        }
        else
        {
            Destroy(Instance);
        }
        foreach (var item in controllers)
        {
            item.Initialize();
        }
        Instance = this;
    }

    public void Update()
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].ControllerUpdate(currentState);
        }
    }

    public static void ChangeState(GameStates state)
    {
        Instance.currentState = state;
        Instance.OnStateChange?.Invoke(state);
        for (int i = 0; i < Instance.controllers.Length; i++)
        {
            Instance.controllers[i].OnStateChange(state);
        }

        switch (state)
        {
            case GameStates.Main:
                break;
            case GameStates.Game:
                break;
            case GameStates.Win:
                break;
            case GameStates.Lose:
                break;
            default:
                break;
        }
    }

    public static void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


