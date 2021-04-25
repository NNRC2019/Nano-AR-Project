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
    [SerializeField] GameObject BeadPlaceHolderPrefab;
    [SerializeField] int beadAmount;
    
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
    /// The Start method defines the amount of beads to instantiate per area and stores them in variables along with a counter for each of them.
    /// </summary>
    private void Start()
    {
        beadsPerArea = Mathf.FloorToInt(Mathf.Floor(beadAmount / size));
        int leftoverBeads = beadAmount % size;

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

    /// <summary>
    /// Method that instantiates a Maze cell then sets its name, parent transform, position relative to the parent transform, and rotation 
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
        newCell.transform.localPosition = new Vector3((z) *separationSpace, 0f, (x) *separationSpace);
        newCell.transform.rotation = Quaternion.Euler(0, instantiationRotation, 0);
 
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

    /// <summary>
    /// The BeadInstantiation method uses the z and x parameters to name each bead object similarly to the cell they correspond to and
    /// the count to give a unique name to each one of them. The newCell parameter is used to locate the bead once instantiated near it
    /// in a random position inside the cell.
    /// </summary>
    /// <param name="z"></param>
    /// <param name="x"></param>
    /// <param name="newCell"></param>
    /// <param name="count"></param>
    private void BeadInstantiation(int z, int x, MazeCell newCell, int count)
    {
        GameObject newBead = Instantiate(BeadPlaceHolderPrefab);
        newBead.name = "Bead " + z + ", " + x + ", " + count;
        newBead.transform.parent = newCell.transform;
        /*
         * The random range is derived from the position relative to the cell as a parent of the bead
         * If the bead does not have a parent, the random ranges should be the following
         * newBead.transform.localPosition = new Vector3(newCell.transform.localPosition.x + Random.Range(-1.9f, 1.90f),
         *                                               newCell.transform.localPosition.y + Random.Range(0.16f, 1.84f),
         *                                               newCell.transform.localPosition.z + Random.Range(-1.9f, 1.9f));
         * */
        newBead.transform.localPosition = new Vector3(Random.Range(-0.48f, 0.48f),
                                                      Random.Range(0.15f, 1.95f),
                                                      Random.Range(0.48f, -0.48f));
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
            if(left) instantiationRotation = 90;
            else if(up) instantiationRotation = 180; 
            else if(right) instantiationRotation = 270;
            else instantiationRotation = 0;
            return ThreeWallCellPrefab;
        }
        //If cell is only connected to 2 other cells:
        if(count == 2)
        {
            //If two connected cells are in the same row or same column it is a 2 wall cell
            if((left && right) || (up && down)) 
            {
                if(left && right) instantiationRotation = 90;
                else instantiationRotation = 0;
                return TwoWallCellPrefab;
            }
            //Otherwise it is a corner 2 wall cell
            else
            {
                if(down && left) instantiationRotation = 90;
                else if(up && left) instantiationRotation = 180;
                else if(up && right) instantiationRotation = 270;
                else instantiationRotation = 0;
                return cornerTwoWallCellPrefab;
            }
        }
        //If cell is only connected to 3 other cells it is a 1 wall cell
        if (count == 3)
        {
            if(!up) instantiationRotation = 90;
            else if(!right) instantiationRotation = 180;
            else if(!down) instantiationRotation = 270;
            else instantiationRotation = 0;
            return OneWallCellPrefab;
        }
        else return cellPrefab;
    }
}
