using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameEvents", menuName = "ScriptableObjects/GameEvents")]

public class GameEvents : ScriptableObject
{
    [SerializeField] public UnityAction mainSceneLoadRequestedEvent = delegate { };
    [SerializeField] public UnityAction menuSceneLoadRequestedEvent = delegate { };
    [SerializeField] public UnityAction applicationStartEvent = delegate { };
    [SerializeField] public UnityAction playerWantsToBuyEvent = delegate { };
    [SerializeField] public UnityAction playerWantsToSellEvent = delegate { };
    [SerializeField] public UnityAction<float> audioVolumeChangedEvent = delegate { };
    [SerializeField] public UnityAction audioFinishedPlayingEvent = delegate { };
    [SerializeField] public UnityAction applicationExitEvent = delegate { };
    //Inventory System Events
    [SerializeField] public UnityAction<ItemSO, int> itemAmountChangedEvent = delegate { };//parameter: Item and amount
    [SerializeField] public UnityAction clearInventoryEvent = delegate { };
    [SerializeField] public UnityAction buyScreenUpdateRequestedEvent = delegate { };
    [SerializeField] public UnityAction buyScreenHideRequestedEvent = delegate { };
    [SerializeField] public UnityAction sellScreenUpdateRequestedEvent = delegate { };
    [SerializeField] public UnityAction sellScreenHideRequestedEvent = delegate { };
    private void OnEnable()
    {
    }

    private void OnDisable()
    {

    }
    public void raiseMainSceneLoadRequestedEvent()
    {
        //Debug.Log("GameEvents: mainSceneLoadRequested event raised");
        mainSceneLoadRequestedEvent.Invoke();
    }
    public void raiseMenuSceneLoadRequestedEvent()
    {
        //Debug.Log("GameEvents: mainMenuSceneLoadRequested event raised");
        menuSceneLoadRequestedEvent.Invoke();
    }
    public void raiseAudioVolumeChangedEvent(Slider slider)
    {
        float newVolume = slider.value;
        //  Debug.Log("GlobalEventsSO: audioVolumeChangedEvent event raised newVolume" + newVolume);
        audioVolumeChangedEvent.Invoke(newVolume);
    }
    public void raiseAudioFinishedPlayingEvent()
    {
        //Debug.Log("GameEvents: audioFinishedPlaying event raised");
        audioFinishedPlayingEvent.Invoke();
    }
    public void raiseApplicationStartEvent()
    {
        //Debug.Log("GameEvents: applicationStart event raised");
        applicationStartEvent.Invoke();
    }
    public void raisePlayerWantsToBuyEvent()
    {
        //Debug.Log("GameEvents: playerWantsToBuy event raised");
        playerWantsToBuyEvent.Invoke();
    }
    public void raisePlayerWantsToSellEvent()
    {
        //Debug.Log("GameEvents: playerWantsToSell event raised");
        playerWantsToSellEvent.Invoke();
    }
    public void raiseClearInventoryEvent()
    {
        //Debug.Log("GameEventsSO: clear inventory event raised");
        clearInventoryEvent.Invoke();
    }

    public void raiseItemAmountChangedEvent(ItemSO item, int amount)
    {
        //Debug.Log("GameEventsSO: item amount changed event raised");
        itemAmountChangedEvent.Invoke(item, amount);
    }

    public void raiseBuyScreenUpdateRequestedEvent()
    {//Debug.Log("GameEventsSO: BuyScreenUpdateRequestedEvent raised");
        buyScreenUpdateRequestedEvent.Invoke();
    }
    public void raiseBuyScreenHideRequestedEvent()
    {//Debug.Log("GameEventsSO: BuyScreenHideRequested event raised");
        buyScreenHideRequestedEvent.Invoke();
    }

    public void raiseSellScreenUpdateRequestedEvent()
    {//Debug.Log("GameEventsSO: SellScreenUpdateRequestedEvent raised");
        sellScreenUpdateRequestedEvent.Invoke();
    }

    public void raiseSellScreenHideRequestedEvent()
    {//Debug.Log("GameEventsSO: SellScreenHideRequested event raised");
        sellScreenHideRequestedEvent.Invoke();
    }

    public void raiseApplicationExitEvent()
    {
        //Debug.Log("GameEvents: applicationExit event raised");
        applicationExitEvent.Invoke();
    }

}
