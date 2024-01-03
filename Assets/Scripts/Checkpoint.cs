using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private CheckpointMaster cm;

    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            return;
        }
        cm.lastCheckpointPosition = transform.position;
    }

}