using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int width = 6;
	public int height = 6;

	public HexCell cellPrefab;

	HexCell[] cells;

    //HexMesh hexMesh;

    // Label for cell coords.
    public Text cellLabelPrefab;

    // Canvas containing all the text.
    Canvas gridCanvas;

	void Awake () {
        gridCanvas = GetComponentInChildren<Canvas>();

		cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++) {
            Debug.Log("Z: " + z);
			for (int x = 0; x < width; x++) {
				CreateCell(x, z, i++);
			}
		}
	}


	void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = x * 10f;
		position.y = 0f;
		position.z = z * 10f;

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;

        // Set the text label on the cell.
        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = x.ToString() + "\n" + z.ToString();
	}
}