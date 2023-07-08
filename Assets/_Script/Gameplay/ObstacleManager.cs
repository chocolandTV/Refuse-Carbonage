using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ObstacleManager : MonoBehaviour
{
    private static NavMeshData _surface;
    [SerializeField] private GameObject obstacle;

    private void Awake()
    {
        _surface = GetComponent<NavMeshData>();

    }
    private void Start()
    {
        for (int x = (int)(transform.position.x + (-5 * transform.localScale.x)); x < (int)(transform.position.x + (5 * transform.localScale.x)); x += 2)
        {
            for (int z = (int)(transform.position.z + (-5 * transform.localScale.z)); z < (int)(transform.position.z + (5 * transform.localScale.z)); z += 2)
            {
                if (Random.Range(0, 100) < 80)
                {
                    SpawnObject(obstacle, new Vector3(x, transform.position.y, z));
                }
            }
        }
    }
    private void SpawnObject(GameObject spawningObject, Vector3 position)
    {
        Instantiate(spawningObject, position, Quaternion.identity);

    }
    public static void UpdateNavMesh()
    {
        //_surface.BuildNavMesh();
    }

}
