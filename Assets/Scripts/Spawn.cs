using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Denna array innehåller alla prefabs för tetrominosarna. Se unitys inspector. 
    public GameObject[] Tetrominoes;

    private GameObject previewTetromino;
    private GameObject nextTetromino;

    private bool gameStarted = false;

    private Vector3 previewTetrominoPosition = new Vector3(19, 15, 0);

    private void Start()
    {
        SpawnTetromino();
    }

    public void SpawnTetromino()
    {
        // Sker bara vid starten av spelet
        if(!gameStarted)
        {
            gameStarted = true;

            //Spawnar en ny tetromino, ett index från tetrominoarrayen slumpas fram.
            nextTetromino = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);

            //Slumpar fram nästa tetromino.
            previewTetromino = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], previewTetrominoPosition, Quaternion.identity);
            //Deaktiverar tetrominon eftersom att vi vill bara kunna se den, inte spela med den.
            previewTetromino.GetComponent<Tetromino>().enabled = false;
        }
        // Sker i resten av spelet.
        else
        {
            //Flyttar förhandsgransknings-tetrominon till spawnpositionen.
            previewTetromino.transform.localPosition = transform.position;

            //Sätter förhandsgransknings-tetrominon till den nuvarande tetrominon.
            nextTetromino = previewTetromino;
            //Aktiverar tetrominon så att vi kan spela med den.
            nextTetromino.GetComponent<Tetromino>().enabled = true;

            //Slumpar fram nästa tetromino.
            previewTetromino = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], previewTetrominoPosition, Quaternion.identity);
            //Deaktiverar tetrominon eftersom att vi vill bara kunna se den, inte spela med den.
            previewTetromino.GetComponent<Tetromino>().enabled = false;
        }
    }
}