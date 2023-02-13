using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Graffiti.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;

        [NonSerialized]
        GUIStyle nodeStyle;

        [NonSerialized]
        GUIStyle playerNodeStyle;

        [NonSerialized]
        DialogueNode draggingNode = null;

        [NonSerialized]
        Vector2 draggingOffset;

        [NonSerialized]
        DialogueNode creatingNode = null;

        [NonSerialized]
        DialogueNode deletingNode = null;

        /// <summary>
        /// ��ũ�� �θ� ���
        /// </summary>
        [NonSerialized]
        DialogueNode linkingParentNode = null;

        Vector2 scrollPosition;

        [NonSerialized]
        bool draggingCanvas = false;

        [NonSerialized]
        Vector2 draggingCanvasOffset;

        /// <summary>
        /// ���̾�α� ������ ĵ���� ������
        /// </summary>
        const float canvasSize = 4000;
        const float backgroundSize = 2048;


        /// <summary>
        /// 
        /// </summary>
        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            //Debug.Log("ShowEditorWindow");
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }


        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;

            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            playerNodeStyle = new GUIStyle();
            playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;

            playerNodeStyle.normal.textColor = Color.white;
            playerNodeStyle.padding = new RectOffset(20, 20, 20, 20);
            playerNodeStyle.border = new RectOffset(12, 12, 12, 12);
        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                selectedDialogue = newDialogue;
                Repaint();
            }

        }

        private void OnGUI()
        {
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected ");
            }
            else
            {
                ProcessEvents();

                // �����Ϳ� ��ũ�Ѻ並 �߰� -> ũ�⿡ �°� �ڵ����� ��ũ���� �߰�
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                //Debug.Log(scrollPosition);

                //GUILayout.Label("�̰��� ���̾�α��� ������ ������̰� ");

                // ���̾�α� �������� ũ�⸦ 4000 x 2048 ���� ����
                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
                Texture2D backgroundTexture = Resources.Load("background") as Texture2D;
                Rect texCoords = new Rect(0, 0, canvasSize / backgroundSize, canvasSize / backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas, backgroundTexture, texCoords);

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                }

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);
                }

                EditorGUILayout.EndScrollView();

                if (creatingNode != null)
                {
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }

                if (deletingNode != null)
                {
                    selectedDialogue.DeleteNode(deletingNode);
                    deletingNode = null;
                }

            }

        }


        /// <summary>
        /// ������ �巡�� & Ŭ������ ������ �� �ְ� �ϴ� �޼����Դϴ� 
        /// </summary>
        private void ProcessEvents()
        {

            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);

                // ��带 �����̰� ���� ��� 
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;

                    Selection.activeObject = draggingNode;
                }
                // ��带 �����̴ٰ� ������ ���
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedDialogue;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPosition = draggingCanvasOffset - Event.current.mousePosition;

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseUp && draggingCanvas)
            {
                draggingCanvas = false;
            }
        }

        /// <summary>
        /// ���̾�α� ��带 �׸��� �Լ��Դϴ�. 
        /// 
        /// </summary>
        /// <param name="node"></param>
        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = nodeStyle;
            if (node.IsPlayerSpeaking())
            {
                style = playerNodeStyle;
            }
            GUILayout.BeginArea(node.GetRect(), style); // ���� ���� 
            EditorGUI.BeginChangeCheck();

            //����� �� �ʵ�
            EditorGUILayout.LabelField("<Node> ", EditorStyles.whiteLabel);

            //����� TextField �� ���� Text�Է� �ʵ��Դϴ�. 
            node.SetText(EditorGUILayout.TextField(node.GetText()));

            GUILayout.BeginHorizontal(); // Begin - End Horizontal ���� ������ �������� ��ġ 

            // ���̾�α׸� �߰��ϴ� ��ư 
            if (GUILayout.Button("�߰�"))
            {
                creatingNode = node;
            }

            // ���̾�α׸� �����ϴ� ��ư 
            if (GUILayout.Button("����"))
            {
                deletingNode = node;
            }

            GUILayout.EndHorizontal();

            DrawLinkButtons(node);

            GUILayout.EndArea(); // �� ���� 
        }

        /// <summary>
        /// �̹� ������ ���̾�α� ���� ������ ������ִ� �޼���
        /// </summary>
        private void DrawLinkButtons(DialogueNode node)
        {
            // ���̾�α׳��� ���� 
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("���̾�α� ��ũ"))
                {
                    linkingParentNode = node;
                }
            }

            // �����ϴ� ���� ��� 
            else if (linkingParentNode == node)
            {
                if (GUILayout.Button("���"))
                {
                    linkingParentNode = null;
                }
            }

            // �ڽ� ������ ��ũ�� ����
            else if (linkingParentNode.GetChildren().Contains(node.name))
            {
                if (GUILayout.Button("��ũ����"))
                {
                    linkingParentNode.RemoveChild(node.name);   // ��ũ�Ǵ� �θ� ����� �ڽ� ��带 ������ ��Ͽ� ���� ��带 ����
                    linkingParentNode = null;
                }
            }

            // �ڽ� ��� �����ؼ� ��ũ 
            else
            {
                if (GUILayout.Button("�ڽ� ���̾�α׷� ����"))
                {
                    Undo.RecordObject(selectedDialogue, "Add Dialogue Link");
                    linkingParentNode.AddChild(node.name);  // ��ũ�Ǵ� �θ� ����� �ڽ� ��带 ������ ��Ͽ� ���� ��带 �߰�
                    linkingParentNode = null;
                }
            }
        }

        /// <summary>
        /// ��带 �մ� ���� ���õ� �޼���  / ������ � ���
        /// </summary>
        /// <param name="node"></param>
        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.GetRect().xMax, node.GetRect().center.y);
            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startPosition, endPosition,
                    startPosition + controlPointOffset,
                    endPosition - controlPointOffset,
                    Color.white, null, 4f);
            }
        }

        /// <summary>
        /// �� ��带 �մ� ������ ��ġ�� ���ϴ� �޼���
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if (node.GetRect().Contains(point))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }
    }
}