using UnityEngine;
using UnityEngine.UI;

public class WheelSelection : MonoBehaviour
{
    public GameObject squarePrefab; // Prefab for the square UI element
    public int numberOfSquares = 8; // Number of squares in the circle
    public float radius = 200f; // Radius of the circular arrangement
    public float rotationSpeed = 10f; // Speed at which the selection rotates
    public Text selectedText; // UI Text to display selected item (optional)

    private GameObject[] squares;
    private int currentSelection = 0; // Index of the selected square

    void Start()
    {
        // Instantiate squares in a circle
        squares = new GameObject[numberOfSquares];
        for (int i = 0; i < numberOfSquares; i++)
        {
            squares[i] = Instantiate(squarePrefab, transform);
            float angle = i * Mathf.PI * 2 / numberOfSquares;
            Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            squares[i].transform.localPosition = position;
        }
        UpdateSelection();
    }

    void Update()
    {
        // Handle input to rotate the wheel
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotateSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotateSelection(1);
        }
    }

    void RotateSelection(int direction)
    {
        // Update the current selection index
        currentSelection += direction;

        // Ensure the index wraps around when out of bounds
        if (currentSelection < 0)
            currentSelection = numberOfSquares - 1;
        else if (currentSelection >= numberOfSquares)
            currentSelection = 0;

        // Rotate the square positions to simulate wheel rotation
        float angle = currentSelection * Mathf.PI * 2 / numberOfSquares;
        for (int i = 0; i < numberOfSquares; i++)
        {
            float targetAngle = (i - currentSelection) * Mathf.PI * 2 / numberOfSquares;
            Vector3 targetPosition = new Vector3(Mathf.Cos(targetAngle), Mathf.Sin(targetAngle), 0) * radius;
            squares[i].transform.localPosition = Vector3.Lerp(squares[i].transform.localPosition, targetPosition, Time.deltaTime * rotationSpeed);
        }

        UpdateSelection();
    }

    void UpdateSelection()
    {
        // Optionally, update a UI text to show the selected square
        if (selectedText != null)
        {
            selectedText.text = "Selected: Square " + (currentSelection + 1);
        }
    }
}
