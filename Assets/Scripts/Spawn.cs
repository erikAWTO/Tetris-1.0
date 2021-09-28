using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] Tetrominoes;

    private void Start()
    {
        SpawnTetromino();
    }

    public void SpawnTetromino()
    {
        //Spawnar en ny tetromino, ett index fr�n tetrominolistan slumpas fram.
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
    }
}