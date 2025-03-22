using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    public Canvas indicatorCanva;
    public Bot botPrefabs;
    public List<Transform> characterTranform = new List<Transform>();
    public Player player;
    private int playerInScene = 6;
    public TargetIndicator indicator;
    public TextMeshProUGUI aliveText;
    private int totalPlayerInOneGame;
    public List<Bot> bots = new List<Bot>();
    public int gold;
    public InitSkin skin;
    //public NavMeshSurface gameNavMeshSurface;
    //public Bullet bulletPrefabs;


    private void Start()
    {
        OnInit();
        SetPlayerIndicater();
    }

    public void OnInit()
    {
        totalPlayerInOneGame = 50;
        if (playerInScene > characterTranform.Count - 1)
        {
            playerInScene = characterTranform.Count - 1;
        }
        aliveText.text = "Alive: " + totalPlayerInOneGame;
        SetBotInGame();
        InitGold();
    }

    public void ResponsNewBot()
    {
        SetTextAlive();
        if (totalPlayerInOneGame > playerInScene)
        {
            Bot botn = MyPoolManager.InstancePool.Get(botPrefabs);
            botn.gameObject.SetActive(true);
            botn.tag = "Bot";
            botn.isDead = false;
            Vector3 spawnPos = characterTranform[Random.Range(1, characterTranform.Count)].position;
            botn.transform.position = spawnPos;
            botn.agent.enabled = false;
            botn.agent.enabled = true;
            botn.agent.ResetPath();
            botn.OnInit();
            TargetIndicator botIndicater = MyPoolManager.InstancePool.Get(indicator, indicatorCanva.transform);
            botIndicater.character = botn;
            botn.indicator = botIndicater;
            UnityEngine.Color color = Random.ColorHSV();
            string name = Constant.botName[Random.Range(0, Constant.botName.Length)] + Random.Range(0, 1000);
            botn.indicator.InitTarget(color, name, 1);
            botn.indicator.gameObject.SetActive(true);
            bots.Add(botn);
        }
    }


    public void SetTextAlive()
    {
        totalPlayerInOneGame--;
        aliveText.text = "Alive: " + totalPlayerInOneGame;
        if (totalPlayerInOneGame <= 1)
        {
            Victory();
        }
    }

    public void SetPlayerIndicater()
    {
        TargetIndicator playerIndicater = Instantiate(indicator, indicatorCanva.transform);
        player.indicator = playerIndicater;
        playerIndicater.character = player;
        playerIndicater.gameObject.SetActive(true);
    }


    public void SetBotInGame()
    {
        
        player.transform.position = characterTranform[characterTranform.Count - 1].position;
        player.OnInit();
        MyPoolManager.InstancePool.ClearPool(botPrefabs);
        MyPoolManager.InstancePool.ClearPool(indicator);
        for (int i = 0; i < playerInScene; i++)
        {
            Bot botn = MyPoolManager.InstancePool.Get(botPrefabs);
            botn.gameObject.SetActive(true);
            botn.transform.position = characterTranform[i].position;
            TargetIndicator botIndicater = MyPoolManager.InstancePool.Get(indicator, indicatorCanva.transform);
            botIndicater.gameObject.SetActive(true);
            botIndicater.character = botn;
            botn.indicator = botIndicater;
            bots.Add(botn);
            UnityEngine.Color color = Random.ColorHSV();
            string name = Constant.botName[Random.Range(0, Constant.botName.Length)] + Random.Range(0, 1000);
            botn.indicator.InitTarget(color, name, 1);
            botn.indicator.gameObject.SetActive(true);
        }
    }



    public void InitPlayerItems()
    {
        player.skin.PlayerEquipItems();
        player.UpdateWeapons();
    }
    private void InitGold()
    {
        if (!PlayerPrefs.HasKey("gold"))
        {
            gold = 0;
            PlayerPrefs.SetInt("gold", 0);
        }
        else
        {
            gold = PlayerPrefs.GetInt("gold");
        }
    }

    public void GainGold(int num)
    {
        gold += num;
        PlayerPrefs.SetInt("gold", gold);
        UIManager.Instance.InitGold();
    }

    public void ReduceGold(int num)
    {
        gold -= num;
        PlayerPrefs.SetInt("gold", gold);
        UIManager.Instance.InitGold();
    }

    public void ReplayGame()
    {
        DeleteAllBots();
        player.OnDespawn();
        OnInit();
        indicatorCanva.gameObject.SetActive(false);
    }

    public void DeleteAllBots()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            Destroy(bots[i].indicator.gameObject);
            Destroy(bots[i].gameObject);
        }
        bots.Clear();
    }


    public void StartGame()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].OnInit();
        }
    }

    public void Victory()
    {
        JoystickControl.direct = Vector3.zero;
        JoystickControl.Instance.gameObject.SetActive(false);
        UIManager.Instance.RewardCoin(100, 1);
    }


    public void GameOver()
    {
        JoystickControl.direct = Vector3.zero;
        JoystickControl.Instance.gameObject.SetActive(false);
        UIManager.Instance.RewardCoin(player.level, totalPlayerInOneGame);
    }
}
