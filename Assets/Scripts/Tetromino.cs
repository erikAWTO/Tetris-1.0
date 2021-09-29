using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private float prevFalltime = 0f;

    private static float falltime;

    public static bool decreaseFalltime = false;
    private static bool gameStarted = false;

    private bool gamePaused = false;

    public Vector3 rotationPoint;

    private void Start()
    {
        // Vid f�rsta uppstart av spelet vill vi ha 0.8 i falltid.
        if (!gameStarted)
        {
            gameStarted = true;

            falltime = 0.8f;
        }
    }

    private void Update()
    {
        CheckIfPaused();

        if (!gamePaused)
        {
            CheckInput();
        }
    }

    private void CheckIfPaused()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                gamePaused = true;
                Time.timeScale = 0;
            }
            else
            {
                gamePaused = false;
                Time.timeScale = 1f;
            }
        }
    }

    // Input fr�n anv�ndaren.
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;

            if (!ValidPosition())
            {
                transform.position -= Vector3.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;

            if (!ValidPosition())
            {
                transform.position -= Vector3.right;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Rotera runt punkten som vi har angett. Detta kr�vs f�r att offseten p� vissa tetrominos inte ska bli fel.
            //Exempelbild p� vart vi vill har v�r rotationspunkt f�r de olika tetrominorna.
            //https://static.wikia.nocookie.net/tetrisconcept/images/3/3d/SRS-pieces.png/revision/latest/scale-to-width-down/336?cb=20060626173148
            //OBS! Rotationspunkten utg�r ifr�n parent.
            transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, 90f);

            if (!ValidPosition())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, -90f);
            }
        }
        // Vi kollar diffrenensen: (Tid sen spelet startade) - (F�reg�ende tid) om detta �verstiger v�r fallTime(falltid) kommer tetrominon flyttas ned ett steg.
        // Om ned�tpilen �r nedtryckt minskar vi fallTime(falltiden) och d�rmed kan vi flytta tetrominon snabbare.
        else if (Time.time - prevFalltime > (Input.GetKeyDown(KeyCode.DownArrow) ? falltime / 10 : falltime))
        {
            transform.position += Vector3.down;

            if (!ValidPosition())
            {
                transform.position -= Vector3.down;

                FindObjectOfType<Game>().UpdateGrid(this);

                FindObjectOfType<Game>().DeleteRow();

                FindObjectOfType<Game>().UpdateScore();

                //Om n�gon tetromino �r �ver spelplanen.
                if (FindObjectOfType<Game>().CheckIsAboveGrid(this))
                {
                    FindObjectOfType<Game>().GameOver();
                }

                //Sl� av nuvarande tetromino och spawna en ny.
                this.enabled = false;
                FindObjectOfType<Spawn>().SpawnTetromino();
            }
            //S�tter den f�rflutna tiden till tiden sen spelet startade.
            prevFalltime = Time.time;
            DecreaseFalltime();
            Debug.Log(falltime);
        }
    }

    // G�r s� att tetrominon hamnar p� den l�gsta tillg�ngliga raden direkt.
    private void HardDrop()
    {
        if (ValidPosition())
        {
            while (ValidPosition())
            {
                transform.position += Vector3.down;
            }
            if (!ValidPosition())
            {
                transform.position -= Vector3.down;
            }
        }
    }

    // Minskar falltiden / tetrominon faller snabbare.
    private void DecreaseFalltime()
    {
        if (decreaseFalltime && falltime > 0.1f)
        {
            decreaseFalltime = false;

            falltime = falltime * 0.9f;
            Debug.Log(falltime);
        }
    }

    // Kollar om positionen �r tom eller inte.
    private bool ValidPosition()
    {
        foreach (Transform children in transform)
        {
            //Avrundar positionen till ett heltal f�r att vi ska anv�nda en 2D-array.
            int posX = Mathf.RoundToInt(children.transform.position.x);
            int posY = Mathf.RoundToInt(children.transform.position.y);

            //Kollar om positionen �r inom spelplanen.
            if (posX < 0 || posX >= Game.gridWidth || posY < 0 || posY >= Game.gridHeight)
            {
                return false;
            }

            //Om positionen �r upptagen.
            if (Game.grid[posX, posY] != null)
            {
                return false;
            }
        }
        return true;
    }
}