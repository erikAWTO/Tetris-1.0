using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private float fall = 0f;
    private float fallSpeed = 0.8f;

    public Vector3 rotationPoint;

    // Update is called once per frame
    private void Update()
    {
        CheckInput();
    }

    // Tar input från användaren.
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
            //Rotera runt punkten som vi har angett. Detta krävs för att offseten på vissa tetrominos inte ska bli fel.
            //Exempelbild på vart vi vill har vår rotationspunkt för de olika "tetrominorna".
            //https://static.wikia.nocookie.net/tetrisconcept/images/3/3d/SRS-pieces.png/revision/latest/scale-to-width-down/336?cb=20060626173148
            //OBS! Rotationspunkten utgår ifrån parent.
            transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, 90f);

            if (!ValidPosition())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, -90f);
            }
        }
        // Vi kollar diffrenensen: (Tid sen spelet startade) - (Föregående tid) om detta överstiger vår fallSpeed(falltid) kommer tetrominon flyttas ned ett steg.
        // Om nedåtpilen är nedtryckt minskar vi fallSpeed(falltiden) och därmed kan vi flytta tetrominon snabbare.
        else if (Time.time - fall > (Input.GetKeyDown(KeyCode.DownArrow) ? fallSpeed / 10 : fallSpeed))
        {
            transform.position += Vector3.down;

            if (!ValidPosition())
            {
                transform.position -= Vector3.down;

                FindObjectOfType<Game>().UpdateGrid(this);

                FindObjectOfType<Game>().DeleteRow();

                if (FindObjectOfType<Game>().CheckIsAboveGrid(this))
                {
                    FindObjectOfType<Game>().GameOver();
                }

                //Slå av nuvarande tetromino och spawna en ny.
                this.enabled = false;
                FindObjectOfType<Spawn>().SpawnTetromino();
            }
            fall = Time.time;
        }
    }

    private void HardDrop()
    {
        if (ValidPosition())
        {
            while (ValidPosition())
            {
                transform.position += Vector3.down;
            }

            if(!ValidPosition())
            {
                transform.position -= Vector3.down;
            }
        }
    }

    private bool ValidPosition()
    {
        foreach (Transform children in transform)
        {
            //Avrundar positionen till ett heltal för att vi ska kunna jobba med en 2D-array.
            int posX = Mathf.RoundToInt(children.transform.position.x);
            int posY = Mathf.RoundToInt(children.transform.position.y);

            if (posX < 0 || posX >= Game.gridWidth || posY < 0 || posY >= Game.gridHeight)
            {
                return false;
            }

            //Om positionen inte är upptagen.
            if (Game.grid[posX, posY] != null)
            {
                return false;
            }
        }
        return true;
    }
}