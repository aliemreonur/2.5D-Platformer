using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if(player == null)
        {
            Debug.LogError("Coin could not find the player");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.CoinCollected();
            Destroy(this.gameObject);
        }
    }
}
