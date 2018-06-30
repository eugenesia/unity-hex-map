using UnityEngine;

public class HexCell : MonoBehaviour {

	//public HexCoordinates coordinates;

	public Color color;
    public Vector3[] vertices;
    public Vector2[] uv;
    public int[] triangles;

	private void Awake()
	{
        // Initialise new mesh
        // https://docs.unity3d.com/ScriptReference/Mesh.html
        Mesh m = new Mesh();
        GetComponent<MeshFilter>().mesh = m;
        m.vertices = vertices;
        m.uv = uv;
        m.triangles = triangles;
	}
}