using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerAttempt1 : MonoBehaviour
{
    public int CurrentStage;
    public AudioSource PlayerMusicPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "TutorialArea" && PlayerVariableSet.PlayerWins == false)
        {
            CurrentStage = 0;
            StartCoroutine(PlayMusic());
        }
        else if (collision.name == "Area1" && PlayerVariableSet.PlayerWins == false)
        {
            StopCoroutine(PlayMusic());
            CurrentStage = 1;
            StartCoroutine(PlayMusic());
        }
        else if (collision.name == "Area2" && PlayerVariableSet.PlayerWins == false)
        {
            StopCoroutine(PlayMusic());
            CurrentStage = 2;
            StartCoroutine(PlayMusic());
        }
        else if (PlayerVariableSet.PlayerWins == true)
        {
            StopCoroutine(PlayMusic());
            CurrentStage = 3;
            StartCoroutine(PlayMusic());
        }
    }
    IEnumerator PlayMusic()
    {
        switch (CurrentStage)
        {
            case 0:
                PlayerMusicPlayer.clip = PlayerVariableSet.VS.GeneralMusic[0];
                PlayerMusicPlayer.Play();
                yield return new WaitForSeconds(PlayerVariableSet.VS.GeneralMusic[0].length);
                StartCoroutine(PlayMusic());
                break;
            case 1:
                int Music1 = 0;
                PlayerMusicPlayer.clip = PlayerVariableSet.VS.Area1Music[Music1];
                PlayerMusicPlayer.Play();
                yield return new WaitForSeconds(PlayerVariableSet.VS.Area1Music[Music1].length);
                Music1++;
                PlayerMusicPlayer.clip = PlayerVariableSet.VS.Area1Music[Music1];
                PlayerMusicPlayer.Play();
                yield return new WaitForSeconds(PlayerVariableSet.VS.Area1Music[Music1].length);
                Music1++;
                PlayerMusicPlayer.clip = PlayerVariableSet.VS.Area1Music[Music1];
                PlayerMusicPlayer.Play();
                yield return new WaitForSeconds(PlayerVariableSet.VS.Area1Music[Music1].length);
                StartCoroutine(PlayMusic());
                break;
            case 2:
                int Music2 = 0;
                PlayerMusicPlayer.clip = PlayerVariableSet.VS.Area2Music[Music2];
                PlayerMusicPlayer.Play();
                yield return new WaitForSeconds(PlayerVariableSet.VS.Area2Music[Music2].length);
                Music2++;
                PlayerMusicPlayer.clip = PlayerVariableSet.VS.Area2Music[Music2];
                PlayerMusicPlayer.Play();
                yield return new WaitForSeconds(PlayerVariableSet.VS.Area2Music[Music2].length);
                Music2++;
                PlayerMusicPlayer.clip = PlayerVariableSet.VS.Area2Music[Music2];
                PlayerMusicPlayer.Play();
                yield return new WaitForSeconds(PlayerVariableSet.VS.Area2Music[Music2].length);
                StartCoroutine(PlayMusic());
                break;
            case 3:
                PlayerMusicPlayer.clip = PlayerVariableSet.VS.GeneralMusic[1];
                PlayerMusicPlayer.Play();
                yield return new WaitForSeconds(PlayerVariableSet.VS.GeneralMusic[1].length);
                StartCoroutine(PlayMusic());
                break;
        }
    }
}
