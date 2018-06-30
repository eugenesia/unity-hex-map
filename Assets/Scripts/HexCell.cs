﻿using UnityEngine;

public class HexCell : MonoBehaviour {

	//public HexCoordinates coordinates;

	public Color color;

	private void Awake()
	{
        Mesh m = new Mesh();
        GetComponent<MeshFilter>().mesh = m;

        Vector3[] vertices = {
            new Vector3(1, 0, 1),
            new Vector3(1, 0, -1),
            new Vector3(-1, 0, -1),
            new Vector3(1, 0, 1),
        };
        m.vertices = vertices;
	}
}