using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleController : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    private bool isFacingObstacle = false;
    // Start is called before the first frame update
    void Start()
    {
        infoText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingObstacle && Input.GetKeyDown(KeyCode.Z))
        {
            infoText.gameObject.SetActive(true);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") // �����ײ����������
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = Vector2.zero; // ֹͣ���ǵ��˶�

            // ��������������ķ���Ƕ�
            Vector3 direction = transform.position - collision.transform.position;
            float angle = Vector3.Angle(direction, collision.transform.up);

            // �ж������Ƿ��������
            if (angle < 45)
            {
                isFacingObstacle = true;
            }
            else
            {
                isFacingObstacle = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") // ����뿪��ײ����������
        {
            isFacingObstacle = false;
        }
    }
    






}
