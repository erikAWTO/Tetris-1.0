using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    //Kollar om tetrominon �r �ver v�r spelplan.
    public bool CheckIsAboveGrid(Tetromino tetromino)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            foreach (Transform children in tetromino.transform)
            {
                int posX = Mathf.RoundToInt(children.transform.position.x);
                int posY = Mathf.RoundToInt(children.transform.position.y);

                Vector2 pos = new Vector2(posX, posY);

                if (pos.y >= gridHeight - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //Vi g�r igenom den angivna raden och kollar om positionerna �r tom eller inte.
    //S� snabbt vi hittar en tom --> Return false;
    //Om ingen �r tom s� betyder det att vi har en full rad.
    public bool IsFullRowAt(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void DeleteMinoAt(int y)
    {
        //Tar bort raden och s�tter raden till tom.
        for (int x = 0; x < gridWidth; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    //Allting som �r ovanf�r y flyttas ned.
    public void MoveAllRowsDown(int y)
    {
        for (int i = y; i < gridHeight; i++)
        {
            MoveRowDown(i);
        }
    }

    //Tar bort raden och flyttar ned raderna ovanf�r.
    public void DeleteRow()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            if (IsFullRowAt(y))
            {
                DeleteMinoAt(y);

                MoveAllRowsDown(y + 1);

                --y;
            }
        }
    }

    public void UpdateGrid(Tetromino tetromino)
    {
        foreach (Transform children in tetromino.transform)
        {
            int posX = Mathf.RoundToInt(children.transform.position.x);
            int posY = Mathf.RoundToInt(children.transform.position.y);

            grid[posX, posY] = children;
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}