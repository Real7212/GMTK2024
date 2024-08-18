using UnityEngine;


[CreateAssetMenu(fileName = "New Label Settings", menuName = "Label Settings")]
public class LabelSO : ScriptableObject
{
    [SerializeField] private Vector2 _newScale;
    [SerializeField] private Size _size;

    public Vector2 NewScale => _newScale;
    public Size Size => _size;
}
