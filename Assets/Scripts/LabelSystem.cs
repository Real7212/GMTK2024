using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LabelSystem : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public static LabelSystem Instance {get; private set;}
    public Label CurrentLabel;
    public ScalingObject CurrentScalingObject;

    
    private Stack<(ScalingObject, Vector2, Label)> _labelsToUndo = new();


    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(Instance);

        DontDestroyOnLoad(Instance);
        Time.timeScale = 0f;
    }

    private void Update() {
        

        if(Input.GetMouseButtonUp(0)) {
            if(CurrentScalingObject == null) return;
            Vector2 previousScale = CurrentScalingObject.Scale;
            CurrentScalingObject.ChangeScale(CurrentLabel.LabelSettings.NewScale);
            CurrentLabel.gameObject.SetActive(false);
            _labelsToUndo.Push((CurrentScalingObject, previousScale, CurrentLabel));
            CurrentLabel = null;

            if(Time.timeScale == 0f) Time.timeScale = 1f;
        }

        if(Input.GetKeyDown(KeyCode.Z)) {
            var val = _labelsToUndo.Pop();
            val.Item1.ChangeScale(val.Item2);
            val.Item3.gameObject.SetActive(true);
        }
        
    }
}
