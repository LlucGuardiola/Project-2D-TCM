using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererController : MonoBehaviour
{
    private TrailRenderer tr;
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        tr = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if (player.GetComponent<Dash>().IsDashing) { tr.emitting = true; }
        else {  tr.emitting = false; }
    }
}
