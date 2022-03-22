using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{
    public GameObject nodeA=null;
    public GameObject nodeB=null;
    private new LineRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<LineRenderer>().material.DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void PosInit()
    {
        renderer = gameObject.GetComponent<LineRenderer>();
        // 線の幅
        renderer.startWidth = 0.25f;
        renderer.endWidth = 0.25f;
        // 頂点の数
        renderer.positionCount = 2;
        renderer.material = new Material(Shader.Find("Sprites/Default"));
        //renderer.endColor = Color.red;
        //renderer.startColor = Color.red;

        renderer.SetPosition(0, nodeA.GetComponent<Transform>().position);
        renderer.SetPosition(1, nodeB.GetComponent<Transform>().position);
    }
    // Update is called once per frame
    void Update()
    {

    }

    
}
