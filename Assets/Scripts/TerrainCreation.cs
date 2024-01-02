using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreation : MonoBehaviour
{
    public LayerMask layers;
    public GameObject vineTile;
    public GameObject bridgeTile;
    private Vector3 scaleChange, positionChange;
    private float moveHorizontally;
    [SerializeField] private float creationDistance = 10f;
    private bool isBridgeExsist = false; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontally = Input.GetAxisRaw("Horizontal");


        if (Input.GetKeyDown(KeyCode.R))
        {
            createBridge(moveHorizontally);
        }

    }


    void createBridge(float moveHorizontally)
    {
        int layerToSet = 6;

        if(moveHorizontally > 0 && !isBridgeExsist)
        {
            scaleChange = new Vector3(creationDistance, 0f, 0f);
            GameObject bridgeObject = GameObject.Instantiate(bridgeTile, new Vector3(transform.position.x + (creationDistance / 2), transform.position.y - 1), Quaternion.identity);
            bridgeObject.transform.localScale += scaleChange;
            bridgeObject.layer = layerToSet;
            isBridgeExsist = true;

            Destroy(bridgeObject, 3);
            isBridgeExsist = false;
        }
        if (moveHorizontally < 0 && !isBridgeExsist)
        {
            scaleChange = new Vector3(creationDistance, 0f, 0f);
            GameObject bridgeObject = GameObject.Instantiate(bridgeTile, new Vector3(transform.position.x - (creationDistance / 2), transform.position.y - 1), Quaternion.identity);
            bridgeObject.transform.localScale += scaleChange;
            bridgeObject.layer = layerToSet;
            isBridgeExsist = true;

            Destroy(bridgeObject, 3);
            isBridgeExsist = false;
        }
    }
}
