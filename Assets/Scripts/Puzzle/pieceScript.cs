using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PieceScript : MonoBehaviour
{
    private Vector3 randomPosition;
    public bool InTruePosition;
    public bool Selected;
    public bool AllPieceInCurrentPosition;

    void Start()
    {
        randomPosition = transform.position;
        transform.position = new Vector3(Random.Range(2f, 7f), Random.Range(2.6f, -3f));
    }

    void Update()
    {
        if (AllPieceInCurrentPosition == false)
        {
            if (Vector3.Distance(transform.position, randomPosition) < 0.5f)
            {
                if (!Selected)
                {
                    if (InTruePosition == false)
                    {
                        transform.position = randomPosition;
                        InTruePosition = true;
                        GetComponent<SortingGroup>().sortingOrder = 0;
                        CheckAllPiecesInPosition(); // Check if all pieces are in the correct position
                    }
                }
            }
        }
    }

    void CheckAllPiecesInPosition()
    {
        // Check if all pieces are in the correct position
        GameObject[] puzzlePieces = GameObject.FindGameObjectsWithTag("Puzzle");
        foreach (GameObject piece in puzzlePieces)
        {
            if (!piece.GetComponent<PieceScript>().InTruePosition)
            {
                return; // If any piece is not in the correct position, exit the method
            }
        }

        // If all pieces are in the correct position, reload the scene
        SceneManager.LoadScene("MapMain");
    }
}