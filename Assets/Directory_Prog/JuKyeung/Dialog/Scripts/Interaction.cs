using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] private GameObject interact_UI;
    [SerializeField] private GameObject interact_UIUpPos;

    [SerializeField] Camera lookCamera;

    [SerializeField] DialogUI dialogUI;

    [Header("�÷��̾� �ξƿ� üũ")]
    [SerializeField] bool playerCheck;

    private void Awake()
    {
        playerCheck = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCheck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerCheck = false;
    }

    private void Update()
    {
        DialogPosition();
        PlayerInOutCheck();

    }
    private void PlayerInOutCheck()
    {
        if (playerCheck == true)
        {
            Debug.Log("�÷��̾� IN");
            interact_UI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E Ű�� �Է��� �����Ǿ����ϴ�. ");
                interact_UI.SetActive(false);
                dialogUI.GetContext();
            }

        }
        else if (playerCheck == false)
        {
            Debug.Log("�÷��̾� Out");
            interact_UI.SetActive(false);
        }
    }

    void DialogPosition() // ī�޶� �ٶ󺸾ƶ�
    {
        interact_UI.transform.position = interact_UIUpPos.transform.position;
        interact_UI.transform.LookAt(lookCamera.transform);
    }
}
