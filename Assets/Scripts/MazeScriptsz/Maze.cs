using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{


    [SerializeField] MazeCell cellPrefab;
    [SerializeField] MazeCell ThreeWallCellPrefab;
    [SerializeField] MazeCell TwoWallCellPrefab;
    [SerializeField] MazeCell cornerTwoWallCellPrefab;
    [SerializeField] MazeCell OneWallCellPrefab;


    public int size;
    public float separationSpace = 2f;
    public float instantiationRotation = 0;
    public float generationStepDelay = 0.5f;


    private MazeCell[,] cells;

    private int[,] testGrid1 =
    {
        {1,1,1,0,0,0},
        {0,0,1,0,0,0},
        {0,1,1,1,1,1},
        {1,0,1,0,0,0},
        {1,1,1,1,0,0},
        {0,0,1,0,0,0}

    };
    private int[,] testGrid2 =
    {
        {1,1,1,1,1,0},
        {0,0,0,0,1,0},
        {1,1,1,1,1,1},
        {0,1,0,0,0,1},
        {0,1,1,1,1,0},
        {0,0,1,0,1,1}
    };



    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size, size];
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                if(testGrid2[x,z] == 1)
                {
                    yield return delay;
                    CreateCell(z,x);
                }
                //else yield return 0;
            }
        }
    }

    private void CreateCell(int z, int x)
    {

        MazeCell newCell = Instantiate(CellChoice(testGrid2, z, x)) as MazeCell;
        cells[z, x] = newCell;
        newCell.name = "Maze Cell " + z + ", " + x;
        newCell.transform.parent = transform;
        //newCell.transform.localPosition = new Vector3(x - sizeX * separationSpace + separationSpace, 0f, z - sizeZ * separationSpace + separationSpace);
        newCell.transform.localPosition = new Vector3((z-size) *separationSpace, 0f, (size-x) *separationSpace); //I would like it if it appeared in the middle but It seems to work for now
        newCell.transform.rotation = Quaternion.Euler(0, instantiationRotation, 0); //does this work this way?
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
