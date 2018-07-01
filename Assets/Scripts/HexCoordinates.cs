/**
 * Convert coordinate systems.
 */
using UnityEngine;

// This is so that Unity will save settings we put in Inspector,
// and reload even if we change the script.
[System.Serializable]
public struct HexCoordinates {
    
    public int X { get; private set; }
    public int Z { get; private set; }

    public HexCoordinates (int x, int z) {
        X = x;
        Z = z;
    }

    // Convert offset coords to hex coords.
    public static HexCoordinates FromOffsetCoordinates (int x, int z) {
        return new HexCoordinates(x, z);
    }

	public override string ToString() {
        return "(" + X.ToString() + ", " + Z.ToString() + ")";
	}

    public string ToStringOnSeparateLines() {
        return X.ToString() + "\n" + Z.ToString();
    }
}
