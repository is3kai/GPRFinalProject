using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class DungeonGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject spawnerPrefab;
    public Transform player;
    public int dungeonSizeX = 40;
    public int dungeonSizeY = 40;
    public int roomCount = 4;
    public int corridorCount = 10;

    // Replace these with your actual tile assets
    public Tile floorTile;
    public Tile wallTile;

    private List<Vector3Int> generatedSpawners = new List<Vector3Int>();

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        int[,] dungeonGrid = new int[dungeonSizeX, dungeonSizeY];

        // Start the dungeon generation at a random position
        int startX = Random.Range(0, dungeonSizeX);
        int startY = Random.Range(0, dungeonSizeY);

        CreateRoom(dungeonGrid, startX, startY);

        // Generate paths between rooms
        for (int i = 0; i < roomCount - 1; i++)
        {
            int nextX = Random.Range(0, dungeonSizeX);
            int nextY = Random.Range(0, dungeonSizeY);

            CreatePath(dungeonGrid, startX, startY, nextX, nextY, 3); // Use 3 as the width for a 3-tile-wide path
            CreateRoom(dungeonGrid, nextX, nextY);

            startX = nextX;
            startY = nextY;
        }

        // Additional generation logic...
        for (int i = 0; i < corridorCount; i++)
        {
            CreateCorridor(dungeonGrid);
        }

        // Create colliders for walls
        CreateWallColliders(dungeonGrid);

        // Render the dungeon
        RenderDungeon(dungeonGrid);

        // Place spawners in each room
        PlaceSpawners(dungeonGrid);

        // Initialize NavMesh
        InitializeNavMesh();
    }

    void CreateRoom(int[,] dungeonGrid, int startX, int startY)
    {
        int roomWidth = Random.Range(7, 15); // Adjust room size based on the required width for paths
        int roomHeight = Random.Range(7, 15);

        int x = Mathf.Clamp(startX - roomWidth / 2, 0, dungeonSizeX - roomWidth);
        int y = Mathf.Clamp(startY - roomHeight / 2, 0, dungeonSizeY - roomHeight);

        for (int i = x; i < x + roomWidth; i++)
        {
            for (int j = y; j < y + roomHeight; j++)
            {
                dungeonGrid[i, j] = 1;
            }
        }

        // Set the player's position to be in the center of the room
        player.position = new Vector3(x + roomWidth / 2, y + roomHeight / 2, 0);
    }

    void CreatePath(int[,] dungeonGrid, int startX, int startY, int endX, int endY, int pathWidth)
    {
        int currentX = startX;
        int currentY = startY;

        while (currentX != endX || currentY != endY)
        {
            for (int i = currentX - pathWidth / 2; i <= currentX + pathWidth / 2; i++)
            {
                for (int j = currentY - pathWidth / 2; j <= currentY + pathWidth / 2; j++)
                {
                    dungeonGrid[i, j] = 1;
                }
            }

            if (currentX < endX)
                currentX++;
            else if (currentX > endX)
                currentX--;

            if (currentY < endY)
                currentY++;
            else if (currentY > endY)
                currentY--;
        }
    }

    void CreateCorridor(int[,] dungeonGrid)
    {
        int corridorLength = Random.Range(5, 10);
        int x = Random.Range(0, dungeonSizeX - corridorLength);
        int y = Random.Range(0, dungeonSizeY - 1);

        for (int i = x; i < x + corridorLength; i++)
        {
            dungeonGrid[i, y] = 1;
        }
    }

    void CreateWallColliders(int[,] dungeonGrid)
    {
        // Iterate through the dungeon grid to create colliders for walls
        for (int x = 0; x < dungeonSizeX; x++)
        {
            for (int y = 0; y < dungeonSizeY; y++)
            {
                if (dungeonGrid[x, y] == 0)
                {
                    // Create a collider for walls
                    BoxCollider2D collider = new GameObject("WallCollider").AddComponent<BoxCollider2D>();
                    collider.transform.position = new Vector3(x + 0.5f, y + 0.5f, 0);
                    collider.size = Vector2.one;
                }
            }
        }
    }

    void RenderDungeon(int[,] dungeonGrid)
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < dungeonSizeX; x++)
        {
            for (int y = 0; y < dungeonSizeY; y++)
            {
                if (dungeonGrid[x, y] == 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
                }
            }
        }
    }

    void PlaceSpawners(int[,] dungeonGrid)
    {
        for (int x = 0; x < dungeonSizeX; x++)
        {
            for (int y = 0; y < dungeonSizeY; y++)
            {
                if (dungeonGrid[x, y] == 1)
                {
                    Vector3Int cellPosition = new Vector3Int(x, y, 0);

                    if (!generatedSpawners.Contains(cellPosition))
                    {
                        // Check if the player is within the room
                        if (IsPlayerInRoom(cellPosition, dungeonGrid))
                        {
                            Instantiate(spawnerPrefab, cellPosition, Quaternion.identity);
                            generatedSpawners.Add(cellPosition);
                        }
                    }
                }
            }
        }
    }

    bool IsPlayerInRoom(Vector3Int roomPosition, int[,] dungeonGrid)
    {
        int roomWidth = Random.Range(7, 15); // Adjust room size based on the required width for paths
        int roomHeight = Random.Range(7, 15);

        for (int i = roomPosition.x; i < roomPosition.x + roomWidth; i++)
        {
            for (int j = roomPosition.y; j < roomPosition.y + roomHeight; j++)
            {
                if (dungeonGrid[i, j] == 1 && Vector3.Distance(player.position, new Vector3(i, j, 0)) < 10)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void InitializeNavMesh()
    {
        NavMeshSurface navMeshSurface = GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
    }
}
