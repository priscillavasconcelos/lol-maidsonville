using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudView<T> : MonoBehaviour where T : Component
{
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance != null) {
            string typename = typeof(T).Name;
            Debug.LogWarning($"More that one instance of {typename} found.");
        }
        Instance = this as T;
    }
}
