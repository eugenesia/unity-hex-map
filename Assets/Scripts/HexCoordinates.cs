/**
 * Axial coordinates for a cell, and useful functions for converting
 * from offset coordinates.
 */
using UnityEngine;

// This is so that Unity will save settings we put in Inspector,
// and reload even if we change the script.
[System.Serializable]
public struct HexCoordinates {

    // Force serialization of a private field, so we can see in Inspector in
    // play mode.
    [SerializeField]
    private int x, z;
    
    public int X {
        get {
            return x;
        }
    }
    public int Z {
        get {
            return z;
        }
    }

    // Y coord is redundant as X + Y + Z = 0
    // Don't store it but calculate it on demand.
    public int Y {
        get {
            return -X - Z;
        }
    }

    public HexCoordinates (int x, int z) {
        this.x = x;
        this.z = z;
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

    // Convert touch position to hex coords so we know which cell was touched.
    public static HexCoordinates FromPosition(Vector3 position) {

        // Get hex coords if we were on bottom row (z = 0).
        float x = position.x / (HexMetrics.innerRadius * 2f);
        float y = -x;

        // Every 2 rows we move up, x and y decrease by one.
        // outerRadius x 3 == height of 2 rows
        float offset = position.z / (HexMetrics.outerRadius * 3f);
        x -= offset;
        y -= offset;

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

        // Rounding error - see which coord got rounded the most, it's probably
        // the culprit.
        if (iX + iY + iZ != 0) {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x -y -iZ);

            // X got rounded the most, let's get X another way.
            if (dX > dY && dX > dZ) {
                iX = -iY - iZ;
            }
            // Z got rounded the most.
            else if (dZ > dY) {
                iZ = -iX - iY;
            }
        }

        return new HexCoordinates(iX, iZ);
    }
}
