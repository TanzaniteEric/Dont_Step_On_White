using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplatterSystem.TopDown;

public class PlayerStateController : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    public void PlayerHitEnabled()
    {
        player.PlayerCanBeHit = true;
    }

    public void PlayerHitDisabled()
    {
        player.PlayerCanBeHit = false;
    }
}
