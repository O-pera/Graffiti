using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PuzzleEffect : MonoBehaviour
{
    public Image xEffect;

    public GameObject moveObject;
    public Transform targetPosition;

    /// <summary>
    ///  ���� ȿ��
    /// </summary>
    public void BlinkAnim()
    {
        StartCoroutine(BlinkAnimCorutine());
        Debug.Log("�ڷ�ƾ ����");
    }
    public IEnumerator BlinkAnimCorutine()
    {
        Debug.Log("�ڷ�ƾ ���౸��");
        float duration = 2.0f; // �����̴� ��ü �ð� (��)
        float blinkInterval = 0.5f; // �����̴� �ֱ� (��)

        float timer = 0f;
        while (timer < duration)
        {
            float alpha = Mathf.PingPong(timer / blinkInterval, 1f); // 0~1 ���̷� �ݺ��Ǵ� ��
            Color color = xEffect.color;
            color.a = alpha;
            xEffect.color = color;

            yield return null;
            timer += Time.deltaTime;
        }

        // �������� Ȯ���� ���̵��� ����
        Color finalColor = xEffect.color;
        finalColor.a = 1.0f;
        xEffect.color = finalColor;

        yield return 2.0f;
        Debug.Log("2�� ����");
        StartCoroutine( MoveObjectCorutine());
    }

    /// <summary>
    /// Ư�� ������Ʈ�� Ÿ�ٿ�����Ʈ�� ��ġ�� �̵���Ű�� ȿ��
    /// </summary>
    //public void MoveObject()
    //{
    //    MoveObjectCorutine();
    //    Debug.Log("������Ʈ �̵�");
    //}

    private IEnumerator MoveObjectCorutine()
    {
        Debug.Log("������Ʈ �̵� ����");
        // float moveDuration = 2.5f;
        float moveSpeed = 100f;


        //while(moveObject.transform.position != targetPosition.position)
        //{
        //    moveObject.GetComponent<Draggable>().enabled = false;
        //    moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position, 
        //        targetPosition.position, moveSpeed * Time.deltaTime);

        //    yield return null;
        //}
        while (Vector3.Distance(moveObject.transform.position, targetPosition.position) > 0.1f)
        {
            moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position,
                targetPosition.position, moveSpeed * Time.deltaTime);

            yield return null;
        }
        moveObject.SetActive(false);

    }
}

