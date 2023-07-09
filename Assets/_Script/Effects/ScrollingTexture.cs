using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    //3D Version
    public Texture2D[] textures;
    private  MeshRenderer rend;
    public float speed = 50;
    private float _myTime;
    private void Start() {
        rend = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        _myTime += Time.deltaTime * speed;
        int texID = Mathf.RoundToInt(_myTime) % textures.Length;
        rend.material.SetTexture("_MainTex", textures[texID]);
    }
}