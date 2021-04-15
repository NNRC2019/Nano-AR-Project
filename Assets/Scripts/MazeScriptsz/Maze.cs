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

    [SerializeField] GameObject BeadPlaceHolderPrefab;
    [SerializeField] int beadAmount;

    public int size;
    public float separationSpace = 2f;
    public float instantiationRotation = 0;
    public float generationStepDelay = 0.5f;

    private int beadsPerArea;

    private int beadsForArea0;
    private int cellCounteroftheArea0;

    private int beadsForArea1;
    private int cellCounteroftheArea1;

    private int beadsForArea2;
    private int cellCounteroftheArea2;
    
    private int beadsForArea3;
    private int cellCounteroftheArea3;
    
    private int beadsForArea4;
    private int cellCounteroftheArea4;
    
    private int beadsForArea5;
    private int cellCounteroftheArea5;

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

    private void Start()
    {
        beadsPerArea = Mathf.FloorToInt(Mathf.Floor(beadAmount / 6));
        int leftoverBeads = beadAmount % 6;

        beadsForArea0 = beadsPerArea;
        cellCounteroftheArea0 = 1;

        beadsForArea1 = beadsPerArea + (leftoverBeads > 0 ? 1 : 0);
        cellCounteroftheArea1 = 1;

        beadsForArea2 = beadsPerArea + (leftoverBeads > 1 ? 1 : 0);
        cellCounteroftheArea2 = 1;

        beadsForArea3 = beadsPerArea + (leftoverBeads > 2 ? 1 : 0);
        cellCounteroftheArea3 = 1;

        beadsForArea4 = beadsPerArea + (leftoverBeads > 3 ? 1 : 0);
        cellCounteroftheArea4 = 1;

        beadsForArea5 = beadsPerArea + (leftoverBeads > 4 ? 1 : 0);
        cellCounteroftheArea5 = 1;
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
 
        switch (z)
        {
            case 5:
                int beadsToInstantiateForArea5 = Random.Range(0, beadsForArea5 + 1);
                beadsToInstantiateForArea5 = cellCounteroftheArea5 == 3 ? beadsForArea5 : beadsToInstantiateForArea5;
                beadsToInstantiateForArea5 = beadsToInstantiateForArea5 > beadsForArea5
                                                ? beadsToInstantiateForArea5 - beadsForArea5 : beadsToInstantiateForArea5;
                for (int count = 0; count < beadsToInstantiateForArea5; count++)
                {
                    BeadInstantiation(z, x, newCell, count + 1);
                }
                cellCounteroftheArea5++;
                beadsForArea5 -= beadsToInstantiateForArea5;
                break;
            case 4:
                int beadsToInstantiateForArea4 = Random.Range(0, beadsForArea4 + 1);
                beadsToInstantiateForArea4 = cellCounteroftheArea4 == 5 ? beadsForArea4 : beadsToInstantiateForArea4;
                beadsToInstantiateForArea4 = beadsToInstantiateForArea4 > beadsForArea4
                                                ? beadsToInstantiateForArea4 - beadsForArea4 : beadsToInstantiateForArea4;
                for (int count = 0; count < beadsToInstantiateForArea4; count++)
                {
                    BeadInstantiation(z, x, newCell, count + 1);
                }
                cellCounteroftheArea4++;
                beadsForArea4 -= beadsToInstantiateForArea4;
                break;
            case 3:
                int beadsToInstantiateForArea3 = Random.Range(0, beadsForArea3 + 1);
                beadsToInstantiateForArea3 = cellCounteroftheArea3 == 3 ? beadsForArea3 : beadsToInstantiateForArea3;
                beadsToInstantiateForArea3 = beadsToInstantiateForArea3 > beadsForArea3
                                                ? beadsToInstantiateForArea3 - beadsForArea3 : beadsToInstantiateForArea3;
                for (int count = 0; count < beadsToInstantiateForArea3; count++)
                {
                    BeadInstantiation(z, x, newCell, count + 1);
                }
                cellCounteroftheArea3++;
                beadsForArea3 -= beadsToInstantiateForArea3;
                break;
            case 2:
                int beadsToInstantiateForArea2 = Random.Range(0, beadsForArea2 + 1);
                beadsToInstantiateForArea2 = cellCounteroftheArea2 == 4 ? beadsForArea2 : beadsToInstantiateForArea2;
                beadsToInstantiateForArea2 = beadsToInstantiateForArea2 > beadsForArea2
                                                ? beadsToInstantiateForArea2 - beadsForArea2 : beadsToInstantiateForArea2;
                for (int count = 0; count < beadsToInstantiateForArea2; count++)
                {
                    BeadInstantiation(z, x, newCell, count + 1);
                }
                cellCounteroftheArea2++;
                beadsForArea2 -= beadsToInstantiateForArea2;
                break;
            case 1:
                int beadsToInstantiateForArea1 = Random.Range(0, beadsForArea1 + 1);
                beadsToInstantiateForArea1 = cellCounteroftheArea1 == 4 ? beadsForArea1 : beadsToInstantiateForArea1;
                beadsToInstantiateForArea1 = beadsToInstantiateForArea1 > beadsForArea1
                                                ? beadsToInstantiateForArea1 - beadsForArea1 : beadsToInstantiateForArea1;
                for (int count = 0; count < beadsToInstantiateForArea1; count++)
                {
                    BeadInstantiation(z, x, newCell, count + 1);
                }
                cellCounteroftheArea1++;
                beadsForArea1 -= beadsToInstantiateForArea1;
                break;
            case 0:
                int beadsToInstantiateForArea0 = Random.Range(0, beadsForArea0 + 1);
                beadsToInstantiateForArea0 = cellCounteroftheArea0 == 2 ? beadsForArea0 : beadsToInstantiateForArea0;
                beadsToInstantiateForArea0 = beadsToInstantiateForArea0 > beadsForArea0
                                                ? beadsToInstantiateForArea0 - beadsForArea0 : beadsToInstantiateForArea0;
                for (int count = 0; count < beadsToInstantiateForArea0; count++)
                {
                    BeadInstantiation(z, x, newCell, count + 1);
                }
                cellCounteroftheArea0++;
                beadsForArea0 -= beadsToInstantiateForArea0;
                break;
            default:
                print("Incorrect z value.");
                break;
        }
    }

    private void BeadInstantiation(int z, int x, MazeCell newCell, int count)
    {
        GameObject newBead = Instantiate(BeadPlaceHolderPrefab, newCell.transform.localPosition, transform.rotation);
        newBead.name = "Bead " + z + ", " + x + ", " + count;
        newBead.transform.localPosition = new Vector3(newBead.transform.localPosition.x - Random.Range(-1.9f, 1.9f), //Select only the extremes (Random.Range(-1.9f, 1.9f) < 0 ? -1.9f : 1.9f),
                                                      newBead.transform.localPosition.y + Random.Range(0.16f, 1.84f),
                                                      newBead.transform.localPosition.z + Random.Range(-1.9f, 1.9f)); //Select only the extremes (Random.Range(-1.9f, 1.9f) < 0 ? -1.9f : 1.9f));
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
