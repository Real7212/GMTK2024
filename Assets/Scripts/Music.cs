using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance {get; private set;}

    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Dead() {
        Destroy(gameObject);
    }
}
