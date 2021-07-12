using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


    public class CanvasScript : UIBehaviour
    {
        [SerializeField] private GameEvents gameEvents;
        protected override void OnRectTransformDimensionsChange()
        {
        gameEvents.raiseWindowSizeChangedEvent();
        }
    }

