using System.Collections.Generic;
using UnityEngine;

public class World
{
    private int cout=-5;
    private int deley = 0;
    private serial serial;
    private List<Node> nodes;
    Node[,] tessst;
    Random r;
    // Start is called before the first frame update
    public void Start(main main)
    {
        this.serial = new serial(main);
        nodes = new List<Node>();
        r = new Random();
        tessst = new Node[90,90];
    }

    // Update is called once per frame
    public void Update()
    {
        if(cout==-1) generation();
        //if (cout < -1) test1();
        if (cout++>deley)
        {
            cout = 0;
            foreach (Node node in nodes)
            {
                node.items[0].amount += Random.Range(0, node.items[0].deviation);
                node.items[0].amount -= Random.Range(0, node.items[0].decrease);
                node.items[1].amount += Random.Range(0, node.items[1].deviation);
                node.items[1].amount -= Random.Range(0, node.items[1].decrease);
                node.items[2].amount += Random.Range(0, node.items[2].deviation);
                node.items[2].amount -= Random.Range(0, node.items[2].decrease);


                node.update();

                serial.ChangeDispPople(node.ID,node.pople);
                serial.sendData(2, node.ID, node.items[2].amount);
                serial.sendData(1, node.ID, node.items[1].amount);
                serial.sendData(0, node.ID, node.items[0].amount);


            }
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].pople < 0)
                {
                    this.DeleteNode(nodes[i].ID);
                    i--;
                }
            }

        }
        //Split();
    }

    private void generation()
    {
        int num = 20;
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num; j++)
            {
                AddNode(i * 5, j * 5);
                tessst[i, j] = nodes[nodes.Count - 1];
                
                int tmp = Random.Range(0, 3);
                if (0 == tmp)
                {
                    nodes[nodes.Count - 1].items[0].deviation = 20;
                    nodes[nodes.Count - 1].items[1].decrease = 20;
                }
                else if (1 == tmp)
                {
                    nodes[nodes.Count - 1].items[1].deviation = 20;
                    nodes[nodes.Count - 1].items[2].decrease = 20;
                }
                else if (2 == tmp)
                {
                    nodes[nodes.Count - 1].items[2].deviation = 20;
                    nodes[nodes.Count - 1].items[0].decrease = 20;
                }
            }
        }

        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num; j++)
            {
                if (i + 1 < num) AddPath(tessst[i, j], tessst[i + 1, j]);
                if (j + 1 < num) AddPath(tessst[i, j], tessst[i, j + 1]);
                if (j + 1 < num && i + 1 < num) AddPath(tessst[i, j], tessst[i + 1, j + 1]);
            }
        }
    }
    private void Split()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].items[0].amount > 200)
            {
                nodes[i].items[0].amount -= 90; 
                nodes[i].pople -= 10;

                AddNode(nodes[i].PosX+ Random.Range(-0, 4), nodes[i].PosY + Random.Range(-0, 4));
                nodes[nodes.Count - 1].pople = 20;
                foreach (Node pathnode in nodes[i].nodes)
                {
                    AddPath(nodes[nodes.Count - 1], pathnode);
                }
            }
        }
    }
    public void AddNode(int x, int y)
    {

        nodes.Add(new Node(this));
        nodes[nodes.Count - 1].PosX = x;
        nodes[nodes.Count - 1].PosY = y;

        serial.addnode(nodes[nodes.Count - 1].ID,x,y);

        nodes[nodes.Count - 1].pople = 100;//Random.Range(1, 1000);
        nodes[nodes.Count - 1].items[0].amount = Random.Range(1, 100);

    }
    public void AddPath(Node A,Node B)
    {
        A.addLinkingNode(ref B);
        B.addLinkingNode(ref A);

        serial.AddPath(A.ID, B.ID);

    }

    public void DeleteNode(string ID)
    {

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].ID == ID)
            {
                nodes.Remove(nodes[i]);
                i--;
            }
        }
        serial.DestroyNode(ID);
    }
}
