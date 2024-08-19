using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LabelSystem : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public static LabelSystem Instance {get; private set;}
    public Label CurrentLabel;
    public ScalingObject CurrentScalingObject;

    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(Instance);
    }

    private void Update() {
        

        if(Input.GetMouseButtonUp(0)) {
            if(CurrentScalingObject == null) return;

            if(CurrentLabel.LabelSettings.Type == LabelType.Scale) {
                CurrentScalingObject.ChangeScale(CurrentLabel.LabelSettings);
            
            } else {
                if(!CurrentScalingObject.IsShapeActive(CurrentLabel.LabelSettings.Shape)) return;

                CurrentScalingObject.ChangeShape(CurrentLabel.LabelSettings);
            }

            
            CurrentLabel.gameObject.SetActive(false);
            CurrentLabel = null;
        }

    }
}
