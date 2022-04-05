using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileControll
{
    private environment evmt;

    public Tilemap tile;
    public TileBase tile1;

    // Start is called before the first frame update
    public void init()
    {
        evmt = new environment();
        var position = new Vector3Int(0, 0, 0);
        for (int i = 0; i < 200; i++)
        {
            for (int j = 0; j < 200; j++)
            {
                position.Set(i, j, 0);
                tile.SetTile(position, tile1);
                tile.SetTileFlags(position, TileFlags.None);
            }
        }
    }

    // Update is called once per frame
    public void Update()
    {
        evmt.update();
        var position = new Vector3Int(0, 0, 0);
        Color color = new Color();
        color.a = (float)0.9;
        for (int i = 0; i < evmt.SIZE; i++)
        {
            for (int j = 0; j < evmt.SIZE; j++)
            {
                color.g = (float)evmt.map[i, j];
                position.Set(i, j, 0);
                tile.SetColor(position, color);
            }
        }
    }
}
