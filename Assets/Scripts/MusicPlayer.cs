using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public Animator TimeUpScreen;
    public Animator PlayerAnimator;

    public AudioSource PlayerMusicPlayer;
    public int CurrentStage = 0;
    public bool IsActive = false;
    public string WinCollision;
    private void Update()
    {
        if (PlayerVariableSet.VS.TimeToFinish <= 10)
        {
            PlayerAnimator.SetBool("TimeEnding", true);
        }

        PlayerAnimator.SetFloat("MusicChangeTime", PlayerVariableSet.VS.TimeToFinish);
        TimeUpScreen.SetInteger("Time", (int)PlayerVariableSet.VS.TimeToFinish);
        if (PlayerVariableSet.VS.TimeToFinish <= 0 && PlayerVariableSet.CoinsCollected == PlayerVariableSet.CoinsToWin && WinCollision != "WinCollider")
        {
            // Play the victory music when the player has collected all the coins and won the game
            PlayerMusicPlayer.clip = PlayerVariableSet.VS.GeneralMusic[3];
            if (IsActive == false)
            {
                PlayerMusicPlayer.Play();
                IsActive = true;
            }
            TimeUpScreen.SetBool("TimeIsUp", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "TutorialArea" && PlayerVariableSet.VS.TimeToFinish > 0)
        {
            CurrentStage = 0;
            PlayerMusicPlayer.clip = PlayerVariableSet.VS.GeneralMusic[0];
            PlayerMusicPlayer.Play();
        }
        else if (collision.name == "Area1" && PlayerVariableSet.VS.TimeToFinish > 0)
        {
            CurrentStage = 1;
            PlayerMusicPlayer.clip = PlayerVariableSet.VS.Area1Music[0];
            PlayerMusicPlayer.Play();
        }
        else if (collision.name == "Area2" && PlayerVariableSet.CoinsCollected < PlayerVariableSet.CoinsToWin)
        {
            CurrentStage = 2;
            PlayerMusicPlayer.clip = PlayerVariableSet.VS.Area2Music[1];
            PlayerMusicPlayer.Play();
        }
        else if (collision.name == "WinCollider" && PlayerVariableSet.CoinsCollected == PlayerVariableSet.CoinsToWin -1)
        {
            CurrentStage = 3;
            PlayerMusicPlayer.clip = PlayerVariableSet.VS.GeneralMusic[1];
            PlayerMusicPlayer.Play();
        }
    }
}

