using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CookingContainer))]
public class RepairableProductionBuilding : MonoBehaviour
{
    [SerializeField] BuildingState startingState;

    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private Sprite pausedSprite;
    [SerializeField] private Sprite producingSprite;

    public enum BuildingState
    {
        BROKEN,
        PAUSED,
        PRODUCING
    }

    public BuildingState currentState { get; private set; }

    private CookingContainer cc;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = brokenSprite;

        cc = GetComponent<CookingContainer>();
        
        Interactable interactable = GetComponent<Interactable>();
        interactable.OnInteractedWith += HandleInteractedWith;

        switch (startingState)
        {
            case BuildingState.BROKEN:
                setBroken();
                break;
            case BuildingState.PAUSED:
                setPaused();
                break;
            case BuildingState.PRODUCING:
                setProducing();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleInteractedWith(Interactable other) {
        setProducing();
        Debug.Log("Building is now producing");
        Debug.Log(cc.enabled + ", ");
    }

    public void setBroken()
    {
        currentState = BuildingState.BROKEN;
        GetComponent<SpriteRenderer>().sprite = brokenSprite;

        cc.Disable();
    }

    public void setPaused()
    {
        currentState = BuildingState.PAUSED;
        GetComponent<SpriteRenderer>().sprite = pausedSprite;

        cc.Disable();
    }
    
    public void setProducing()
    {

        currentState = BuildingState.PRODUCING;
        GetComponent<SpriteRenderer>().sprite = producingSprite;

        cc.Enable();
    }

}
