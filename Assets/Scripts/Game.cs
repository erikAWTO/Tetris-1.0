using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    private int rowsThisTurn = 0;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    // Underl�ttar om vi vill �ndra p� po�ng per rad.
    public enum ScorePerRow
    {
        One = 100,
        Two = 250,
        Three = 500,
        Four = 1000
    }

    // Uppdaterar po�ngen.
    public void UpdateScore()
    {
        if (rowsThisTurn > 0)
        {
            switch (rowsThisTurn)
            {
                case 1:
                    Score.currentScore += (int)ScorePerRow.One;
                    break;

                case 2:
                    Score.currentScore += (int)ScorePerRow.Two;
                    break;

                case 3:
                    Score.currentScore += (int)ScorePerRow.Three;
                    break;

                case 4:
                    Score.currentScore += (int)ScorePerRow.Four;
                    break;

                default:
                    break;
            }
            Tetromino.decreaseFalltime = true;
            rowsThisTurn = 0;
        }
    }

    // Kollar om tetrominon �r �ver v�r spelplan.
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

    // Vi g�r igenom den angivna raden och kollar om positionerna �r null eller inte.
    // S� snabbt vi hittar en null --> Return false;
    // Om ingen �r tom s� betyder det att vi har en full rad.
    public bool IsFullRow(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        rowsThisTurn++;
        Score.linesCleared++;
        return true;
    }

    // Tar bort alla tetrominos p� en rad.
    public void DeleteTetromino(int y)
    {
        //Tar bort raden och s�tter raden till tom.
        for (int x = 0; x < gridWidth; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    // Flyttar ned en rad.
    public void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] != null)
            {
                //F�rflyttar radens positioner i grid ett steg ned�t.
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                //Uppdatera positionen f�r tetrominon.
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // Allting som �r ovanf�r raden y flyttas ned.
    public void MoveAllRowsDown(int y)
    {
        for (int i = y; i < gridHeight; i++)
        {
            MoveRowDown(i);
        }
    }

    // Tar bort raden och flyttar ned raderna ovanf�r.
    public void DeleteRow()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            if (IsFullRow(y))
            {
                DeleteTetromino(y);
                MoveAllRowsDown(y + 1);

                //y minskar med en varje g�ng en rad raderas.
                //F�r att se till att n�sta iteration i for-loopen forts�tter vid r�tt index.
                --y;
            }
        }
    }

    // Uppdaterar grid med de upptagna positionerna.
    public void UpdateGrid(Tetromino tetromino)
    {
        foreach (Transform children in tetromino.transform)
        {
            int posX = Mathf.RoundToInt(children.transform.position.x);
            int posY = Mathf.RoundToInt(children.transform.position.y);

            grid[posX, posY] = children;
        }
    }

    // Tetrisproffsets mardr�m :O
    public void GameOver()
    {
        //Score.finalScore = Score.currentScore;
        Score.currentScore = 0;
        Score.linesCleared = 0;
        SceneManager.LoadScene("GameOver");
    }
}