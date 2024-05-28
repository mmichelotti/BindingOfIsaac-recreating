using UnityEngine;

[CreateAssetMenu(fileName = "Grid", menuName = "ScriptableObjects/Grid", order = 1)]
public class MazeGrid : ScriptableObject
{
    #region properties
    [field: SerializeField] public int Length { get; set; } = 11;
    [field: SerializeField] public Float2 Size { get; set; } = new(20, 10);
    #endregion

    #region methods
    public Float2 GetCoordinatesAt(Directions dir) => dir.DirectionToMatrix() * (Length - 1);
    public Vector3 CoordinateToPosition(Float2 coord) => new(GetHalfPoint(Size.x, (int)coord.x), GetHalfPoint(Size.y, (int)coord.y));
    private float GetHalfPoint(float tileDimension, int gridIndex) => tileDimension * (gridIndex - Length / 2);
    public bool IsWithinGrid(Float2 position) => position.x >= 0 && position.y >= 0 && position.x < Length && position.y < Length;
    #endregion
}
