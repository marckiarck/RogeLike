using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class RoomEditor : EditorWindow
{
    [MenuItem("Window/Room Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(RoomEditor));
    }

    string path = "Assets/Resources/Data";
          

    RoomList roomList;
    EditorGizmo editorGizmo;

    List<RoomList> scriptables = new List<RoomList>();
    int soIndex;

    private void OnEnable()
    {
        LoadInit(); 
    }

    void LoadInit()
    {
        editorGizmo = FindObjectOfType<EditorGizmo>();
        tilemap = FindObjectsOfType<Tilemap>();

        LoadScriptables();

        foreach (Tilemap map in tilemap)
        {
            map.ClearAllTiles();
        }
        //if (tilemap != null)
        //{
        //    tilemap.ClearAllTiles();            
        //}

        if (editorGizmo != null)
        {
            editorGizmo.Size = roomList.size;            
        }
    }


    //    M E T O D O S   C A R G A R   -   C R E A R   -   B O R R A R    S C R I P T A B L E   O B J E C T S

    void LoadScriptables()
    {
        string[] guids;
        guids = AssetDatabase.FindAssets("t:RoomList");
        scriptables = new List<RoomList>();
        foreach (string guid in guids)
        {
            string guidpath=AssetDatabase.GUIDToAssetPath(guid);
            scriptables.Add(AssetDatabase.LoadAssetAtPath(guidpath, typeof(RoomList)) as RoomList);
        }

        if (scriptables.Count == 0)
        {
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RoomList>(), path + "/RoomList_Example.asset");
            AssetDatabase.SaveAssets();
            scriptables.Add(AssetDatabase.LoadAssetAtPath(path + "/RoomList_Example.asset", typeof(RoomList)) as RoomList);
            scriptables[0].size = 10;
        }
        
        roomList = scriptables[0];
    }

    void NewScriptable(int size, string name)
    {
        soIndex = scriptables.Count; //Para que en el desplegable aparezca la nueva lista creada
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RoomList>(), path + "/RoomList_"+name+".asset");
        
        scriptables.Add(AssetDatabase.LoadAssetAtPath(path + "/RoomList_" + name + ".asset", typeof(RoomList)) as RoomList);

        roomList = scriptables[soIndex];
        roomList.size = size;

        EditorUtility.SetDirty(roomList);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void DeleteScriptable() 
    {
        scriptables.Remove(roomList);
        AssetDatabase.DeleteAsset(path+"/"+roomList.name+ ".asset");
        if (scriptables.Count>0)
        { roomList = scriptables[0]; }
        soIndex = 0;
        currentRoom = null;

        foreach (Tilemap map in tilemap)
        {
            map.ClearAllTiles();
        }
    }



    
    //    V A R I A B L E S

    int roomType;
    int subtype0;
    int subtype1;
    int subtype2;
    int subtype3;

    string[] toolbarTypes = { "Corridor", "Intersection", "Entry", "Exit" };
    string[] corridorTypes = { "Left-Right", "Up-Down", "Left-Up", "Left-Down","Right-Up","Right-Down" };
    string[] interTypes = { "Left-Right-Up", "Left-Right-Down", "Left-Up-Down", "Right-Up-Down" };
    string[] entryTypes = { "Left", "Right", "Up", "Down" };

    GUIStyle titleStyle = new GUIStyle();
    

    List<Room> currentList;
    Room currentRoom;
    Tilemap[] tilemap;

    static int newSize;
    static string newName;
    

    //          I N T E R F A Z   D E   L A   H E R R A M I E N T A

    private void OnGUI()
    {
        titleStyle.fontSize = 13;
        titleStyle.alignment = TextAnchor.MiddleCenter;

        //--------------------------------------------------------------------------------------

        GUILayout.Space(10);
        GUILayout.Label("CREATE NEW ROOM LIST", titleStyle);
        GUILayout.Space(10);

        newSize = EditorGUILayout.IntField("Size", newSize);
        newName = EditorGUILayout.TextField("Name", newName);
        GUILayout.Space(10);

        if (GUILayout.Button("New List ", GUILayout.Width(150)))
        {
            NewScriptable(newSize, newName);           
        }


        //---------------------------------------------------------------------------------------------------------------

        GUILayout.Space(20);
        GUILayout.Label("EDIT ROOM LIST", titleStyle);
        GUILayout.Space(10);
        //--------------------------------------------------------------------------->COMPROBAR SI ESTÁN LOS ELEMENTOS
        if (tilemap.Length <0 || editorGizmo==null)
        {
            GUILayout.Label("To use the Room Editor you must go to RoomEditor Scene.");
            GUILayout.Space(10);
            if (GUILayout.Button("Refresh ", GUILayout.Width(150)))
            {
                LoadInit();
            }
        }
        else if (scriptables.Count > 0)
        {
            //--------------------------------------------------------------------------->ELEGIR EL SCRIPTABLE
            string[] namesArray = new string[scriptables.Count];
            for (int i = 0; i < scriptables.Count; i++)
            {
                namesArray[i] = scriptables[i].name;
            }

            soIndex = EditorGUILayout.Popup(soIndex, namesArray);
            roomList = scriptables[soIndex];
            editorGizmo.Size = roomList.size;

            GUILayout.Space(10);


            EditorGUILayout.LabelField("Name:", roomList.name);
            EditorGUILayout.LabelField("Size:", roomList.size.ToString());

            GUILayout.Space(10);

            //-------------------------------------------------------------------------->BOTONES HABITACIONES

            roomType = GUILayout.Toolbar(roomType, toolbarTypes);

            switch (roomType)
            {
                case 0:
                    subtype0 = GUILayout.Toolbar(subtype0, corridorTypes);
                    switch (subtype0)
                    {
                        case 0:
                            currentList = roomList.corridor.typeLR;
                            editorGizmo.ShowGizmos(true,true,false,false);
                            break;
                        case 1:
 //                     ...
                            currentList = roomList.corridor.typeUD;
                            editorGizmo.ShowGizmos(false, false,true, true);
                            break;
                        case 2:
                            currentList = roomList.corridor.typeLU;
                            editorGizmo.ShowGizmos(true, false, true, false);
                            break;
                        case 3:
                            currentList = roomList.corridor.typeLD;
                            editorGizmo.ShowGizmos(true, false, false, true);
                            break;
                        case 4:
                            currentList = roomList.corridor.typeRU;
                            editorGizmo.ShowGizmos(false, true, true, false);
                            break;
                        case 5:
                            currentList = roomList.corridor.typeRD;
                            editorGizmo.ShowGizmos(false, true, false, true);
                            break;
                    }
                    break;

                case 1:
                    subtype1 = GUILayout.Toolbar(subtype1, interTypes);
                    switch (subtype1)
                    {
                        case 0:
                            currentList = roomList.intersection.typeLRU;
                            editorGizmo.ShowGizmos(true, true, true, false);
                            break;
                        case 1:
                            currentList = roomList.intersection.typeLRD;
                            editorGizmo.ShowGizmos(true, true, false, true);
                            break;
                        case 2:
                            currentList = roomList.intersection.typeLUD;
                            editorGizmo.ShowGizmos(true, false, true, true);
                            break;
                        case 3:
                            currentList = roomList.intersection.typeRUD;
                            editorGizmo.ShowGizmos(false, true, true, true);
                            break;
                    }
                    break;

                case 2:
                    subtype2 = GUILayout.Toolbar(subtype2, entryTypes);
                    switch (subtype2)
                    {
                        case 0:
                            currentList = roomList.entry.typeL;
                            editorGizmo.ShowGizmos(true, false, false, false);
                            break;
                        case 1:
                            currentList = roomList.entry.typeR;
                            editorGizmo.ShowGizmos(false, true, false, false);
                            break;
                        case 2:
                            currentList = roomList.entry.typeU;
                            editorGizmo.ShowGizmos(false, false, true, false);
                            break;
                        case 3:
                            currentList = roomList.entry.typeD;
                            editorGizmo.ShowGizmos(false, false, false, true);
                            break;
                    }
                    break;

                case 3:
                    subtype3 = GUILayout.Toolbar(subtype3, entryTypes);
                    switch (subtype3)
                    {
                        case 0:
                            currentList = roomList.exit.typeL;
                            editorGizmo.ShowGizmos(true, false, false, false);
                            break;
                        case 1:
                            currentList = roomList.exit.typeR;
                            editorGizmo.ShowGizmos(false, true, false, false);
                            break;
                        case 2:
                            currentList = roomList.exit.typeU;
                            editorGizmo.ShowGizmos(false, false, true, false);
                            break;
                        case 3:
                            currentList = roomList.exit.typeD;
                            editorGizmo.ShowGizmos(false, false, false, true);
                            break;
                    }
                    break;
            }
            GUILayout.Space(10);

            int r = 0;
            float usedWidth = 0;
            GUILayout.BeginHorizontal();
            foreach (Room item in currentList)
            {

                if (currentRoom == currentList[r])
                {
                    GUI.backgroundColor = Color.grey;
                    if (GUILayout.Button("Room " + (r + 1), GUILayout.Width(100), GUILayout.Height(100)))
                    {
                        LoadRoom(r);
                    }
                    GUI.backgroundColor = Color.white;
                }
                else
                {
                    if (GUILayout.Button("Room " + (r + 1), GUILayout.Width(100), GUILayout.Height(100)))
                    {
                        LoadRoom(r);
                    }
                }

                r++;
                usedWidth += 100;

                if (usedWidth + 100 > position.width)
                {
                    usedWidth = 0;
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
            }

            if (GUILayout.Button("New room", GUILayout.Width(100), GUILayout.Height(100)))
            {
                CreateRoom();
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(20);

            //----------------------------------------------------------------------->BORRAR / GUARDAR

            if (currentList.Count != 0)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Save room", GUILayout.Width(100), GUILayout.Height(20)))
                {
                    SaveRoom();
                }

                if (GUILayout.Button("Delete room", GUILayout.Width(100), GUILayout.Height(20)))
                {
                    DeleteRoom();
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(20);
            }
            

            if (GUILayout.Button("Delete List "))
            {
                DeleteScriptable();
            }
        }
        else
        {
            string[] namesArray = new string[] {"No Room List Created"};
            EditorGUILayout.Popup(soIndex, namesArray);
        }
    }



    //    M E T O D O S   C R E A R   -   C A R G A R   -   G U A R D A R  -   B O R R A R    H A B I T A C I O N E S

    void DeleteRoom()
    {
        //tilemap.ClearAllTiles();
        foreach (Tilemap map in tilemap)
        {
            map.ClearAllTiles();
        }
        currentList.Remove(currentRoom);

        EditorUtility.SetDirty(roomList);
    }

    void CreateRoom()
    {
        //tilemap.ClearAllTiles();
        foreach (Tilemap map in tilemap)
        {
            map.ClearAllTiles();
        }

        currentRoom = new Room(roomList.size, tilemap.Length);
        currentList.Add(currentRoom);

        EditorUtility.SetDirty(roomList);
    }

    void LoadRoom(int roomIndex)
    {
        if (currentRoom!= currentList[roomIndex])
        {
            currentRoom = currentList[roomIndex];
            //tilemap.ClearAllTiles();
            for (int t = 0; t<tilemap.Length; t++)
            {
                tilemap[t].ClearAllTiles();
                for (int x = 0; x < roomList.size; x++)
                {
                    for (int y = 0; y < roomList.size; y++)
                    {
                        TileBase tile = currentRoom.GetTile(x, y, t);
                        if (tile != null)
                        {
                            tilemap[t].SetTile(new Vector3Int(x, y, 0), tile); //currentRoom.GetTile(x, y, t)
                        }
                    }
                }

            }
            
        }
    }

    void SaveRoom() 
    {
        currentRoom.ClearAllTiles();

        for (int t = 0; t < tilemap.Length; t++)
        {
            for (int x = 0; x < roomList.size; x++)
            {
                for (int y = 0; y < roomList.size; y++)
                {
                    TileBase tile = tilemap[t].GetTile(new Vector3Int(x, y, 0));

                    if (tile != null)
                        currentRoom.SetTile(x, y, t, tile);
                }
            }
        }
            
        EditorUtility.SetDirty(roomList);
    }    
}
