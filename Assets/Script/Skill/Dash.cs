//using UnityEngine;
//using System.Collections;
//[CreateAssetMenu(fileName = "Dash", menuName = "Skill/Mobility/Dash", order = 1)]
//public class Dash : Mobility
//{
//    public float dashSpeed = 10f; // ��������㹡�� dash
//    public float dashDistance = 5f; // ���зҧ�٧�ش��� dash ��
//    private Camera mainCamera;

//    private void Awake()
//    {
//        // �֧ Camera ��ѡ
//        mainCamera = Camera.main;
//    }

//    public override IEnumerator OnUse()
//    {
//        Player character = Player.Instance; // ��ҧ�ԧ����Фü�����
//        if (character == null || mainCamera == null)
//        {
//            Debug.LogError("Player or Camera is missing!");
//            yield break;
//        }

//        // �֧���˹�������˹�Ҩ�
//        Vector3 mousePosition = Input.mousePosition;

//        // �ŧ���˹������ҡ˹�Ҩ� (Screen Position) �繵��˹���š (World Position)
//        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

//        // �ӹǳ��ȷҧ�ҡ����Ф���ѧ���˹��������
//        Vector2 characterPosition = character.transform.position;
//        Vector2 direction = (worldPosition - characterPosition).normalized;

//        // �ӹǳ���˹觻��·ҧ�������зҧ�٧�ش
//        Vector2 targetPosition = characterPosition + direction * dashDistance;

//        // ��Ǩ�ͺ��ê��ѵ�� (������к���觡մ��ҧ)
//        RaycastHit2D hit = Physics2D.Raycast(characterPosition, direction, dashDistance);
//        if (hit.collider != null)
//        {
//            // ��Ҫ���觡մ��ҧ �������¹���˹觻��·ҧ�繵��˹觷�誹
//            targetPosition = hit.point;
//        }

//        // ����� dash
//        yield return DashMovement(character, targetPosition);
//    }

//    private IEnumerator DashMovement(Character character, Vector2 targetPosition)
//    {
//        Vector2 startPosition = character.transform.position; // ���˹��������
//        float journeyTime = dashDistance / dashSpeed; // ����㹡�� dash
//        float elapsedTime = 0f;

//        // �������͹���Ẻ�Һ���
//        while (elapsedTime < journeyTime)
//        {
//            elapsedTime += Time.deltaTime;
//            character.transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / journeyTime);
//            yield return null;
//        }

//        // ��駵��˹觵���Ф������������·ҧ
//        character.transform.position = targetPosition;
//    }
//}
