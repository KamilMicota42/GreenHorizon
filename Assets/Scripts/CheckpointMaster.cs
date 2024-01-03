using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMaster : MonoBehaviour
{

    private static CheckpointMaster instance;
    public Vector2 lastCheckpointPosition;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
