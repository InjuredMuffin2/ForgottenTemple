using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text CoinText;
    public Text HPText;
    public Text PlayerScore;
    public Text DeadText;
    public Text TimeLeft;

    public Image LowHP;
    public Image GameOverBanner;

    public GameObject WinScreen;

    public Animator WinAnim;

    void Update()      
    {
        CoinText.text = PlayerVariableSet.CoinsCollected.ToString() + "/" + PlayerVariableSet.CoinsToWin.ToString();
        HPText.text = "+" + PlayerVariableSet.HealthPoints.ToString();
        int IntScore = (int)PlayerVariableSet.Score;
        PlayerScore.text = "Score:" + IntScore.ToString();
        int Timer = (int)PlayerVariableSet.VS.TimeToFinish;
        if(PlayerVariableSet.PlayerWins == false)
        {
            TimeLeft.text = "Time:" + Timer.ToString();
        }
        switch (PlayerVariableSet.HealthPoints)
        {
            case <= 0:
                if(PlayerVariableSet.PlayerWins == false)
                {
                    LowHP.enabled = true;
                    GameOverBanner.enabled = true;
                    DeadText.enabled = true;
                    DeadText.text = "You Died";
                }
                break;
            case 1:
                if (PlayerVariableSet.PlayerWins == false)
                {
                    LowHP.enabled = true;
                }
                break;
            default:
                if (PlayerVariableSet.PlayerWins == false)
                {
                    DeadText.enabled = false;
                    LowHP.enabled = false;
                    GameOverBanner.enabled = false;
                }
                break;
        }
        if(PlayerVariableSet.CoinsCollected == PlayerVariableSet.CoinsToWin)
        {
            WinScreen.gameObject.SetActive(true);
            WinAnim.Play("WinAnim");
        }
        else
        {
            WinScreen.gameObject.SetActive(false);
        }
    }
}
