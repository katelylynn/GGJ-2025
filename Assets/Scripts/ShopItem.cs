using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* BuyFireRate
* BuyDash
* BuyMaxHealth
* BuyMultishot
* BuyBiggerBullet
*/

public class ShopItem : MonoBehaviour
{

    [SerializeField] GameManager.PowerUpType type;
    [SerializeField] int cost;
    private GameManager gameManager;

    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteractedWith += HandleInteractedWith;
        gameManager = GameManager.Instance;
    }

    public void HandleInteractedWith(Interactable other)
    {
        Debug.Log("ShopItem " + name + " was interacted with by " + other.name);
        int curOffenseTokens = gameManager.inventory[0];

        if (curOffenseTokens < cost) {
            Debug.Log("Not enough tokens to buy " + name + ". Need " + cost + " but have " + curOffenseTokens);
            return;
        }

        gameManager.inventory[0] -= cost;
        gameManager.AddPowerUp(type);

        Debug.Log(name + " bought a " + type + " for " + cost + " tokens. Now have " + gameManager.inventory[0] + " tokens left and " + gameManager.ownedPowerUps[type] + " " + type + " powerups.");
    }
}
