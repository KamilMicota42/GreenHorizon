using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneToChase : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            return;
        }
        SceneManager.LoadScene("ChaseScene");
        cm.lastCheckpointPosition = new Vector2(-16.4f, 27.8f);
    }

}
