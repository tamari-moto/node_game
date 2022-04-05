using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    private List<GameObject> nodes;/*.GetComponent<PlayerController>()*/
    public Dictionary<string,PlayerController> PC_nodes;

    List<GameObject> paths;/*.path*/
    GameObject prefabOfNode;
    GameObject prefabOfPath;

    public GameObject Canvas;

    public Tilemap tile;
    public TileBase tile1;

    private Text Select_items;//所有
    private Text Select_Products;//生産
    private Text Select_cost;//消費
    private Text Select_pople;//人口    
    private Material mat;
    private tileControll evmt;

    private string select_ID;
    private static World world;

    public void selectNode(string select_ID)
    {
        this.select_ID = select_ID;
        Debug.Log("select:"+this.select_ID);
        ;
        Select_items.text = ""+ this.getNodeByID(select_ID).items;
        Select_Products.text = "" + this.getNodeByID(select_ID).Products;
        Select_cost.text = "" + this.getNodeByID(select_ID).cost;
        Select_pople.text = "" + this.getNodeByID(select_ID).pople;

        mat.SetFloat("_Fillpercentage", (float)(this.getNodeByID(select_ID).items*0.01));
    }

    public GameObject getNodeByID_GameObject(string ID)
    {

        foreach (GameObject ss in nodes)
        {
            if (ss.GetComponent<PlayerController>().ID == ID)
            {
                return ss;
            }
        }
        return null;
    }
    public PlayerController getNodeByID(string ID)
    {
        if (PC_nodes[ID] != null)
        {
            return PC_nodes[ID];
        }
        return null;
        
    }

    //サーバ側への操作のため他クラスからの操作禁止
    public void DestroyNode(GameObject destroyNode)
    {
        for (int i = 0; i < paths.Count; i++)
        {
            if (paths[i].GetComponent<path>().nodeA == destroyNode ||
                paths[i].GetComponent<path>().nodeB == destroyNode)
            {
                Destroy(paths[i]);
                paths.Remove(paths[i]);
                i--;
            }
        }
        Destroy(destroyNode);
        world.DeleteNode(destroyNode.GetComponent<PlayerController>().ID);
        nodes.Remove(destroyNode);
    }
    public void DestroyNode_forServer(GameObject destroyNode)
    {
        for (int i = 0; i < paths.Count; i++)
        {
            if (paths[i].GetComponent<path>().nodeA == destroyNode ||
                paths[i].GetComponent<path>().nodeB == destroyNode)
            {
                Destroy(paths[i]);
                paths.Remove(paths[i]);
                i--;
            }
        }
        Destroy(destroyNode);
        nodes.Remove(destroyNode);
    }

    public GameObject addme(string ID,int x,int y)
    {
        Vector3 s = new Vector3(0, 0, 0.0f);
        nodes.Add(Instantiate(prefabOfNode, s, Quaternion.Euler(0, 0, 0)));
        PlayerController pc = nodes[nodes.Count - 1].GetComponent<PlayerController>();
        pc.ChangeID(ID);
        PC_nodes.Add(ID, nodes[nodes.Count - 1].GetComponent<PlayerController>());
        pc.main = this;
        Vector2 ve = nodes[nodes.Count - 1].transform.position;
        ve.x = x;
        ve.y = y;
        nodes[nodes.Count - 1].transform.position = ve;
        return nodes[nodes.Count-1];
    }

    public void addPath(GameObject afterNode,GameObject beforeNode)
    {
        Vector3 w = new Vector3(0, 0, 0.0f);
        paths.Add(Instantiate(prefabOfPath, w, Quaternion.Euler(0, 0, 0)));

        paths[paths.Count - 1].GetComponent<path>().nodeA = afterNode;
        paths[paths.Count - 1].GetComponent<path>().nodeB = beforeNode;

        paths[paths.Count - 1].GetComponent<path>().PosInit();
    }


    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        PC_nodes = new Dictionary<string, PlayerController>();
        nodes = new List<GameObject>();
        paths = new List<GameObject>();
        // プレハブを取得
        prefabOfNode = Resources.Load("prefabs/player") as GameObject;
        prefabOfPath = Resources.Load("prefabs/path") as GameObject;


        // プレハブからインスタンスを生成
        //Vector3 s = new Vector3(0.0f, 0.0f, 0.0f);
        //nodes.Add(Instantiate(prefabOfNode, s, Quaternion.EulerAngles(0, 0, 0)));
        //afternode = nodes[nodes.Count - 1];

        world = new World();
        Debug.Log("test:" + world);
        world.Start(this);

        Select_items = this.Canvas.transform.GetChild(0).GetComponent<Text>();
        Select_Products = this.Canvas.transform.GetChild(3).GetComponent<Text>();
        Select_cost = this.Canvas.transform.GetChild(2).GetComponent<Text>();
        Select_pople = this.Canvas.transform.GetChild(1).GetComponent<Text>();
        Select_pople = this.Canvas.transform.GetChild(1).GetComponent<Text>();
        mat =this.Canvas.transform.transform.Find("UI_pal").GetComponent<Renderer>().material;

        evmt = new tileControll();
        evmt.tile = this.tile;
        evmt.tile1 = this.tile1;
        evmt.init();

        setnodeup();
    }


    // Update is called once per frame
    void Update()
    {

    }

    void setnodeup()
    {
        //Invoke("addme", 0);
        world.Update();
        evmt.Update();

        Invoke("setnodeup", 0.1f);
    }
}
