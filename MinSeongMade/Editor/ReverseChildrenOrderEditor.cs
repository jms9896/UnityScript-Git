using UnityEditor;
using UnityEngine;

// *** how to use ***
// 부모 오브젝트를 선택하고, Tools/Reverse Children Order를 선택합니다.
// 해당 부모 오브젝트 산하의 자식 오브젝트들의 순서가 모두 역순이 됩니다. 

public class ReverseChildrenOrderEditor : EditorWindow
{
    [MenuItem("Tools/Reverse Children Order")]
    public static void ShowWindow()
    {
        GetWindow<ReverseChildrenOrderEditor>("Reverse Children Order");
    }

    private void OnGUI()
    {
        GUILayout.Label("Reverse Children Order", EditorStyles.boldLabel);
        if (GUILayout.Button("Reverse Selected Object's Children Order"))
        {
            ReverseChildrenOrder();
        }
    }

    private void ReverseChildrenOrder()
    {
        GameObject parentObject = Selection.activeGameObject;
        
        if (parentObject == null)
        {
            Debug.LogWarning("No object selected. Please select a parent object in the hierarchy.");
            return;
        }

        int childCount = parentObject.transform.childCount;
        if (childCount < 2)
        {
            Debug.LogWarning("The selected object must have at least two children to reverse their order.");
            return;
        }

        // Reverse the sibling index of each child
        for (int i = 0; i < childCount / 2; i++)
        {
            Transform child1 = parentObject.transform.GetChild(i);
            Transform child2 = parentObject.transform.GetChild(childCount - 1 - i);
            int index1 = child1.GetSiblingIndex();
            int index2 = child2.GetSiblingIndex();
            
            child1.SetSiblingIndex(index2);
            child2.SetSiblingIndex(index1);
        }
        
        Debug.Log("Children order reversed for " + parentObject.name);
    }
}