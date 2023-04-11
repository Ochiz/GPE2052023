using System.Collections;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    private Room[,] grid;

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }
    public void GenerateMap()
    {
        grid = new Room[cols, rows];
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            for (int currentCol = 0; currentCol < cols; currentCol++)
            { 
                float xPosition = currentCol * roomWidth;
                float zPosition = currentRow * roomHeight;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                tempRoomObj.transform.parent = this.transform;
                tempRoomObj.name = "Room" + currentCol + "," + currentRow;
                Room tempRoom = tempRoomObj.GetComponent<Room>();
                grid[currentCol, currentRow] = tempRoom;

                if (currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentRow == rows - 1)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }
                if (currentCol == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (currentCol == cols - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorWest.SetActive(false);
                    tempRoom.doorEast.SetActive(false);
                }
            }
        }
    }
    public void DestroyMap()
    {
        if (grid != null)
        {
            foreach (Room tile in grid)
            {
                Destroy(tile);
            }
        }
    }
}
