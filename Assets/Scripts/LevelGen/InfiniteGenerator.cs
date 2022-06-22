using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public enum RType
{ 
    corridor, 
    intersection,
    hall
}

public class RoomNode
{
    public RType type;
    public Vector2Int pos;
    public Vector3Int worldPos;
    public Vector4 exits = new Vector4(0, 0, 0, 0);

    public void AddExit(int pos)
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

    public int GetExit(int pos)
    {
        switch (pos)
        {
            case 0:
                return (int)exits.x;
            case 1:
                return (int)exits.y;
            case 2:
                return (int)exits.z;
            case 3:
                return (int)exits.w;
        }
        return 0;
    }
    public RoomNode(int row, int column, int unitSize)
    {
        pos = new Vector2Int(row, column);
        worldPos = new Vector3Int(row * unitSize, column * unitSize, 0);
    }
}


public class InfiniteGenerator : MonoBehaviour
{
    [SerializeField] Transform player;
    RoomNode lastPlayerRoom;

    [SerializeField] RoomList roomData;
    Tilemap[] tilemap;

    [SerializeField] PrefabPair[] mapAux;
    Dictionary<TileBase, GameObject> objectsMap;

    Dictionary<Vector2Int, RoomNode> existingRooms;



    private void Awake()
    {
        // INICIALIZACION DEL MAPA
        objectsMap = new Dictionary<TileBase, GameObject>();
        for (int i = 0; i < mapAux.Length; i++)
        {
            if (!objectsMap.ContainsKey(mapAux[i].tile))
            {
                objectsMap.Add(mapAux[i].tile, mapAux[i].prefab);
            }
        }

        existingRooms = new Dictionary<Vector2Int, RoomNode>();
    }

    // Start is called before the first frame update
    void Start()
    {
        tilemap = FindObjectsOfType<Tilemap>();

        RoomNode node = new RoomNode(0, 0, roomData.size);
        node.type = RType.hall;
        node.exits = new Vector4(1, 1, 1, 1);
        existingRooms.Add(node.pos, node);
        InstantiateRoom(node);
        GenerateAdyacents(node);

        lastPlayerRoom = node;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 60 == 0)
        {
            CheckPlayerPos();
        }
    }

    // Comprueba si el player ha cambiado de habitación y genera nuevas habitaciones si se requieren
    void CheckPlayerPos()
    {
        float x = player.position.x / roomData.size; if (x < 0) x -= 1;
        float y = player.position.y / roomData.size; if (y < 0) y -= 1;
        Vector2Int playerRoomPos = new Vector2Int((int)x, (int)y);

        RoomNode room;
        existingRooms.TryGetValue(playerRoomPos, out room);

        if (room != null && room != lastPlayerRoom)
        {
            GenerateAdyacents(room);
            lastPlayerRoom = room;
        }

    }

    // (0-left, 1-right, 2-up, 3-down)
    Vector2Int[] ady = new[]
    {
            new Vector2Int(-1, 0),
            new Vector2Int( 1, 0),
            new Vector2Int( 0, 1),
            new Vector2Int( 0,-1),
            new Vector2Int( 1, 1),
            new Vector2Int(-1,-1),
            new Vector2Int(-1, 1),
            new Vector2Int( 1,-1)
    };

    void GenerateAdyacents(RoomNode node)
    {
        List<RoomNode> newRooms = new List<RoomNode>();

        foreach (Vector2Int pos in ady)
        {
            Vector2Int adyPos = node.pos + pos;
            if (!existingRooms.ContainsKey(adyPos))
            {
                RoomNode newNode = new RoomNode(adyPos.x, adyPos.y, roomData.size);
                newRooms.Add(newNode);
            }
        }

        foreach (RoomNode newRoom in newRooms)
        {
            existingRooms.Add(newRoom.pos, newRoom);
            MakeConections(newRoom);
            InstantiateRoom(newRoom);

        }
    }

    // (0-left, 1-right, 2-up, 3-down)
    void MakeConections(RoomNode node)
    {
        int conections = 0;

        for (int i = 0; i<4; i++)
        {
            Vector2Int adyPos = node.pos + ady[i];
            if (existingRooms.ContainsKey(adyPos))
            {
                RoomNode adyRoom;
                existingRooms.TryGetValue(adyPos, out adyRoom);

                //Comprueba las salidas de la adyacente
                switch (i)
                {
                    case 0:
                        if (adyRoom.exits.y == 1)
                        {
                            node.AddExit(i);
                            conections++;
                        }
                        break;
                    case 1:
                        if (adyRoom.exits.x == 1)
                        {
                            node.AddExit(i);
                            conections++;
                        }
                        break;
                    case 2:
                        if (adyRoom.exits.w == 1)
                        {
                            node.AddExit(i);
                            conections++;
                        }
                        break;
                    case 3:
                        if (adyRoom.exits.z == 1)
                        {
                            node.AddExit(i);
                            conections++;
                        }
                        break;

                }
            }
        }

        if (conections == 4)
        {
            node.type = RType.hall;
            return;
        }

        int newConections = Random.Range(2, 5)-conections;
        if (conections + newConections >= 4)
        { 
            node.exits = new Vector4(0,0,0,0);
            node.type = RType.hall;
            return;
        }

        List<int> aux = new List<int>();
        for (int i = 0; i<4; i++)
        {
            if (node.GetExit(i)==0)
            {
                aux.Add(i);
            }
        }

        for (int i = 0; i<newConections; i++)
        {
            if (aux.Count < 1) 
            {
                break; 
            }
            int r = Random.Range(0, aux.Count);
            node.AddExit(aux[r]);
            aux.Remove(r);
            conections++;

        }

        switch (conections)
        {
            case 2:
                node.type = RType.corridor;
                break;

            case 3:
                node.type = RType.intersection;
                break;

            case 4:
                node.type = RType.hall;
                break;
        }
    }


    void InstantiateRoom(RoomNode node)
    {
        List<Room> list = SelectList(node);

        if (list.Count == 0)
        {
            Debug.Log("There aren't rooms of each type in " + roomData.name);
            return;
        }

        Room room = list[Random.Range(0, list.Count - 1)];

        for (int t = 0; t < tilemap.Length; t++)
        {
            for (int x = 0; x < roomData.size; x++)
            {
                for (int y = 0; y < roomData.size; y++)
                {
                    TileBase tile = room.GetTile(x, y, t);
                    if (tile != null)
                    {
                        if (t != 1)
                        {
                            tilemap[t].SetTile(new Vector3Int(node.worldPos.x + x, node.worldPos.y + y, 0), tile);
                        }
                        else if (objectsMap.ContainsKey(tile))
                        {
                            Instantiate(objectsMap[tile], new Vector3Int(node.worldPos.x + x, node.worldPos.y + y, 0), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }


    List<Room> SelectList(RoomNode node)
    {
        switch (node.type)
        {
            case RType.intersection:
                if (node.exits.x == 0) return roomData.intersection.typeRUD;
                else if (node.exits.y == 0) return roomData.intersection.typeLUD;
                else if (node.exits.z == 0) return roomData.intersection.typeLRD;
                else if (node.exits.w == 0) return roomData.intersection.typeLRU;
                break;

            case RType.corridor:
                if (node.exits.x == 1)
                {
                    if (node.exits.y == 1) return roomData.corridor.typeLR;
                    else if (node.exits.z == 1) return roomData.corridor.typeLU;
                    else if (node.exits.w == 1) return roomData.corridor.typeLD;
                }
                else if (node.exits.y == 1)
                {
                    if (node.exits.z == 1) return roomData.corridor.typeRU;
                    else if (node.exits.w == 1) return roomData.corridor.typeRD;
                }
                else return roomData.corridor.typeUD;
                break;

            case RType.hall:
                return roomData.hall.type;
        }

        return roomData.hall.type;
    }
}
