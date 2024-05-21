using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRespawn : MonoBehaviour
{
    public bool HasToRespawn;
    private Vector3 checkpoint;
    private GameObject player;

    void Start()
    {
        HasToRespawn = false;
        player = GameObject.FindWithTag("Player");
        checkpoint = player.transform.position;
    }
    void Update()
    {
        Respawn();
    }
    public void Respawn()
    {
        if (HasToRespawn)
        {
            player.transform.position = checkpoint;
            HasToRespawn = false;
        }
    }
    public void NewCheckpoint(Vector2 newCheckpoint)
    {
        checkpoint = newCheckpoint;
    }
}
