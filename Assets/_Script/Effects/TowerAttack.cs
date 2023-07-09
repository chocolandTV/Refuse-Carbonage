using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] GameObject Head;
    public float speed = 25f;
    private float sinTime;
    private Vector3 startPos;
    private bool attacking = false;
    private void Start() {
        startPos = Head.transform.position;
    }
    // private float evaluate(float x)
    // {
    //     return 0.5f *Mathf.Sin(x - Mathf.PI /2f) +0.5f;
    // }
    public void AttackUnit(Vector3 Position)
    {
        // if(!attacking)
        // {
        //     attacking = true;
        //     // WHILE DISTANCE > HEAD TO ZERGLING
        //     // MOVE HEAD BULLET  POSITION  = a + (b-a)*t

        //     while (Head.transform.position != Position)
        //     {
        //           sinTime += Time.deltaTime * speed;
        //           sinTime = Mathf.Clamp(sinTime,0,Mathf.PI);
        //           float t = evaluate(sinTime);
        //           transform.position = Vector3.Lerp(startPos, Position,t);
        //             // PARTICLE EFFECT
        //     }
        //     attacking = false;
        //     Head.transform.position = startPos;
        // }
    }
    

}
