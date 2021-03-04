using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCenter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private short chunkSize;

    private void Update()
    {
        transform.position = player.position - new Vector3(chunkSize / 2, 0, chunkSize / 2);
    }
}
