using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class dictates the behavior of the maze object. It Generates the maze beased on a selected layout by choosing the appropriate piece in each position.
/// </summary>
public class Maze : MonoBehaviour
{

    [Header("Cell Prefabs")]
    [SerializeField] MazeCell cellPrefab;
    [SerializeField] MazeCell ThreeWallCellPrefab;
    [SerializeField] MazeCell TwoWallCellPrefab;
    [SerializeField] MazeCell cornerTwoWallCellPrefab;
    [SerializeField] MazeCell OneWallCellPrefab;

    [Header("Class Properties")]
    [SerializeField] int size;
    [SerializeField] float separationSpace = 2f;
    [SerializeField] float instantiationRotation = 0;
    [SerializeField] float generationStepDelay = 0f;


    private MazeCell[,] cells;

    List<int[,]> array = new List<int[,]>()
    { new int[,]
  {
    { 1, 1, 1, 0, 0, 0 },
    { 0, 0, 1, 0, 0, 0 },
    { 0, 1, 1, 1, 1, 1 },
    { 1, 0, 1, 0, 0, 0 },
    { 1, 1, 1, 1, 0, 0 },
    { 0, 0, 1, 0, 0, 0 }

            },
    new int[,]
  {
    { 1, 1, 1, 1, 1, 0 },
    { 0, 0, 0, 0, 1, 0 },
    { 1, 1, 1, 1, 1, 1 },
    { 0, 1, 0, 0, 0, 1 },
    { 0, 1, 1, 1, 1, 0 },
    { 0, 0, 1, 0, 1, 1 }
            },
    new int[,]
  {
    { 1, 0, 1, 1, 1, 1 },
    { 1, 0, 1, 1, 0, 1 },
    { 1, 1, 1, 0, 0, 0 },
    { 0, 1, 1, 1, 1, 1 },
    { 0, 1, 0, 1, 0, 0 },
    { 1, 1, 0, 1, 1, 1 }
            },
    new int[,]
  {
    { 0, 1, 1, 1, 1, 1 },
    { 0, 0, 1, 0, 1, 0 },
    { 0, 1, 1, 1, 0, 0 },
    { 0, 0, 0, 1, 1, 1 },
    { 0, 0, 0, 1, 0, 1 },
    { 0, 1, 1, 1, 0, 1 }
            },
    new int[,]
  {
    { 0, 1, 1, 0, 1, 0 },
    { 1, 0, 1, 0, 1, 1 },
    { 1, 1, 1, 0, 0, 1 },
    { 0, 1, 1, 1, 0, 1 },
    { 1, 1, 0, 1, 1, 1 },
    { 0, 1, 1, 0, 0, 1 }
            },
    new int[,]
  {
    { 0, 1, 0, 0, 1, 1 },
    { 0, 1, 0, 0, 0, 1 },
    { 1, 1, 1, 0, 1, 1 },
    { 0, 0, 1, 1, 1, 0 },
    { 0, 1, 1, 0, 1, 1 },
    { 1, 1, 0, 0, 0, 1 }
            },
    new int[,]
  {
    { 1, 1, 1, 0, 1, 1 },
    { 0, 0, 1, 0, 1, 0 },
    { 0, 1, 1, 0, 1, 1 },
    { 1, 1, 0, 0, 0, 1 },
    { 0, 1, 1, 1, 1, 1 },
    { 0, 0, 0, 0, 1, 0 }
            },
    new int[,]
  {
    { 1, 1, 0, 0, 1, 1 },
    { 1, 0, 0, 0, 0, 1 },
    { 1, 0, 1, 0, 0, 1 },
    { 1, 1, 1, 1, 1, 1 },
    { 0, 1, 0, 1, 0, 0 },
    { 1, 1, 0, 1, 1, 1 }
            },
    new int[,]
  {
    { 0, 0, 1, 1, 1, 0 },
    { 0, 0, 1, 0, 0, 1 },
    { 1, 0, 1, 1, 1, 1 },
    { 1, 1, 0, 0, 1, 0 },
    { 0, 1, 1, 1, 1, 0 },
    { 0, 1, 0, 1, 0, 0 }
            },
    new int[,]
  {
    { 1, 0, 1, 0, 1, 1 },
    { 1, 1, 1, 1, 1, 0 },
    { 0, 1, 0, 1, 0, 0 },
    { 0, 1, 1, 1, 0, 0 },
    { 1, 1, 0, 1, 1, 1 },
    { 1, 0, 1, 1, 0, 1 }
            }
        }
;

public int[,] TestGrid()
    {
        var a = Random.Range(0, 10);
        
        return array[a];
    }


    /// <summary>
    /// Coroutine that generates the maze by going through every index of the specified grid layout.
    /// </summary>
    /// <returns> the time delay for which to wait before generating the next tile</returns>
    public IEnumerator Generate()
    {
        int[,] selectedGrid = TestGrid();
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size, size];
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                if(selectedGrid[x,z] == 1)
                {
                    yield return delay;
                    CreateCell(z,x,selectedGrid);
                }
            }
        }
    }

    /// <summary>
    /// Method that instantiates a Maze cell, its name, parent transform, position relative to the parent transform, and rotation 
    /// based on the position in the specified grid. Also adds the newly instantiated cell the Maze 2D array.
    /// </summary>
    /// <param name="z">Specifies which column in the test grid that this cell is located in.</param>
    /// <param name="x">Specifies which row in the test grid that this cell is located in.</param>
    /// <param name="selectedGrid">Specifies the layout grid that was chosen to generate the maze</param>
    private void CreateCell(int z, int x, int[,]selectedGrid)
    {

        MazeCell newCell = Instantiate(CellChoice(selectedGrid, z, x)) as MazeCell;
        cells[z, x] = newCell;
        newCell.name = "Maze Cell " + z + ", " + x;
        newCell.transform.parent = transform;
        //newCell.transform.localPosition = new Vector3(x - sizeX * separationSpace + separationSpace, 0f, z - sizeZ * separationSpace + separationSpace);
        newCell.transform.localPosition = new Vector3((z-size) *separationSpace, 0f, (size-x) *separationSpace);
        newCell.transform.rotation = Quaternion.Euler(0, instantiationRotation, 0);
    }

    /// <summary>
    /// Function that chooses which type of prefab current cell is, and which rotation it should have based on the surrounding 
    /// cells of the given position in the grid array.
    /// </summary>
    /// <param name="grid">The test grid that the maze is using to generate its cells.</param>
    /// <param name="z">The column in the test grid that this cell is located in </param>
    /// <param name="x">The row in the test grid that this cell is located in</param>
    /// <returns>The chosen prefab to instantiate</returns>
    private MazeCell CellChoice(int[,] grid, int z, int x)
    {
        bool left = false;
        bool right = false;
        bool up = false;
        bool down = false;
        int count = 0;

        if(x - 1 >= 0)
        {
            if(grid[x - 1, z] == 1)
            {
                up = true;
                count++;
            }
            
        }
        if(z - 1 >= 0)
        {
            if(grid[x, z - 1] == 1)
            {
                left = true;
                count++;
            }
        }
        if(x + 1 < size)
        {
            if(grid[x + 1, z] == 1)
            {
                down = true;
                count++;
            }
        }
        if(z + 1 < size)
        {
            if(grid[x, z + 1] == 1)
            {
                right = true;
                count++;
            }
        }
        //If cell is only connected to one other cell it is a 3 wall cell
        if (count == 1)
        {
            if(left) instantiationRotation = 90; //rotate 90 degrees 
            else if(up) instantiationRotation = 180; //rotate 180 degrees 
            else if(right) instantiationRotation = 270; //rotate 270 degrees
            else instantiationRotation = 0; //default points down
            return ThreeWallCellPrefab;
        }
        //If cell is only connected to 2 other cells:
        if(count == 2)
        {
            //If two connected cells are in the same row or same column it is a 2 wall cell
            if((left && right) || (up && down)) 
            {
                if(left && right) instantiationRotation = 90; //rotate 90 degrees 
                else instantiationRotation = 0; //default points down and up by default
                return TwoWallCellPrefab;
            }
            //Otherwise it is a corner 2 wall cell
            else
            {
                if(down && left) instantiationRotation = 90; //rotate 90 degrees
                else if(up && left) instantiationRotation = 180; //rotate 180 degrees
                else if(up && right) instantiationRotation = 270; //rotate 270 degrees
                else instantiationRotation = 0; //default points down and right by default
                return cornerTwoWallCellPrefab;
            }
        }
        //If cell is only connected to 3 other cells it is a 1 wall cell
        if (count == 3)
        {
            if(!up) instantiationRotation = 90; //rotate 90 degrees
            else if(!right) instantiationRotation = 180; //rotate 180 degrees
            else if(!down) instantiationRotation = 270; //rotate 270 degrees
            else instantiationRotation = 0; //default wall points left by default
            return OneWallCellPrefab;
        }
        else return cellPrefab;
    }
}
