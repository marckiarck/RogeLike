using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGizmo : MonoBehaviour
{
    int size;
    public int Size {  set => size = value; }

    bool left;
    bool right;
    bool up;
    bool down;
    

    public void ShowGizmos(bool left, bool right, bool up, bool down)
    {
        this.left = left;
        this.right = right;
        this.up = up;
        this.down = down;
    }


    private void OnDrawGizmos()
    {
        if (size>0)
        {
            Rect rect = new Rect(0, 0, size, size);
            UnityEditor.Handles.DrawSolidRectangleWithOutline(rect, Color.clear, Color.cyan);

            int d = DoorSize();

            if (left)
            {
                Rect rect1 = new Rect(0, size/3, 1, d);
                UnityEditor.Handles.DrawSolidRectangleWithOutline(rect1, Color.clear, Color.red);
            }
            if (right)
            {
                Rect rect2 = new Rect(size-1, size/3, 1, d);
                UnityEditor.Handles.DrawSolidRectangleWithOutline(rect2, Color.clear, Color.red);
            }
            if (up)
            {
                Rect rect3 = new Rect(size/3, size-1, d, 1);
                UnityEditor.Handles.DrawSolidRectangleWithOutline(rect3, Color.clear, Color.red);
            }
            if (down)
            {
                Rect rect4 = new Rect(size/3, 0, d, 1);
                UnityEditor.Handles.DrawSolidRectangleWithOutline(rect4, Color.clear, Color.red);
            }
        }
    }

    int DoorSize()
    {
        int doorSize = size / 3;
        if (size%3!=0)
        {
            doorSize += 1;
        }
        return doorSize;
    }
}
