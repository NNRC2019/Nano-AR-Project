using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    int[,,] array = {

  {
    { 1, 1, 1, 0, 0, 0 },
    { 0, 0, 1, 0, 0, 0 },
    { 0, 1, 1, 1, 1, 1 },
    { 1, 0, 1, 0, 0, 0 },
    { 1, 1, 1, 1, 0, 0 },
    { 0, 0, 1, 0, 0, 0 }

            },
  {
    { 1, 1, 1, 1, 1, 0 },
    { 0, 0, 0, 0, 1, 0 },
    { 1, 1, 1, 1, 1, 1 },
    { 0, 1, 0, 0, 0, 1 },
    { 0, 1, 1, 1, 1, 0 },
    { 0, 0, 1, 0, 1, 1 }
            },
  {
    { 1, 0, 1, 1, 1, 1 },
    { 0, 0, 1, 1, 0, 1 },
    { 0, 1, 1, 0, 0, 0 },
    { 0, 0, 1, 0, 1, 1 },
    { 0, 0, 0, 1, 1, 1 },
    { 1, 0, 0, 0, 1, 1 }
            },
  {
    { 0, 1, 1, 1, 1, 1 },
    { 0, 0, 1, 0, 1, 0 },
    { 0, 1, 1, 1, 0, 0 },
    { 0, 0, 0, 1, 1, 0 },
    { 0, 0, 0, 1, 0, 1 },
    { 0, 1, 1, 0, 0, 1 }
            },
  {
    { 0, 1, 1, 0, 1, 0 },
    { 1, 0, 1, 0, 1, 1 },
    { 1, 1, 1, 0, 0, 1 },
    { 0, 1, 1, 1, 0, 1 },
    { 1, 1, 0, 1, 1, 1 },
    { 0, 0, 1, 0, 0, 1 }
            },
  {
    { 0, 1, 0, 0, 1, 1 },
    { 0, 0, 0, 0, 0, 1 },
    { 1, 0, 1, 1, 0, 1 },
    { 0, 1, 0, 1, 1, 0 },
    { 0, 1, 1, 0, 0, 1 },
    { 1, 1, 0, 0, 0, 1 }
            },
  {
    { 1, 0, 1, 0, 0, 1 },
    { 0, 0, 1, 0, 0, 0 },
    { 0, 1, 1, 0, 1, 0 },
    { 1, 1, 0, 0, 0, 1 },
    { 0, 1, 1, 1, 1, 1 },
    { 0, 0, 0, 0, 1, 0 }
            },
  {
    { 1, 1, 0, 0, 1, 1 },
    { 1, 0, 0, 0, 0, 1 },
    { 0, 0, 1, 0, 0, 1 },
    { 1, 1, 0, 1, 1, 1 },
    { 0, 1, 1, 0, 1, 0 },
    { 1, 1, 0, 1, 1, 1 }
            },
  {
    { 0, 0, 1, 1, 1, 0 },
    { 0, 0, 1, 1, 0, 1 },
    { 1, 0, 1, 1, 1, 0 },
    { 1, 1, 0, 0, 1, 0 },
    { 0, 1, 1, 1, 1, 0 },
    { 0, 1, 0, 1, 1, 0 }
            },
  {
    { 1, 1, 1, 0, 0, 1 },
    { 1, 1, 0, 0, 0, 0 },
    { 0, 1, 0, 0, 0, 0 },
    { 0, 1, 1, 1, 1, 0 },
    { 1, 0, 0, 0, 1, 1 },
    { 1, 0, 1, 0, 1, 1 }
            }
        };

    public int[,] TestGrid()
    {
        var a = Random.Range(0, 10);
        var i = 0;
        var j = 0;
        int[,] testGrid2 = {};
        for (i = 0; i < 6; i++)
        {
            for (j = 0; j < 6; j++)
            {
                testGrid2[i,j] = array[a,i,j];
            }
        }
        return testGrid2;
    }


    public IEnumerator Generate()
    {
        int[,] testGrid2 = TestGrid();
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size, size];
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                if(testGrid2[x,z] == 1)
                {
                    yield return delay;
                    CreateCell(z,x,testGrid2);
                }
            }
        }
    }

    private void CreateCell(int z, int x, int[,]testGrid2)
    {

        MazeCell newCell = Instantiate(CellChoice(testGrid2, z, x)) as MazeCell;
        cells[z, x] = newCell;
        newCell.name = "Maze Cell " + z + ", " + x;
        newCell.transform.parent = transform;
        //newCell.transform.localPosition = new Vector3(x - sizeX * separationSpace + separationSpace, 0f, z - sizeZ * separationSpace + separationSpace);
        newCell.transform.localPosition = new Vector3((z-size) *separationSpace, 0f, (size-x) *separationSpace);
        newCell.transform.rotation = Quaternion.Euler(0, instantiationRotation, 0);
    }


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
