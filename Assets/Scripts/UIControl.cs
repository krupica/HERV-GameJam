using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject menu;
    public GameObject inGame;
    public GameObject help;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI torchText;
    public TextMeshProUGUI upgradeEf;
    public TextMeshProUGUI upgradeMax;
    public TextMeshProUGUI upgradePick;
    public TextMeshProUGUI score;
    public TextMeshProUGUI max;

    public void MainMenu()
    {
        startMenu.SetActive(true);
        menu.SetActive(false);
        inGame.SetActive(false);
        help.SetActive(false);
    }
    
    public void Help()
    {
        startMenu.SetActive(false);
        menu.SetActive(false);
        inGame.SetActive(false);
        help.SetActive(true);
        
    }
    
    public void GameMenu()
    {
        startMenu.SetActive(false);
        menu.SetActive(true);
        inGame.SetActive(true);
        help.SetActive(false);
    }

    public void ShowGame()
    {
        startMenu.SetActive(false);
        menu.SetActive(false);
        inGame.SetActive(true);
        help.SetActive(false);
    }

    public void SetScore(int x)
    {
        score.text = x.ToString();
    }
    
    public void SetMax(int x)
    {
        max.text = x.ToString();
    }
    
    public void setCoinCount(int x)
    {
        coinsText.text = x.ToString();
    }
    
    public void setTorchCount(int x)
    {
        torchText.text = x.ToString();
    }

    public void updateEfCost(int x)
    {
        upgradeEf.text = x.ToString();
    }
    public void UpdateMaxCost(int x)
    {
        upgradeMax.text = x.ToString();
    }
    public void UpdatePickCost(int x)
    {
        upgradePick.text = x.ToString();
    }
    
    
}
