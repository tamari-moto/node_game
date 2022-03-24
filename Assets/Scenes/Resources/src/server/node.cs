using System.Collections;
using System.Collections.Generic;
using System;



public class Node
{
    public String ID;

    public List<Node> nodes;//   連結node
    public int PosX;
    public int PosY;
    public int pople;//人口

    const int MAXitem = 5;
    public Item[] items = new Item[MAXitem];//所有


    private World world;

    int[] ransu = new int[MAXitem];
    public Node(World world)
    {
        for (int i = 0; i < MAXitem; i++)
        {
            this.items[i] = new Item();
        }

        for(int i = 0; i < MAXitem; i++)
        {
            this.items[i].deviation = 0;
            this.items[i].decrease = 0;
        }

        this.ID = Guid.NewGuid().ToString("N");
        this.world = world;
        nodes = new List<Node>();
    }

    public void update()
    {
        createItem();
        transaction();

    }

    private void createItem()
    {
        foreach (Item item in items)
        {
            if (item.amount < 0) death();
            //item.amount += item.deviation;
        }
    }
    private void transaction()
    {
        foreach (Node node in nodes)
        {
            for(int i=0;i< 3;i++)
            {
                if (node.items[i].amount+10 < this.items[i].amount && node.pople > 0)
                {
                    node.items[i].amount += 10;
                    this.items[i].amount -= 10;
                }
            }
        }
    }



    public void addLinkingNode(ref Node A)
    {
        nodes.Add(A);
    }

    /*
    bool senditem( node A,int kind,int amount)
    {
        if (items - amount < 0) return false;//送れない場合false
        
        A.arrivalitem(kind, amount);
        items -= amount;
        return true;
    }

    public void arrivalitem(int kind, int amount)
    {
        items += amount;
    }*/

    void death()
    {
        foreach (Item item in items)
        {
            if (item.amount < 0)
            {
                pople += item.amount;
            }
        }
        
        if (this.pople < 0)
        {
            this.pople = -1000;
        }

    }

}
