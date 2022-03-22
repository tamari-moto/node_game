using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public main main;

    public GameObject node;
    public String ID;

    private Vector3 screenPoint;
    private Vector3 offset;
    //private Text pople;
    //private Text product;
    //private Text cust;
    //private Text item;

    public int items;//所有
    public int Products;//生産
    public int cost;//消費
    public int pople;//人口

    SpriteRenderer SR;

    void Start()
    {
        SR = this.GetComponent<SpriteRenderer>();
        SR.color = Color.white;
        this.node=transform.root.gameObject;
        //pople = this.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        //product = this.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        //cust = this.transform.GetChild(0).transform.GetChild(2).GetComponent<Text>();
        //item = this.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>();
        //item.material = new Material(Shader.Find("Sprites/Default"));
        //product.color = Color.green;
        //cust.color = Color.red;
    }

    public static explicit operator PlayerController(GameObject v)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

        //pairNode_pos.Set(pairNod.transform.position.x, pairNod.transform.position.y, pairNod.transform.position.z);


    }

    void OnMouseDown()
    {
        main.selectNode(this.ID);
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        //main.DestroyNode(main.getNodeByID_GameObject(this.ID));
     }
    // 追加
    void OnMouseDrag()
    {
        //Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        //Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        //currentPosition.z = 0;
        //transform.position = currentPosition;
    }
    public void ChangeDispPople(int pople)
    {
        this.pople = pople;
        //this.pople.text = "" + Pople;
    }
    public void ChangeDispPro(int product)
    {
        //this.product.text = "" + product;
        this.Products = product;
    }
    public void ChangeDispCust(int cost)
    {
        this.cost = cost;
        //this.cust.text = "" + cust;
    }
    public void ChangeDispitem0(int po,int item)
    {
        this.items = item;
        //this.item.text = "" + item;
        Color color = this.SR.color;
        color[po] = (float)((int)items * 0.01);
        this.SR.DOColor(color, 0.1f);
    }

    public void ChangeID(String ID)
    {
        this.ID = ID;
    }

}