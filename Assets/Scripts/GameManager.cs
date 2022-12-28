using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Light2D light;
    public GameObject coin;
    public GameObject torch;
    public UIControl ui;
    
    public int maxTorch;
    public int secPerTick = 1;
    
    public int coinValue=1;
    public int torchValue=10;

    private int curCoin = 0;
    private int curTorch;
    private int curTimeCnt=0;

    private int timeToAdd = 1;

    [Header("Upgrades")] 
    public int eficiencyIncrease = 1;
    public int pickupIncrease = 10;
    public int maxTorchIncrease = 10;

    public int upgradeEfCost = 1;
    public int upgradePickCost = 1;
    public int upgradeMaxCost = 1;

    private int coinsPickedUp = 0;
    private int maxCoinsPickedUp = 0;
    
    
    void Start()
    {
        Spawner.Init();
        ui.MainMenu();
        Pause();
        UpdateCostText();
        ui.SetMax(0);
    }
    private void FixedUpdate()
    {
        curTimeCnt+=timeToAdd;
        if (curTimeCnt >= secPerTick * 10)
        {
            BurnFuel();
            curTimeCnt = 0;
        }
    }

    public void UpdateCostText()
    {
        ui.updateEfCost(upgradeEfCost);
        ui.UpdateMaxCost(upgradeMaxCost);
        ui.UpdatePickCost(upgradePickCost);
    }

    public void UpgradeEficiency()
    {
        if (curCoin >= upgradeEfCost)
        {
            SpendGold(upgradeEfCost);
            secPerTick+=eficiencyIncrease;
            upgradeEfCost++;
            UpdateCostText();
        }
    }

    public void UpgradeCapacity()
    {
        if (curCoin >= upgradeMaxCost)
        {
            SpendGold(upgradeMaxCost);
            maxTorch +=maxTorchIncrease;
            Refill();
            upgradeMaxCost++;
            UpdateCostText();
        }
    }

    public void UpgradeAmount()
    {
        if (curCoin >= upgradePickCost)
        {
            SpendGold(upgradePickCost);
            torchValue +=pickupIncrease;
            upgradePickCost++;
            UpdateCostText();
        }
    }

    private void SpendGold(int x)
    {
        curCoin -= x;
        ui.setCoinCount(curCoin);
    }
    
    public void PickupCoin()
    {
        timeToAdd++;
        curCoin += coinValue;
        coinsPickedUp += coinValue;
        ui.setCoinCount(coinsPickedUp);
        Spawner.SpawnCoin(coin);
    }
    
    public void PickupTorch()
    {
        Refill(torchValue);
        ui.setTorchCount(curTorch);
        Spawner.SpawnTorch(torch);
    }
    
    private void BurnFuel()
    {
        curTorch -= 1;
        SetLightParams();
        SetTorchCount();
        if (curTorch <= 0)
        {
            GameEnd();
        }
    }

    private void SetTorchCount()
    {
        ui.setTorchCount(curTorch);
    }

    private void SetLightParams()
    {
        float intensity=((float)curTorch / maxTorch);
        light.pointLightOuterRadius = intensity*35;
    }
    
    public void Refill()
    {
        curTorch = maxTorch;
        SetTorchCount();
        SetLightParams();
    }

    public void Refill(int amount)
    {
        curTorch += amount;
        if (curTorch > maxTorch)
        {
            curTorch = maxTorch;
        }
        SetTorchCount();
        SetLightParams();
    }

    public void HelpPage()
    {
        ui.Help();
    }

    public void Menu()
    {
        ui.MainMenu();
    }

    public void Play()
    {
        coinsPickedUp = 0;
        ui.setCoinCount(coinsPickedUp);
        timeToAdd = 1;
        Spawner.SpawnCoin(coin);
        Spawner.SpawnTorch(torch);
        Refill();
        ui.ShowGame();
        Resume();
    }
    
    public void GameEnd()
    {
        player.transform.position = new Vector2(0f, 0f);
        ui.setCoinCount(curCoin);
        ui.SetScore(coinsPickedUp);
        if (coinsPickedUp > maxCoinsPickedUp)
        {
            maxCoinsPickedUp = coinsPickedUp;
            ui.SetMax(coinsPickedUp);
        }
        
        ui.GameMenu();
        Refill();
        Pause();
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
