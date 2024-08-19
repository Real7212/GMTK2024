using UnityEngine;


public enum LabelType {
    Scale,
    Shape
}

public enum ShapeType {
    Circle,
    Square,
    Triangle,
    Normal
}



[CreateAssetMenu(fileName = "New Label Settings", menuName = "Label Settings")]
public class LabelSO : ScriptableObject
{
    [SerializeField] private LabelType _type;

    [Header("Scale")]
    [SerializeField] private Vector2 _newScale;
    [SerializeField] private Size _size;

    [Header("Shape")]
    [SerializeField] private ShapeType _shape;

    public LabelType Type => _type;
    public Vector2 NewScale => _newScale;
    public Size Size => _size;
    public ShapeType Shape => _shape;
}
