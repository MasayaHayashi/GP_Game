using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class DrawNode : EditorWindow
{
    private Node root;

    [SerializeField]
    private GameObject test;

    [MenuItem("Window/Node Editor")]
    static void Open()
    {
        EditorWindow.GetWindow<DrawNode>();
    }

    protected void OnGUI()
    {
        if (root == null)
        {
            initilize();
        }

        BeginWindows();
        root.Draw();
        EndWindows();
    }

    private void initilize()
    {
        // 木構造の初期化
        this.root = new Node(0, new Vector2(200, 200));
        this.root.childs.Add(new Node(1, new Vector2(100, 300)));
        this.root.childs.Add(new Node(2, new Vector2(300, 300)));
        this.root.childs[1].childs.Add(new Node(3, new Vector2(200, 400)));
        this.root.childs[1].childs.Add(new Node(4, new Vector2(400, 400)));
    }

    public class Node
    {
        public int id;
        public Rect window;
        public List<Node> childs = new List<Node>();

        public Node(int id, Vector2 position)
        {
            this.id = id;
            this.window = new Rect(position, new Vector2(100, 50));
        }

        public void Draw()
        {
            this.window = GUI.Window(this.id, this.window, DrawNodeWindow, "Window" + this.id); 

            foreach (var child in this.childs)
            {
                DrawNodeLine(this.window, child.window);
                child.Draw();
            }
        }

        void DrawNodeWindow(int id)
        {
            GUI.DragWindow();
            GUI.Label(new Rect(30, 22, 100, 100), "Hoge" + id, EditorStyles.label);
        }

        static void DrawNodeLine(Rect start, Rect end)
        {
            Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height / 2, 0);
            Vector3 endPos = new Vector3(end.x + end.width / 2, end.y + end.height / 2, 0);

            Handles.DrawLine(startPos, endPos);
        }
    }
}
