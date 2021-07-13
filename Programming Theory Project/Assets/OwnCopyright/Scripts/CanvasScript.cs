using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CanvasScript : UIBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private List<GameObject> displayOnWin;

    protected override void OnEnable()
    {
        gameEvents.gameWonEvent += OnWin;
    }
    protected override void OnDisable()
    {
        gameEvents.gameWonEvent -= OnWin;
    }
    public void OnWin()
    {
        foreach (var item in displayOnWin)
        {
            item.SetActive(true);
        }
    }
    protected override void OnRectTransformDimensionsChange()
    {
        gameEvents.raiseWindowSizeChangedEvent();
    }
}

