using UnityEngine;

[CreateAssetMenu(fileName = "Grid", menuName = "ScriptableObjects/Grid", order = 1)]
public class MazeGrid : ScriptableObject
{
    [field: SerializeField]
    public int Length { get; set; } = 11;

    [field: SerializeField]
    public Vector2 Size { get; set; } = new(20, 10);

    public Vector2Int GetCoordinatesAt(Directions dir)
    {
        Vector2 vector = dir.DirectionToMatrix() * (Length - 1);
        return new((int)vector.x, (int)vector.y);
    }

    public Vector3 CoordinateToPosition(Vector2Int coord) => new(GetHalfPoint(Size.x, coord.x), GetHalfPoint(Size.y, coord.y));

    private float GetHalfPoint(float tileDimension, int gridIndex) => tileDimension * (gridIndex - Length / 2);

    public bool IsWithinGrid(Vector2Int position) => position.x >= 0 && position.y >= 0 && position.x < Length && position.y < Length;

}
