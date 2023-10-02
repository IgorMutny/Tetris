using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 100;

        //GetComponent<Timer>().Init();
        //GetComponent<Statistics>().Init();
        GetComponent<Container>().Init();
        GetComponent<FigureController>().Init();
        GetComponent<NextFigurePresenter>().Init();
        GetComponent<FigureSpawner>().Init();
        GetComponent<FixedCells>().Init();
        GetComponent<KeyboardController>().Init();
        GetComponent<UIController>().Init();
    }

    public void NewGameHandle()
    {
        GetComponent<Timer>().NewGameHandle();
        GetComponent<Statistics>().NewGameHandle();
        GetComponent<FixedCells>().NewGameHandle();
        GetComponent<FigureController>().NewGameHandle();
        GetComponent<FigureSpawner>().NewGameHandle();
        GetComponent<Container>().NewGameHandle();
        GetComponent<KeyboardController>().NewGameHandle();
        GetComponent<UIController>().NewGameHandle();
    }
}
