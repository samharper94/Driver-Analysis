using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Lines))]
public class LineInspector : Editor {

    private void OnSceneGUI()
    {
        Lines line = target as Lines; //Draws line when OnScreenGUI is called

        Transform handleTransform = line.transform;
        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;
        Vector3 p0 = handleTransform.TransformPoint(line.p0);
        Vector3 p1 = handleTransform.TransformPoint(line.p1);

        Handles.color = Color.white; //line is white
        Handles.DrawLine(p0, p1); //draw line between points p0 and p1 (user set)

        EditorGUI.BeginChangeCheck(); //This block makes the handles work
        p0 = Handles.DoPositionHandle(p0, handleRotation); //put handles on the ends of the lines
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line); //allow undo
            line.p0 = handleTransform.InverseTransformPoint(p0); //Move handles to new point
        }
        EditorGUI.BeginChangeCheck();
        p1 = Handles.DoPositionHandle(p1, handleRotation);
        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line); //allow undo
            line.p1 = handleTransform.InverseTransformPoint(p1);
        }
    }
}
