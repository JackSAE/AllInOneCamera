using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor((typeof(AllInOneCameraController)))]
public class GUIEditorScript : Editor
{

    public bool seeMeOnly()
    {
        AllInOneCameraController.firstPersonCamera = AllInOneCameraController.thirdPersonCamera = AllInOneCameraController.topDownCamera = AllInOneCameraController.mMOCamera = false;
        return true;
    }

    override public void OnInspectorGUI()
    {
        #region  GUI Style for Foldout menus
        GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
        myFoldoutStyle.fontStyle = FontStyle.Bold;
        myFoldoutStyle.fontSize = 12;
        Color myStyleColor = Color.blue;
        myFoldoutStyle.normal.textColor = myStyleColor;
        myFoldoutStyle.onNormal.textColor = myStyleColor;
        myFoldoutStyle.hover.textColor = myStyleColor;
        myFoldoutStyle.onHover.textColor = myStyleColor;
        myFoldoutStyle.focused.textColor = myStyleColor;
        myFoldoutStyle.onFocused.textColor = myStyleColor;
        myFoldoutStyle.active.textColor = myStyleColor;
        myFoldoutStyle.onActive.textColor = myStyleColor;
        #endregion

        var allInOneCameraController = target as AllInOneCameraController;

        #region General Settings
        EditorGUILayout.LabelField("General Settings");

        allInOneCameraController.target = (GameObject)EditorGUILayout.ObjectField("Target:", allInOneCameraController.target, typeof(GameObject), true);
        #endregion


        #region First Person Camera
        AllInOneCameraController.firstPersonCamera = EditorGUILayout.Toggle("First Person Camera", AllInOneCameraController.firstPersonCamera);

        //Checking if the firstperson tick box is checked
        if (AllInOneCameraController.firstPersonCamera)
        {
            //Code For only having 1 tickbox selected at a time
            AllInOneCameraController.firstPersonCamera = seeMeOnly();

            AllInOneCameraController.firstPersonCameraFoldout = EditorGUILayout.Foldout(AllInOneCameraController.firstPersonCameraFoldout, "FirstPersonCamera", myFoldoutStyle);
            if (AllInOneCameraController.firstPersonCameraFoldout)
            {
                //Enter in Variables here to display them in the inspector

                allInOneCameraController.offSetFromPlayerFirstPerson = EditorGUILayout.Vector3Field("Target Offset:", allInOneCameraController.offSetFromPlayerFirstPerson);
                allInOneCameraController.lookSmooth = EditorGUILayout.FloatField("look Smooth: ", allInOneCameraController.lookSmooth);
                allInOneCameraController.rotateVelo = EditorGUILayout.FloatField("rotate Velocity: ", allInOneCameraController.rotateVelo);

            }
        }
        #endregion


        #region Third Person Camera
        AllInOneCameraController.thirdPersonCamera = EditorGUILayout.Toggle("Third Person Camera", AllInOneCameraController.thirdPersonCamera);

        if (AllInOneCameraController.thirdPersonCamera)
        {
            //Code For only having 1 tickbox selected at a time
            AllInOneCameraController.thirdPersonCamera = seeMeOnly();

            AllInOneCameraController.thirdPersonCameraFoldout = EditorGUILayout.Foldout(AllInOneCameraController.thirdPersonCameraFoldout, "ThirdrdPersonCamera", myFoldoutStyle);

            if (AllInOneCameraController.thirdPersonCameraFoldout)
            {
                //Enter in Variables here to display them in the inspector

                allInOneCameraController.xOffset = EditorGUILayout.Slider("X Offset: ", allInOneCameraController.xOffset, -50, 50);
                allInOneCameraController.yOffset = EditorGUILayout.Slider("Y Offset: ", allInOneCameraController.yOffset, -50, 50);
                allInOneCameraController.zOffset = EditorGUILayout.Slider("Z Offset: ", allInOneCameraController.zOffset, -50, 50);
                allInOneCameraController.rotateCamera = EditorGUILayout.Toggle("Rotate Camera", allInOneCameraController.rotateCamera);

                if (allInOneCameraController.rotateCamera == true)
                    allInOneCameraController.rotationSpeed = EditorGUILayout.Slider("rotation Speed: ", allInOneCameraController.rotationSpeed, -50, 50);
                else
                    allInOneCameraController.damping = EditorGUILayout.Slider("Damping Speed: ", allInOneCameraController.damping, -50, 50);


            }
        }
        #endregion


        #region Top Down Camera
        AllInOneCameraController.topDownCamera = EditorGUILayout.Toggle("Top Down Camera", AllInOneCameraController.topDownCamera);

        if (AllInOneCameraController.topDownCamera)
        {
            //Code For only having 1 tickbox selected at a time
            AllInOneCameraController.topDownCamera = seeMeOnly();

            AllInOneCameraController.topDownCameraFoldout = EditorGUILayout.Foldout(AllInOneCameraController.topDownCameraFoldout, "TopDownCamera", myFoldoutStyle);
            if (AllInOneCameraController.topDownCameraFoldout)
            {
                //Enter in Variables here to display them in the inspector

            }
        }
        #endregion


        #region MMO Camera
        AllInOneCameraController.mMOCamera = EditorGUILayout.Toggle("MMO Camera", AllInOneCameraController.mMOCamera);

        if (AllInOneCameraController.mMOCamera)
        {
            //Code For only having 1 tickbox selected at a time
            AllInOneCameraController.mMOCamera = seeMeOnly();

            AllInOneCameraController.mMOCameraFoldout = EditorGUILayout.Foldout(AllInOneCameraController.mMOCameraFoldout, "MMOCamera", myFoldoutStyle);
            if (AllInOneCameraController.mMOCameraFoldout)
            {
                //Enter in Variables here to display them in the inspector
                allInOneCameraController.lookAt = (Transform)EditorGUILayout.ObjectField("Target Transform: ", allInOneCameraController.lookAt, typeof(Transform), true);
            }
        }

        #endregion
    }

}
