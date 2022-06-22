using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;




public class LevelGenerator : MonoBehaviour
{
    [SerializeField] RoomList roomList;
    Tilemap[] tilemap;

    [SerializeField] PrefabPair[] mapAux;
    Dictionary<TileBase, GameObject> objectsMap;

    GridUnit [,] grid;
    List<GridUnit> unitsFilled = new List<GridUnit>();

    List<GridUnit> endUnits = new List<GridUnit>();

    [SerializeField] int columns = 10;
    [SerializeField] int rows = 10;

    [SerializeField] Vector2Int startPosition;

    [SerializeField] int endRooms;


    private void Awake()
    {
        grid = new GridUnit[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                grid[i,j]= new GridUnit(i,j, roomList.size);
            }
        }

        // INICIALIZACION DEL MAPA
        objectsMap = new Dictionary<TileBase, GameObject>();
        for (int i = 0; i<mapAux.Length; i++)
        {
            if (!objectsMap.ContainsKey(mapAux[i].tile))
            {
                objectsMap.Add(mapAux[i].tile, mapAux[i].prefab);
            }
        }
    }
       
   
    void Start()
    {
        tilemap = FindObjectsOfType<Tilemap>();

        //Clampea los valores en caso de que se hayan introducido posiciones que queden fuera de la grid
        startPosition.x = Mathf.Clamp(startPosition.x, 0, columns - 1);
        startPosition.y = Mathf.Clamp(startPosition.y, 0, rows - 1);

        unitsFilled.Add(grid[startPosition.x, startPosition.y]);
        grid[startPosition.x, startPosition.y].roomType = RoomType.start;

        GenerateEnds();
        GeneratePaths();

        //Al final del todo se llama a este metodo que hace que por cada habitación existente se ponga la que hay que poner
        InstantiateRooms();        
    }

    List<List<GridUnit>> pathList = new List<List<GridUnit>>();
    void GeneratePaths()
    {
        List<GridUnit> currentPath;
        currentPath = CalculatePath( endUnits[0], grid[startPosition.x, startPosition.y]);
        pathList.Add(currentPath);

        for (int i = 0; i < endUnits.Count; i++)
        {

            //SI HAY VARIAS SALIDAS: GENERAR INTERSECCIONES
            if (i + 1 < endUnits.Count)
            {
                List<GridUnit> biggerPath = GetBiggerPath();

                int inter;
                if (biggerPath.Count > 1) inter = Random.Range(1, biggerPath.Count - 1);
                else inter = 0;

                //El camino del que parte la intersección pasa a ser dos caminos de los cuales puede salir otra intersección
                int c = biggerPath.Count-1;
                List<GridUnit> newPath = new List<GridUnit>();
                for (int j = c; j >inter; j--)
                {
                    newPath.Add(biggerPath[j]);
                    biggerPath.Remove(biggerPath[j]);                    
                }
                
                pathList.Add(newPath);
                currentPath = CalculatePath( endUnits[i + 1], biggerPath[inter]);
                pathList.Add(currentPath);
                
                biggerPath.Remove(biggerPath[inter]);
            }
        }
    }
    
    List<GridUnit> GetBiggerPath()
    {
        List<GridUnit> biggerPath = pathList[0];
        foreach (List<GridUnit> path in pathList)
        {
            if (path.Count > biggerPath.Count)
            {
                biggerPath = path;
            }
        }

        return biggerPath;
    }

    //     G E N E R A   C A D A   C A M I N O 

    List<GridUnit> CalculatePath(GridUnit start, GridUnit end)
    {

        List<GridUnit> unitsInPath = new List<GridUnit>();

        GridUnit origin = start;

        //Si la entrada está a más de 6 de distancia se añade una casilla intermedia en una posición aleatoria en un rango de 3x3 endirección a la salida
        while (Vector2.Distance(origin.arrayPos, end.arrayPos)>5)
        {
            int y, x;
            Vector2Int pos = origin.arrayPos;

            //Seleccionar el rango de casillas 
            if (pos.y == end.arrayPos.y) y = Random.Range(pos.y - 1,pos.y + 1);
            else if(pos.y > end.arrayPos.y) y = Random.Range(pos.y - 1, pos.y - 3);
            else y = Random.Range(pos.y + 1, pos.y + 3);

            if (pos.x == end.arrayPos.x) x = Random.Range(pos.x - 1, pos.x + 1);
            else if (pos.x > end.arrayPos.x) x = Random.Range(pos.x - 1, pos.x - 3);
            else x = Random.Range(pos.x + 1, pos.x + 3);

            GridUnit newRoom = grid[Mathf.Clamp(x,0,columns-1),Mathf.Clamp(y,0,rows-1)];

            if (!unitsFilled.Contains(newRoom))
            {
                List<GridUnit> fragment = BreathFirstSearch(origin, newRoom); if (fragment.Count == 0) print("uwu");

                if (fragment.Count == 0 && !AdyacentUnits(newRoom).Contains(origin)) //Si el algoritmo no es capaz de encontrar un camino, elimina esa salida
                {
                    print("No hay un camino disponible hacia esta salida" + start.arrayPos);
                    unitsFilled.RemoveRange(unitsFilled.Count - unitsInPath.Count, unitsInPath.Count);
                    ResetExits(unitsInPath);
                    unitsInPath.Clear();
                    unitsFilled.Remove(start);
                    start.roomType = RoomType.none;
                    return unitsInPath;
                }

                unitsFilled.Add(newRoom);
                unitsFilled.AddRange(fragment);

                unitsInPath.InsertRange(0, fragment);
                unitsInPath.Insert(0, newRoom);
                origin = newRoom;
            }
            else
            {
                if(newRoom.roomType==RoomType.path)end = newRoom;
                break;
            }
        }
        List<GridUnit> endFragment = BreathFirstSearch(origin, end);

        if (endFragment.Count == 0 && !AdyacentUnits(end).Contains(origin))
        {
            print("No hay un camino disponible hacia esta salida");
            unitsFilled.RemoveRange(unitsFilled.Count - unitsInPath.Count, unitsInPath.Count);
            ResetExits(unitsInPath);
            unitsInPath.Clear();
            unitsFilled.Remove(start);
            start.roomType = RoomType.none;
            return unitsInPath;
        }

        unitsInPath.InsertRange(0,endFragment);
        unitsFilled.RemoveRange(unitsFilled.Count - (unitsInPath.Count - endFragment.Count), unitsInPath.Count-endFragment.Count); 
        //Se sacan de la lista y se añaden al final todas juntas para que se pinten en el orden correcto (solo para la demostración)

        unitsFilled.AddRange(unitsInPath);
        foreach (GridUnit item in unitsInPath)
        {
            item.roomType = RoomType.path;
        }

        if (end.roomType==RoomType.path) end.roomType = RoomType.intersection;

        return unitsInPath;
    }

    //Cuando se descarta un camino porque no se ha logrado conectar la salida, se resetean las conexiones de cada casilla 
    void ResetExits(List<GridUnit> list)
    {
        foreach (GridUnit item in list)
        {
            item.exits = new Vector4(0,0,0,0);
        }
    }


    //      A L G O R I T M O   D E   B U S Q U E D A   E N   A N C H U R A 

    List<GridUnit> BreathFirstSearch(GridUnit start, GridUnit end)
    {
        List<GridUnit>unitsInPath=new List<GridUnit>();
        GridUnit actualUnit = start;
        HashSet<GridUnit> explored = new HashSet<GridUnit>();
        Queue<GridUnit> queue = new Queue<GridUnit>();
        IDictionary<GridUnit, GridUnit> parents = new Dictionary<GridUnit, GridUnit>(); //El primer valor es la unidad que se busca, la segunda es el padre asignado

        queue.Enqueue(actualUnit);
        explored.Add(actualUnit);
        do
        {
            actualUnit = queue.Dequeue();

            List<GridUnit> adyacent = AdyacentUnits(actualUnit); 

            foreach (GridUnit unit in adyacent)
            {
                if (!explored.Contains(unit))
                {
                    if (unit.arrayPos == end.arrayPos)
                    {
                        SetNeighbour(actualUnit, end);
                        //Generar el camino con lo que se haya guardado
                        while (actualUnit != start)
                        {
                            SetNeighbour(actualUnit, parents[actualUnit]);
                            unitsInPath.Add(actualUnit);
                            actualUnit = parents[actualUnit];                      
                        } 
                        return unitsInPath;
                    }
                    else if (!unitsFilled.Contains(unit))
                    {
                        explored.Add(unit);
                        parents.Add(unit, actualUnit);
                        queue.Enqueue(unit);
                    }                    
                }
            }       
            
        } while (queue.Count > 0); 

        return unitsInPath;
    }


    //     D E V U E L V E   U N I D A D E S   A D Y A C E N T E S

    List<GridUnit> AdyacentUnits(GridUnit unit) 
    {
        List<GridUnit> list = new List<GridUnit>(); 

        int row = (int)unit.arrayPos.x;
        int column = (int)unit.arrayPos.y;

        
        if (row-1>-1)
        {
            list.Add(grid[row-1,column]); 
        }
        if (row+1<columns)
        {
            list.Add(grid[row+1, column]);
        }
        if (column-1>-1)
        {
            list.Add(grid[row, column-1]);
        }
        if (column + 1 < rows)
        {
            list.Add(grid[row, column + 1]);
        }

        return list;
    }


    //  S E T E A   L A S   S A L I D A S   D E   L A S  H A B I TA C I O N E S  V E C I N A S  (0-left, 1-right, 2-up, 3-down)

    void SetNeighbour(GridUnit unit1, GridUnit unit2)
    {
        if (unit1.arrayPos.x == unit2.arrayPos.x)
        {
            if (unit1.arrayPos.y < unit2.arrayPos.y)
            {
                unit1.AddExits(2);
                unit2.AddExits(3);
            }
            else
            {
                unit1.AddExits(3);
                unit2.AddExits(2);
            }
        }
        else
        {
            if (unit1.arrayPos.x < unit2.arrayPos.x)
            {
                unit1.AddExits(1);
                unit2.AddExits(0);
            }
            else
            {
                unit1.AddExits(0);
                unit2.AddExits(1);
            }
        }
    }


    //  E L I G E   L A   L I S T A   C O R R E S P O N D I E N T E   D E L   S C R I P T A B L E   S E G U N   E L   T I P O   D E   H A B I T A C I O N   Q U E   S E A 

    void InstantiateRooms() 
    {
        foreach (GridUnit unit in unitsFilled)
        {
            switch (unit.roomType)
            {
                case RoomType.exit:
                    InstantiateFromList(roomList.hall.type, unit);
                    //     if (unit.exits.x == 1) InstantiateFromList(roomList.exit.typeL, unit);
                    //else if (unit.exits.y == 1) InstantiateFromList(roomList.exit.typeR, unit);
                    //else if (unit.exits.z == 1) InstantiateFromList(roomList.exit.typeU, unit);
                    //else if (unit.exits.w == 1) InstantiateFromList(roomList.exit.typeD, unit);
                    break;

                case RoomType.intersection:
                         if (unit.exits.x == 0) InstantiateFromList(roomList.intersection.typeRUD, unit);
                    else if (unit.exits.y == 0) InstantiateFromList(roomList.intersection.typeLUD, unit);
                    else if (unit.exits.z == 0) InstantiateFromList(roomList.intersection.typeLRD, unit);
                    else if (unit.exits.w == 0) InstantiateFromList(roomList.intersection.typeLRU, unit);
                    break;

                case RoomType.start:
                        /* if (unit.exits.x == 1) */InstantiateFromList(roomList.hall.type, unit);
                    //else if (unit.exits.y == 1) InstantiateFromList(roomList.hall.typeR, unit);
                    //else if (unit.exits.z == 1) InstantiateFromList(roomList.hall.typeU, unit);
                    //else if (unit.exits.w == 1) InstantiateFromList(roomList.hall.typeD, unit);
                    break;

                case RoomType.path:
                    if (unit.exits.x == 1)
                    {
                             if (unit.exits.y == 1) InstantiateFromList(roomList.corridor.typeLR, unit);
                        else if (unit.exits.z == 1) InstantiateFromList(roomList.corridor.typeLU, unit);
                        else if (unit.exits.w == 1) InstantiateFromList(roomList.corridor.typeLD, unit);
                    }
                    else if (unit.exits.y == 1)
                    {
                             if (unit.exits.z == 1) InstantiateFromList(roomList.corridor.typeRU, unit);
                        else if (unit.exits.w == 1) InstantiateFromList(roomList.corridor.typeRD, unit);
                    }
                    else InstantiateFromList(roomList.corridor.typeUD, unit);
                    break;
            }
            //yield return new WaitForSeconds(0.2f);
        }
    }

    void InstantiateFromList(List<Room> list, GridUnit unit)
    {
        if (list.Count==0)
        {
            Debug.Log("There aren't rooms of each type in " + roomList.name);
            return;
        }

        Room room = list[Random.Range(0, list.Count - 1)];

        for (int t = 0; t < tilemap.Length; t++)
        {
            for (int x = 0; x < roomList.size; x++)
            {
                for (int y = 0; y < roomList.size; y++)
                {
                    TileBase tile = room.GetTile(x, y, t);
                    if (tile != null)
                    {
                        if (t != 1)
                        {
                            tilemap[t].SetTile(new Vector3Int(unit.position.x + x, unit.position.y + y, 0), tile);
                        }
                        else if(objectsMap.ContainsKey(tile))
                        {
                            Instantiate(objectsMap[tile], new Vector3Int(unit.position.x + x, unit.position.y + y, 0), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }


    //No pueden generarse salidas en los extremos para evitar bloquear caminos
    void GenerateEnds()
    {
        List<Vector2Int> unitList = new List<Vector2Int>();
        for (int x = 1; x < columns-1; x++)
        {
            for (int y= 1; y < rows-1 ; y++)
            {
                unitList.Add(new Vector2Int(x,y));
            }
        }

        //Calcula las casillas alrededor de la entrada en las que no se puede generar la salida y para quitarlas de la lista
        int unitsDist = (int)Mathf.Round((columns+rows)/10); // n de casillas libres por cada lado

        Vector2Int unitPos = startPosition;

        for (int i = 0; i < endRooms; i++)
        {
            //Quita de la lista un area cuadrada alrededor de la entrada y las salidas que se generen en cada vuelta del bucle
            for (int x = unitPos.x-unitsDist; x < unitPos.x+ unitsDist*2+1; x++)
            {
                for (int y = unitPos.y - unitsDist; y < unitPos.y + unitsDist * 2 + 1; y++)
                {
                    unitList.Remove(new Vector2Int(x,y));
                }
            }

            if (unitList.Count>0)
            {
                Vector2Int exitPos = unitList[Random.Range(0, unitList.Count-1)];
                GridUnit exit = grid[exitPos.x, exitPos.y];
                exit.roomType = RoomType.exit;
                unitsFilled.Add(exit);
                endUnits.Add(exit); //Es importante usar el count de esta lista, porque si la dismensión de la grid es demasiado pequeña, puede que no se generen todas las salidas que se indican en el int del inspector
                unitPos = exitPos;
            }
            else
            {
                print("El tamaño del grid es demasiado pequeño para el número de salidas");
                break;
            }
        }   
    }

    



    // - - - - - - G I Z M O S - - - - - -
    private void OnDrawGizmos()
    {
        Rect rect = new Rect(0,0,columns*roomList.size, rows* roomList.size);
        UnityEditor.Handles.DrawSolidRectangleWithOutline(rect, Color.clear, Color.cyan);


        int xpos = (int)Mathf.Clamp(startPosition.x, 0, columns - 1) * roomList.size;
        int ypos = (int)Mathf.Clamp(startPosition.y, 0, rows - 1) * roomList.size;
        Rect startRect = new Rect(xpos,ypos, roomList.size, roomList.size);

        UnityEditor.Handles.DrawSolidRectangleWithOutline(startRect, Color.clear, Color.yellow);
    }

}


public class GridUnit 
{
    public RoomType roomType;
    public Vector3Int position;

    public Vector2Int arrayPos;

    public Vector4 exits = new Vector4(0,0,0,0);

    public void AddExits(int pos)
    {
        switch (pos)
        {
            case 0:
                exits.x = 1;
                break;
            case 1:
                exits.y = 1;
                break;
            case 2:
                exits.z = 1;
                break;
            case 3:
                exits.w = 1;
                break;
        }
    }

    public GridUnit(int row, int column, int unitSize)
    {
        arrayPos = new Vector2Int(row, column);
        position = new Vector3Int(row*unitSize,column*unitSize,0);
    }
}

public enum RoomType
{
    none,
    start,
    path,
    exit,
    intersection,
}