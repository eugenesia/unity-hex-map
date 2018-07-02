/**
 * Axial coordinates for a cell, and useful functions for converting
 * from offset coordinates.
 */
using UnityEngine;

// This is so that Unity will save settings we put in Inspector,
// and reload even if we change the script.
[System.Serializable]
public struct HexCoordinates {
    
    public int X { get; private set; }
    public int Z { get; private set; }

    // Y coord is redundant as X + Y + Z = 0
    // Don't store it but calculate it on demand.
    public int Y {
        get {
            return -X - Z;
        }
    }

    public HexCoordinates (int x, int z) {
        X = x;
        Z = z;
    }

    // Convert offset coords to hex coords.
    public static HexCoordinates FromOffsetCoordinates (int x, int z) {
        // x: undo horizontal offset in offset coords.
        return new HexCoordinates(x - z / 2, z);
    }

	public override string ToString() {
        return "(" +
            X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
	}

    public string ToStringOnSeparateLines() {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }
}
