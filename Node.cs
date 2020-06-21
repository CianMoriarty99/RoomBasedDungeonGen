using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public int tileType; //Empty, Floor, Wall
    public int occupied; //Empty, Player, Enemy

    public Node(int _tileType, int _occupied){
        this.tileType = _tileType;
        this.occupied = _occupied;
    }

}
