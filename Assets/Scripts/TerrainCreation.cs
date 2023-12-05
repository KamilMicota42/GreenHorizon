using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreation : MonoBehaviour
{
    public LayerMask layers;
    public float maxCreationDistance = 50;
    public GameObject vineTile;
    public GameObject bridgeTile;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            createVine();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            createBridge();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            createBridgeBackwards();
        }
    }

    void createVine()
    {
        //dado um ponto de origem e um ponto de fim, "disparar um raio" para criação do novo terreno
        //o raycast "descobre" se os novos game objects podem ser criados, e se sim, até onde/quantos
        //bool isHit = Physics2D.Raycast(transform.position, Vector3.up, out RaycastHit hitInfo, maxVineDistance, layers);
        //Vector3 something = hitInfo.point;
        //encontrar, para o caminho da vine, o terreno que colide com o caminho do raycast.
        //ao colidir, perceber a layer com que está a colidir (idealmente ground, ignorar o player)


        RaycastHit2D raycastResult = Physics2D.Raycast(transform.position, Vector2.up, maxCreationDistance, layers);
        Vector2 vetorCriacao = raycastResult.point;
        float creationDistance = vetorCriacao.y - transform.position.y;

        //vine creation (iterate the number of times indicated by the raycast result, upwards)
        for (int i = 0; i < creationDistance; i++)
        {
            GameObject.Instantiate(vineTile, new Vector3(transform.position.x, transform.position.y + i), Quaternion.identity);
        }
       
    }

    void createBridge()
    {
        Vector3 raycastStartPoint = new Vector3(transform.position.x + 1, transform.position.y - 2, transform.position.z);
        RaycastHit2D raycastResult = Physics2D.Raycast(raycastStartPoint, Vector2.right, maxCreationDistance, layers);
        Vector2 vetorCriacao = raycastResult.point;
        float creationDistance = vetorCriacao.x - transform.position.x;

        //vine creation (iterate the number of times indicated by the raycast result, to the right)
        for (int i = 1; i < creationDistance; i++)
        {
            GameObject.Instantiate(bridgeTile, new Vector3(transform.position.x + i, transform.position.y - 1), Quaternion.identity);
        }
    }

    void createBridgeBackwards()
    {
        Vector3 raycastStartPoint = new Vector3(transform.position.x - 1, transform.position.y - 2, transform.position.z);
        RaycastHit2D raycastResult = Physics2D.Raycast(raycastStartPoint, Vector2.left, maxCreationDistance, layers);
        Vector2 vetorCriacao = raycastResult.point;
        float creationDistance = Math.Abs(vetorCriacao.x - transform.position.x);
        
        //vine creation (iterate the number of times indicated by the raycast result, to the right)
        for (int i = 1; i < creationDistance; i++)
        {
            GameObject.Instantiate(bridgeTile, new Vector3(transform.position.x - i, transform.position.y - 1), Quaternion.identity);
        }
    }
}
