using UnityEngine;

public class MazeGrid : MonoBehaviour
{
    [field: SerializeField]
    public int Length { get; set; } = 11;

    [field: SerializeField]
    public Vector2 Size { get; set; } = new(20, 10);

    public Vector2Int GetCoordinatesAt(SpawnPosition pos)
    {
        Vector2 vector = DirectionUtility.PositionToCoordinate[pos] * (Length - 1);
        return new((int)vector.x, (int)vector.y);
    }

    public Vector3 CoordinateToPosition(Vector2Int coord) => new(GetHalfPoint(Size.x, coord.x), GetHalfPoint(Size.y, coord.y));

    private float GetHalfPoint(float tileDimension, int gridIndex) => tileDimension * (gridIndex - Length / 2);

    public bool IsWithinGrid(Vector2Int position) => position.x >= 0 && position.y >= 0 && position.x < Length && position.y < Length;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int x = 0; x < Length; x++)
        {
            for (int y = 0; y < Length; y++)
            {
                Vector3 pos = CoordinateToPosition(new Vector2Int(x, y));
                Gizmos.DrawWireCube(pos, new Vector3(Size.x, Size.y, 1));
            }
        }
    }
}
