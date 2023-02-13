using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Graffiti.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        /// <summary>
        /// ��� ��� 
        /// </summary>
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>();

        /// <summary>
        /// ���Ӱ� �����Ǵ� ����� �ʱ� ��ġ�� �ش� ��ġ�Դϴ�. 
        /// </summary>
        [NonSerialized]
        Vector2 newNodeOffset = new Vector2(250, 0);

        /// <summary>
        /// ������ ������ uniqueID(�̸�) �� DialogueNode �� ������ ��� �ֽ��ϴ�. 
        /// </summary>
        [NonSerialized]
        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        /// <summary>
        /// RootNode�� ��ȯ�մϴ�
        /// </summary>
        /// <returns></returns>
        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (string childID in parentNode.GetChildren())
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
        }

#if UNITY_EDITOR

        /// <summary>
        /// ��带 �����ϴ� �޼����Դϴ�.
        /// </summary>
        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChild(newNode.name);
                newNode.SetPlayerIsSpeaking(!parent.IsPlayerSpeaking());
                newNode.SetPosition(parent.GetRect().position + newNodeOffset);
            }
            Undo.RegisterCreatedObjectUndo(newNode, "Create Dialogue Node"); // ������Ʈ ���� ���� ȣ��
            if (AssetDatabase.GetAssetPath(this) != "")
            {
                Undo.RecordObject(this, "Added Dialogue Node");
            }
            nodes.Add(newNode);
            OnValidate();
        }

        /// <summary>
        /// ��带 �����ϴ� �޼����Դϴ�.
        /// </summary>
        /// <param name="nodeToDelete"></param>
        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialogue Node");

            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);

        }

        private void CleanDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
        }
#endif



        /// <summary>
        /// �θ� ���̾�αװ� ������ ��� ����� �ڽ� ��带 �Բ� ���� ��Ű�� �޼����Դϴ�.
        /// </summary>
        /// <param name="nodeToDelete"></param>



        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                CreateNode(null);
            }

            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (DialogueNode node in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this); // ������Ʈ�� ���Ե� ���¿� �����ϰ� ���ش�
                    }
                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {
        }
    }
}