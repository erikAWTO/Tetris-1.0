using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    private int numberOfRowsThisTurn = 0;
   
    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    public enum ScorePerRow
    {
        One = 1,
        Two,
        Three,
        Four,
        Five,
        Six
    }

    private void Update()
    {
        UpdateScore();
        //Debug.Log(ScorePerRow[0]);
    }

 
    public void UpdateScore()
    {
        if (numberOfRowsThisTurn > 0)
        {
            if (numberOfRowsThisTurn == 1)
            {
                Score.currentScore += 100;
            }

            if (numberOfRowsThisTurn == 2)
            {
                Score.currentScore += 500;
            }

            numberOfRowsThisTurn = 0;

            /*case 2:
                currentScore += (int)ScorePerRow.Two;
                break;

            case 3:
                currentScore += (int)ScorePerRow.Three;
                break;

            case 4:
                currentScore += (int)ScorePerRow.Four;
                break;

            case 5:
                currentScore += (int)ScorePerRow.Five;
                break;

            case 6:
                currentScore += (int)ScorePerRow.Six;
                break;*/
        }
    }


//Kollar om tetrominon är över vår spelplan.
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

//Vi går igenom den angivna raden och kollar om positionerna är tom eller inte.
//Så snabbt vi hittar en tom --> Return false;
//Om ingen är tom så betyder det att vi har en full rad.
public bool IsFullRowAt(int y)
{
    for (int x = 0; x < gridWidth; x++)
    {
        if (grid[x, y] == null)
        {
            return false;
        }
    }
    numberOfRowsThisTurn++;
    return true;
}

public void DeleteMinoAt(int y)
{
    //Tar bort raden och sätter raden till tom.
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

//Allting som är ovanför y flyttas ned.
public void MoveAllRowsDown(int y)
{
    for (int i = y; i < gridHeight; i++)
    {
        MoveRowDown(i);
    }
}

//Tar bort raden och flyttar ned raderna ovanför.
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