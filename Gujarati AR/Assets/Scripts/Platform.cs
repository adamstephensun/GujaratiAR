using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    public Vector3 getSpawnPosition()
    {
        return spawnPoint.position;
    }
}
