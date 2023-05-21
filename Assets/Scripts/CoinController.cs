using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int CoinValue;
    public int RandomCoinSound;
    private void Start()
    {
        RandomCoinSound = UnityEngine.Random.Range(2, 6);
        CoinValue = 1;
        PlayerVariableSet.CoinsToWin = PlayerVariableSet.CoinsToWin + CoinValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerVariableSet.CoinsCollected = PlayerVariableSet.CoinsCollected + CoinValue;
            PlayerVariableSet.VS.PlayerAudioSource2.clip = PlayerVariableSet.VS.AudioClips[RandomCoinSound];
            PlayerVariableSet.VS.PlayerAudioSource2.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            PlayerVariableSet.VS.PlayerAudioSource2.Play();

            PlayerVariableSet.Score = PlayerVariableSet.Score + 10 * CoinValue;
            GameObject.Destroy(this.gameObject);
        }
    }
}