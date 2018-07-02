/**
 * Container and coordinator for grid elements.
 */
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int width = 6;
	public int height = 6;

	public HexCell cellPrefab;

    // Logical data for the cells.
	HexCell[] cells;


    // Label for cell coords.
    public Text cellLabelPrefab;

    // Canvas containing all the text.
    Canvas gridCanvas;

    // Display the cell shapes etc.
    HexMesh hexMesh;

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.magenta;

	void Awake () {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

		cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++) {
			for (int x = 0; x < width; x++) {
				CreateCell(x, z, i++);
			}
		}
	}

    // Create the logical cell, and show some label text on it.
	void CreateCell (int x, int z, int i) {
		Vector3 position;

        // Offset coordinates
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
        // Set the hex coords of the cell.
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.color = defaultColor;

        // Set the text label on the cell.
        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
	}

	void Start() {
        hexMesh.Triangulate(cells);
	}

	void Update() {
        if (Input.GetMouseButton(0)) {
            HandleInput();
        }
	}

    // See which cell was clicked on.
    void HandleInput() {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit)) {
            TouchCell(hit.point);
        }
    }

    void TouchCell(Vector3 position) {
        // Transform position from world space to local space.
        position = transform.InverseTransformPoint(position);
        Debug.Log("Touched position: " + position.ToString());

        // Convert touched position to hex coords.
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);

        Debug.Log("touched at " + coordinates.ToString());

        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.color = touchedColor;
        hexMesh.Triangulate(cells);
    }
}