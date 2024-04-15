using UnityEngine;

public class Grid : MonoBehaviour
{
    [field: SerializeField] public int Length { get; set; } = 11;
    [field: SerializeField] public Vector2 Size { get; set; } = new(20, 10);

    public Vector2Int GetSpawnPosition(SpawnPosition pos)
    {
        Vector2 vector = DirectionUtility.PositionToCoordinate[pos] * (Length - 1);
        return new Vector2Int((int)vector.x, (int)vector.y);
    }

    public Vector3 GetWSPositionAt(Vector2Int gridIndex) => new(GetHalfPoint(Size.x, gridIndex.x), GetHalfPoint(Size.y, gridIndex.y));
    private float GetHalfPoint(float roomDimension, int gridIndex) => roomDimension * (gridIndex - Length / 2);
    public bool IsWithinGrid(Vector2Int position) => position.x >= 0 && position.y >= 0 && position.x < Length && position.y < Length;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int x = 0; x < Length; x++)
        {
            for (int y = 0; y < Length; y++)
            {
                Vector3 pos = GetWSPositionAt(new Vector2Int(x, y));
                Gizmos.DrawWireCube(pos, new Vector3(Size.x, Size.y, 1));
            }
        }
    }
}
