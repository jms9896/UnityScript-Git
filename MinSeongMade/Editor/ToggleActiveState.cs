using UnityEngine;
using UnityEditor;

public class ToggleActiveState : MonoBehaviour
{
    [MenuItem("Tools/Toggle Active State %t")] // %t는 Ctrl+T (Windows) 또는 Cmd+T (Mac)를 의미
    static void ToggleActive()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
}