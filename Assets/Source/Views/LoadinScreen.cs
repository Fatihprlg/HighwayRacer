using UnityEngine;

public class LoadinScreen : ScreenElement
{
    public override void Show()
    {
        base.Show();

        Invoke(nameof(onFakeLoadComplete), Random.Range(1, 2));
    }

    private void onFakeLoadComplete()
    {
        GameController.ChangeState(GameStates.Main);
    }
}
