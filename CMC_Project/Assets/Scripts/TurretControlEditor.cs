using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(TurretControl))]

public class TurretControlEditor : Editor {
	
	enum displayFieldType {DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields}
	displayFieldType DisplayFieldType;
	
	TurretControl t;
	SerializedObject GetTarget;
	SerializedProperty ThisList;
	int ListSize;
	
	void OnEnable(){
		t = (TurretControl)target;
		GetTarget = new SerializedObject(t);
		ThisList = GetTarget.FindProperty("hardPoints"); // Find the List in our script and create a refrence of it
	}
	
	public override void OnInspectorGUI(){
		//Update our list
		
		GetTarget.Update();
		
		//Choose how to display the list<> Example purposes only
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		DisplayFieldType = (displayFieldType)EditorGUILayout.EnumPopup("",DisplayFieldType);
		
		//Resize our list
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		EditorGUILayout.LabelField("Define the list size with a number");
		ListSize = ThisList.arraySize;
		ListSize = EditorGUILayout.IntField ("List Size", ListSize);
		
		if(ListSize != ThisList.arraySize){
			while(ListSize > ThisList.arraySize){
				ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
			}
			while(ListSize < ThisList.arraySize){
				ThisList.DeleteArrayElementAtIndex(ThisList.arraySize - 1);
			}
		}

		EditorGUILayout.LabelField("Or");

		//Or add a new item to the List<> with a button
		EditorGUILayout.LabelField("Add a new item with a button");
		
		if(GUILayout.Button("Add New")){
			t.hardPoints.Add(new TurretControl.HardPoint(new Vector2(0f, 0f), 0f));
		}
		
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		
		//Display our list to the inspector window
		
		for(int i = 0; i < ThisList.arraySize; i++){
			SerializedProperty hardPointsRef = ThisList.GetArrayElementAtIndex(i);
			SerializedProperty MyVect2 = hardPointsRef.FindPropertyRelative("Location");
			SerializedProperty MyAngle = hardPointsRef.FindPropertyRelative("Angle");

//			SerializedProperty MyInt = hardPointsRef.FindPropertyRelative("AnInt");
//			SerializedProperty MyFloat = hardPointsRef.FindPropertyRelative("AnFloat");
//			SerializedProperty MyVect3 = hardPointsRef.FindPropertyRelative("AnVector3");
//			SerializedProperty MyGO = hardPointsRef.FindPropertyRelative("AnGO");
//			SerializedProperty MyArray = hardPointsRef.FindPropertyRelative("AnIntArray");
			
			
			// Display the property fields in two ways.
			
			if(DisplayFieldType == 0){// Choose to display automatic or custom field types. This is only for example to help display automatic and custom fields.
				//1. Automatic, No customization <-- Choose me I'm automatic and easy to setup
				EditorGUILayout.LabelField("Automatic Field By Property Type");
				EditorGUILayout.PropertyField(MyVect2);
				EditorGUILayout.PropertyField(MyAngle);

//				EditorGUILayout.PropertyField(MyGO);
//				EditorGUILayout.PropertyField(MyInt);
//				EditorGUILayout.PropertyField(MyFloat);
//				EditorGUILayout.PropertyField(MyVect3);
				
				// Array fields with remove at index
				EditorGUILayout.Space ();
				EditorGUILayout.Space ();
				EditorGUILayout.LabelField("Array Fields");
				
//				if(GUILayout.Button("Add New Index",GUILayout.MaxWidth(130),GUILayout.MaxHeight(20))){
//					MyArray.InsertArrayElementAtIndex(MyArray.arraySize);
//					MyArray.GetArrayElementAtIndex(MyArray.arraySize -1).intValue = 0;
//				}
				
//				for(int a = 0; a < MyArray.arraySize; a++){
//					EditorGUILayout.PropertyField(MyArray.GetArrayElementAtIndex(a));
//					if(GUILayout.Button("Remove  (" + a.ToString() + ")",GUILayout.MaxWidth(100),GUILayout.MaxHeight(15))){
//						MyArray.DeleteArrayElementAtIndex(a);
//					}
//				}
			}else{
				//Or
				
				//2 : Full custom GUI Layout <-- Choose me I can be fully customized with GUI options.
				EditorGUILayout.LabelField("Customizable Field With GUI");
				MyVect2.vector2Value = EditorGUILayout.Vector2Field("HardPoint Position (X,Y)", MyVect2.vector2Value);
				MyAngle.floatValue = EditorGUILayout.FloatField("Rotation Angle (Up=0)",MyAngle.floatValue);

//				MyGO.objectReferenceValue = EditorGUILayout.ObjectField("My Custom Go", MyGO.objectReferenceValue, typeof(GameObject), true);
//				MyInt.intValue = EditorGUILayout.IntField("My Custom Int",MyInt.intValue);
//				MyFloat.floatValue = EditorGUILayout.FloatField("My Custom Float",MyFloat.floatValue);
//				MyVect3.vector3Value = EditorGUILayout.Vector3Field("My Custom Vector 3",MyVect3.vector3Value);
				
				
				// Array fields with remove at index
				EditorGUILayout.Space ();
				EditorGUILayout.Space ();
				EditorGUILayout.LabelField("Array Fields");
				
//				if(GUILayout.Button("Add New Index",GUILayout.MaxWidth(130),GUILayout.MaxHeight(20))){
//					MyArray.InsertArrayElementAtIndex(MyArray.arraySize);
//					MyArray.GetArrayElementAtIndex(MyArray.arraySize -1).intValue = 0;
//				}
//				
//				for(int a = 0; a < MyArray.arraySize; a++){
//					EditorGUILayout.BeginHorizontal();
//					EditorGUILayout.LabelField("My Custom Int (" + a.ToString() + ")",GUILayout.MaxWidth(120));
//					MyArray.GetArrayElementAtIndex(a).intValue = EditorGUILayout.IntField("",MyArray.GetArrayElementAtIndex(a).intValue, GUILayout.MaxWidth(100));
//					if(GUILayout.Button("-",GUILayout.MaxWidth(15),GUILayout.MaxHeight(15))){
//						MyArray.DeleteArrayElementAtIndex(a);
//					}
//					EditorGUILayout.EndHorizontal();
//				}
			}
			
			EditorGUILayout.Space ();
			
			//Remove this index from the List
			EditorGUILayout.LabelField("Remove an index from the List<> with a button");
			if(GUILayout.Button("Remove This Index (" + i.ToString() + ")")){
				ThisList.DeleteArrayElementAtIndex(i);
			}
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
		}
		
		//Apply the changes to our list
		GetTarget.ApplyModifiedProperties();
	}
}