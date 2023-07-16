using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Untow : MonoBehaviour
{
    [SerializeField] private GameObject Buidling;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Buidling.transform.parent = null;
            Buidling.transform.position = new Vector3(Buidling.transform.position.x,0,Buidling.transform.position.z);
            Destroy(gameObject, 0.5f);
        }
    }
}
