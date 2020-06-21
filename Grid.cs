using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    
    public Node[,] grid;
    public GameObject floorTile;
    public GameObject wallTile;
    public int worldSize;
    public int roomSize;
    public int numberOfRooms;
    
    void Start()
    {
        worldSize = numberOfRooms * roomSize + 1;
        InitialiseGrid();
        GenerateDungeon(numberOfRooms, roomSize);
        FixWalls();
    }

    void Update()
    {
        
    }

    void GenerateDungeon(int numberOfRooms, int roomSize){
        //Start in the middle, make room of roomsize
        int i = 0;
        Vector2 currentPos = new Vector2 (worldSize/2,worldSize/2);
        List<Vector2> tagged = new List<Vector2>();
        

        while (i < numberOfRooms){
            if(!(tagged.Contains(currentPos))) {
                GenerateRoom(currentPos, roomSize);
                tagged.Add(currentPos);
                i++;
            }

            
            
        int r = Random.Range(1,5); // up, down, left, right

        switch (r)
        {
        case 1:
            //Debug.Log("Up");
            currentPos.y += roomSize;
            break;
        case 2:
            //Debug.Log("Down");
            currentPos.y -= roomSize;
            break;
        case 3:
            //Debug.Log("Left");
            currentPos.x -= roomSize;
            break;
        default:
            //Debug.Log("Right");
            currentPos.x += roomSize;
            break;
        }

        }



    }


    void GenerateRoom(Vector2 pos, int size){

        int posX = (int)pos.x;
        int posY = (int)pos.y;
        //Debug.Log(size);
        //Debug.Log(posX);
        //Debug.Log(posY);

        // int modulo = size % 2;


        for (int x = posX; x < (posX + size); x++){
            
            if(x != (posX + size/2)){
            //Bottom wall of room
                if (grid[x, posY].tileType == 0) {

                    grid[x, posY].tileType = 2;
                    Instantiate(wallTile, new Vector3(x, posY,0) , Quaternion.identity);

                }

            //Top wall of room
                if (grid[x, posY + size - 1].tileType == 0) {
                    Instantiate(wallTile, new Vector3(x, posY + size-1 ,0) , Quaternion.identity);
                    grid[x, posY + size - 1].tileType = 2;
                }
            }

			for (int y = posY; y < (posY + size); y++){


                if(y != (posY + size/2)){

                    //Left wall of room
                    if (grid[posX, y].tileType == 0) {
                        Instantiate(wallTile, new Vector3(posX, y , 0) , Quaternion.identity);
                        grid[posX, y].tileType = 2;
                    }

                    //Right wall of room
                    if (grid[posX + size-1, y].tileType == 0) {
                        Instantiate(wallTile, new Vector3(posX + size-1, y , 0) , Quaternion.identity);     
                        grid[posX + size-1, y ].tileType = 2;
                    }
                }
                    
                    //Rest is floor
                    if(grid[x,y].tileType == 0){
                        grid[x,y].tileType = 1;
                        Instantiate(floorTile, new Vector3(x,y,0) , Quaternion.identity);
                    }
                    

                }
            
        }



        
    }

    void FixWalls(){

        for (int x = 0; x < worldSize; x++){ 
			for (int y = 0; y < worldSize; y++){
                
                if (grid[x,y].tileType == 1)
                    if(grid[x+1,y].tileType == 0 || grid[x-1,y].tileType == 0 || grid[x,y+1].tileType == 0 || grid[x,y-1].tileType == 0){
                        grid[x,y].tileType = 2;
                        Instantiate(wallTile, new Vector3(x,y,0) , Quaternion.identity);
                    }
            }
        }
    }

    void InitialiseGrid(){
        grid = new Node[worldSize,worldSize];

        for (int x = 0; x < worldSize; x++){ 
			for (int y = 0; y < worldSize; y++){
                grid[x,y] = new Node(0,0);
                
            }
        }
    }


}
