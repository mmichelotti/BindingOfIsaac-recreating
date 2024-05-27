using UnityEngine;

[CreateAssetMenu(fileName = "Grid", menuName = "ScriptableObjects/Grid", order = 1)]
public class MazeGrid : ScriptableObject
{
    [field: SerializeField]
    public int Length { get; set; } = 11;

    [field: SerializeField]
    public Float2 Size { get; set; } = new(20, 10);

    public Int2 GetCoordinatesAt(Directions dir)
    {
        Float2 vector = dir.DirectionToMatrix() * (Length - 1);
        return new((int)vector.x, (int)vector.y);
    }

    public Vector3 CoordinateToPosition(Int2 coord) => new(GetHalfPoint(Size.x, coord.x), GetHalfPoint(Size.y, coord.y));

    private float GetHalfPoint(float tileDimension, int gridIndex) => tileDimension * (gridIndex - Length / 2);

    public bool IsWithinGrid(Int2 position) => position.x >= 0 && position.y >= 0 && position.x < Length && position.y < Length;

}
