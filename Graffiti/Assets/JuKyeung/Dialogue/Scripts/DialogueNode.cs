using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Graffiti.Dialogue
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogueNode : ScriptableObject
    {
        [SerializeField]
        bool isPlayerSpeaking = false;

        [SerializeField]
        string text;

        [SerializeField]
        List<string> children = new List<string>();

        [SerializeField]
        Rect rect = new Rect(0, 0, 200, 100);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Rect GetRect()
        {
            return rect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return text;
        }

        /// <summary>
        /// List<string> children �� ����
        /// </summary>
        /// <returns>children</returns>
        public List<string> GetChildren()
        {
            return children;
        }

        /// <summary>
        /// bool isPlayerSpeaking �� ����
        /// </summary>
        /// <returns></returns>
        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }
#if UNITY_EDITOR

        /// <summary>
        /// ���̾�α��� ��ġ�� ����մϴ�. (���̾�α׸� �̵���ų ���)
        /// </summary>
        /// <param name="newPosition"></param>
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");

            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newText"></param>
        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newIsPlayerSpeaking"></param>
        public void SetPlayerIsSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Node Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);

        }

        /// <summary>
        /// �ڽ� ���̾�α׿� ��ũ�� �߰�
        /// </summary>
        /// <param name="childID"></param>
        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");    // ��ũ��Ʈ ������Ʈ�� ����ϰ� ���̾�α׸� ����

            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// �ڽ� ���̾�α� ���� ��ũ�� ����
        /// </summary>
        /// <param name="childID"></param>
        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

#endif
    }
}