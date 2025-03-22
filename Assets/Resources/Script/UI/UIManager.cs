using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public GameObject mainMenu;
    public GameObject inGame;
    public GameObject settingPanel;
    public GameObject losePanel;
    public Button btnStart;
    public Button btnReplay;
    public Button btnSetting;
    public Button btnClose;
    public Button btnClaim;
    //public Button btnCloseReward;
    public Button btnShop;
    

    public TextMeshProUGUI coinReward;
    public TextMeshProUGUI rankReward;
    public TextMeshProUGUI coinMainMenu;
    public TextMeshProUGUI loadPlayer;
    public int numberPlayerLoading=0;
    public JoystickControl joystick;
    public GameObject indicatorPanel;
    public GameObject rewardPanel;
    public GameObject shopPanel;
    public GameObject loadPanel;
    
    public int coin = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        InitGameState(1);
        btnStart.onClick.AddListener(() => { 
            InitGameState(2); 
        }) ;
        btnSetting.onClick.AddListener(() => {settingPanel.SetActive(true); });
        btnReplay.onClick.AddListener(()=>MainMenuClick());
        btnClose.onClick.AddListener(()=> {settingPanel.SetActive(false); });
        //btnCloseReward.onClick.AddListener(() => { rewardPanel.SetActive(false); });
        btnClaim.onClick.AddListener(() => {MainMenuClick(); });
        btnShop.onClick.AddListener(() => { OpenShop();});
        InitGold();
    }


    public void OpenShop()
    {
        inGame.SetActive(false);
        mainMenu.SetActive(false);
        shopPanel.SetActive(true);
        InitGameState(3);
    }

    public void MainMenuClick()
    {
        rewardPanel.SetActive(false);
        settingPanel.SetActive(false);
        inGame.SetActive(false);
        InitGameState(1);
        GameController.Instance.ReplayGame();
        InitGold();
        GameController.Instance.GainGold(coin);
    }

    public void InitGold()
    {
        coinMainMenu.text = PlayerPrefs.GetInt("gold")+"";
    }

    public void InitGameState(int a)
    {
        mainMenu.SetActive(a==1);
        loadPanel.SetActive(a==2);
        inGame.SetActive(a==2);
        joystick.gameObject.SetActive(a==2);
        if (a==2)
        {
            //LoadingManager.Instance.getMap(); 
            StartCoroutine(loadingGame());
        }else if (a==1)
        {
            indicatorPanel.gameObject.SetActive(false);
            SoundManager.Instance.PlayerBGMusic(SoundList.BGMainMenu);
            numberPlayerLoading = 0;
        }
        CameraFollower.Instance.ChangeState(a);
    }
     
    IEnumerator loadingGame()
    {
        if (numberPlayerLoading < 30)
        {
            numberPlayerLoading++;
            loadPlayer.text = numberPlayerLoading.ToString();
            yield return new WaitForSeconds(Random.Range(0.05f,0.3f));
            StartCoroutine(loadingGame());
        }
        else
        {
            inGame.SetActive(true);
            GameController.Instance.indicatorCanva.gameObject.SetActive(true);
            GameController.Instance.StartGame();
            indicatorPanel.SetActive(true);
            SoundManager.Instance.PlayerBGMusic(SoundList.BGInGame);
            loadPanel.SetActive(false);
        }
        
    }

    public void RewardCoin(int coinReward, int rank)
    {
        this.coin = coinReward;
        rewardPanel.SetActive(true);
        this.coinReward.text=coinReward.ToString();
        rankReward.text="#"+rank;
    }

}
