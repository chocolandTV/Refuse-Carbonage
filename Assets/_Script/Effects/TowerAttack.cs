using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] GameObject Head;
    public float speed = 25f;
    private Vector3 startPos;
    private bool attacking = false;
    private void Start() {
        startPos = Head.transform.position;
    }
    public void AttackUnit(Vector3 Position)
    {
        if(!attacking)
        {
            attacking = true;
            while (Vector3.Distance(Head.transform.position, Position) > 0.3f)
            {
                    Vector3 bulletVelocity = Vector3.right * speed;
                    Head.transform.Translate(bulletVelocity * Time.deltaTime);
                    // PARTICLE EFFECT
            }
            attacking = false;
            Head.transform.position = startPos;
        }
    }
    

}
