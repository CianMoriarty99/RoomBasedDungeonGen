using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    
    public Node[,] grid;
    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    public int worldSize;
    public int roomSize;
    public int numberOfRooms;

    public int maxMobsPerRoom;
    
    void Start()
    {
        worldSize = numberOfRooms * roomSize *2;
        InitialiseGrid();
        GenerateDungeon(numberOfRooms, roomSize);
        FixWalls();
        PlaceTiles();
    }

    void Update()
    {
        
    }

    void InitialiseGrid(){
        grid = new Node[worldSize,worldSize];

        for (int x = 0; x < worldSize; x++){ 
			for (int y = 0; y < worldSize; y++){
                grid[x,y] = new Node(0,0);
                
            }
        }

        //Add Player 
        int j = 0 + worldSize/2 + roomSize/2;
        grid[j,j].occupied = 1;
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


        for (int x = posX; x < (posX + size); x++){
            
            if(x != (posX + size/2)){
            //Bottom wall of room
                if (grid[x, posY].tileType == 0) {
                    grid[x, posY].tileType = 2;

                }

            //Top wall of room
                if (grid[x, posY + size -1].tileType == 0) {
                    grid[x, posY + size -1].tileType = 2;
                }
            }

			for (int y = posY; y < (posY + size); y++){


                if(y != (posY + size/2)){

                    //Left wall of room
                    if (grid[posX, y].tileType == 0) {
                        grid[posX, y].tileType = 2;
                    }

                    //Right wall of room
                    if (grid[posX + size-1, y].tileType == 0) {   
                        grid[posX + size-1, y ].tileType = 2;
                    }
                }
                    
                    //Rest is floor
                    if(grid[x,y].tileType == 0){
                        grid[x,y].tileType = 1;

                    }
                    

                }
        }

        if(pos != new Vector2(worldSize/2,worldSize/2)){

            //Spawn enemies per room
            int m = Random.Range(0,maxMobsPerRoom);
            Debug.Log(m);

            for(int i = 0; i < m; i++)
            {
                //Debug.Log("How Many Times");
                int randX = Random.Range(posX + 1,posX - 1 + size);
                int randY = Random.Range(posY + 1,posY - 1 + size);

                if(grid[randX,randY].tileType != 2){
                    grid[randX,randY].occupied = 2;
                    
                    
                    
                }

            }
        }
        

            
        

    }

    void FixWalls(){

        for (int x = 0; x < worldSize - 1; x++){ 
			for (int y = 0; y < worldSize ; y++){
                
                if (grid[x,y].tileType == 1)
                    if(grid[x+1,y].tileType == 0 || grid[x-1,y].tileType == 0 || grid[x,y+1].tileType == 0 || grid[x,y-1].tileType == 0){
                        grid[x,y].tileType = 2;
                    }

                

            }
        }
    }

    


    void PlaceTiles(){
        
        for (int x = 0; x < worldSize; x++){ 
			for (int y = 0; y < worldSize; y++){

                if(grid[x,y].tileType == 1)
                    Instantiate(floorTile, new Vector2(x,y), Quaternion.identity);

                if(grid[x,y].tileType == 2)
                    Instantiate(wallTile, new Vector2(x,y), Quaternion.identity);
                
                if(grid[x,y].occupied == 1)
                    Instantiate(playerPrefab, new Vector2(x,y), Quaternion.identity);

                if(grid[x,y].occupied == 2)
                    Instantiate(enemyPrefab, new Vector2(x,y), Quaternion.identity);

            }
        }

    }




}
