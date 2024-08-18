using System.Collections.Generic;
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
            CurrentScalingObject.ChangeScale(CurrentLabel.LabelSettings);
            
            CurrentLabel.gameObject.SetActive(false);
            CurrentLabel = null;
        }

    }
}
