using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Denna array inneh�ller alla prefabs f�r tetrominosarna. Se unitys inspector. 
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

            //Spawnar en ny tetromino, ett index fr�n tetrominoarrayen slumpas fram.
            nextTetromino = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);

            //Slumpar fram n�sta tetromino.
            previewTetromino = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], previewTetrominoPosition, Quaternion.identity);
            //Deaktiverar tetrominon eftersom att vi vill bara kunna se den, inte spela med den.
            previewTetromino.GetComponent<Tetromino>().enabled = false;
        }
        // Sker i resten av spelet.
        else
        {
            //Flyttar f�rhandsgransknings-tetrominon till spawnpositionen.
            previewTetromino.transform.localPosition = transform.position;

            //S�tter f�rhandsgransknings-tetrominon till den nuvarande tetrominon.
            nextTetromino = previewTetromino;
            //Aktiverar tetrominon s� att vi kan spela med den.
            nextTetromino.GetComponent<Tetromino>().enabled = true;

            //Slumpar fram n�sta tetromino.
            previewTetromino = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], previewTetrominoPosition, Quaternion.identity);
            //Deaktiverar tetrominon eftersom att vi vill bara kunna se den, inte spela med den.
            previewTetromino.GetComponent<Tetromino>().enabled = false;
        }
    }
}