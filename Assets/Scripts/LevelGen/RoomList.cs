using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class RoomList : ScriptableObject
{
    public int size;

    public Corridor corridor = new Corridor();
    public Intersection intersection = new Intersection();
    public Entry entry = new Entry();
    public Exit exit = new Exit();


    //He decidido hacerlo así en vez de hacer listas de listas para que sea más facil de enterder y evitar equivocaciones al refernciar los tipos de habitación
    [System.Serializable]
    public class Corridor
    {
        public List<Room> typeLR = new List<Room>();
        public List<Room> typeUD = new List<Room>();
        public List<Room> typeLU = new List<Room>();
        public List<Room> typeLD = new List<Room>();
        public List<Room> typeRU = new List<Room>();
        public List<Room> typeRD = new List<Room>();
    }

    [System.Serializable]
    public class Intersection
    {
        public List<Room> typeLRU = new List<Room>();
        public List<Room> typeLRD = new List<Room>();
        public List<Room> typeLUD = new List<Room>();
        public List<Room> typeRUD = new List<Room>();
    }

    [System.Serializable]
    public class Entry
    {
        public List<Room> typeL = new List<Room>();
        public List<Room> typeR = new List<Room>();
        public List<Room> typeU = new List<Room>();
        public List<Room> typeD = new List<Room>();
    }

    [System.Serializable]
    public class Exit
    {
        public List<Room> typeL = new List<Room>();
        public List<Room> typeR = new List<Room>();
        public List<Room> typeU = new List<Room>();
        public List<Room> typeD = new List<Room>();
    }
}

[System.Serializable]
public class Room
{
    public int size;
    public int layers;
    public TileBase[] roomTiles; 

    public Room(int size, int t) // t = tilemaps num
    {
        roomTiles = new TileBase[t * size*size];
        layers = t;
        this.size = size;
    }

    public void SetTile(int x, int y, int t, TileBase tile) // t = tilemap index
    {     
        if (tile!=null)
        { roomTiles[(t * size*size) + x*size + y] = tile; }        
    }

    public void ClearAllTiles()
    {
        for (int x = 0; x < layers * size * size; x++)
        {
            roomTiles[x] = null;
        }
    }

    public TileBase GetTile(int x, int y, int t)  // t = tilemap index
    {
        if (roomTiles[(t*size*size) + x*size+y] != null)
            return roomTiles[(t * size * size) + x * size + y];
        else return null;
    }
}