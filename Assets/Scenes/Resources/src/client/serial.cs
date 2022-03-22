using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class serial
{ 

    main main;
    // Start is called before the first frame update
    public serial(main msian)
    {
        this.main = msian;
    }
    public void ChangeDispPople(String ID,int people)
    {
        if (main.getNodeByID(ID) == null) return;
        main.PC_nodes[ID].ChangeDispPople(people);
        //main.getNodeByID(ID).ChangeDispPople(people);
    }
    public void ChangeDispPro(String ID, int product)
    {
        if (main.getNodeByID(ID) == null) return;
        main.PC_nodes[ID].ChangeDispPro(product);
    }
    public void ChangeDispCust(String ID, int cust)
    {
        if (main.getNodeByID(ID) == null) return;
        main.PC_nodes[ID].ChangeDispCust(cust);
    }

    public void addnode(String ID,int x,int y)
    {
        main.addme(ID,x,y);
    }

    public PlayerController clientNode(String ID)
    {
        if (main.getNodeByID(ID) == null) return null;
        return main.getNodeByID(ID);
    }
    public void AddPath(String A_ID, String B_ID)
    {

        if (this.main.PC_nodes[A_ID]==null|| this.main.PC_nodes[B_ID] == null) return;

        this.main.addPath(this.main.PC_nodes[A_ID].transform.root.gameObject, this.main.PC_nodes[B_ID].transform.root.gameObject);
    }

    public void DestroyNode(String ID)
    {
        main.DestroyNode_forServer(main.getNodeByID_GameObject(ID));
    }
    public void sendData(int protocol, string id ,int data )
    {

        if (main.getNodeByID(id) == null) return;
        main.PC_nodes[id].ChangeDispitem0(protocol, data);
    }
}
