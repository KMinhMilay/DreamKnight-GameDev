using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    
    [Header("Move")] 
    [SerializeField] private float moveSpd = 5;
    
    //Target
    private PlayerMovement player;
    private float colliderRange = 1f;

    public void Init(PlayerMovement playerMovement)
    {
        player = playerMovement;
    }
    private void Update()
    {
        ChasePlayer();
    }
    
    private bool IsNearPlayer()
    {
        return Vector3.SqrMagnitude(player.transform.position - transform.position) <= colliderRange;
    }

    private void ChasePlayer()
    {
        //Check neu player o ngay canh boss => dung yen
        //Neu khong thi duoi theo player
        bool nearPlayer = IsNearPlayer();
        if (nearPlayer)
        {
           
        }
        else
        {
            var diff = player.transform.position - transform.position;
            Vector3 dir = diff.normalized + 0.5f * Vector3.up;
            transform.localScale = new Vector3(dir.x > 0 ? -1: 1, 1 , 1);
            transform.Translate(dir * moveSpd * Time.deltaTime);
        }
    }

}
