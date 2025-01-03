using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Skill/Mobility/Dash", order = 1)]
public class Dash : Mobility
{
    public float dashSpeed = 1;   // ��������㹡�� Dash

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        Debug.Log("Using Dash skill");

        Player character = Player.Instance;
        if (character == null)
        {
            isActive = true;
            yield break;
        }

        // �֧���˹觨ҡ��ä�ԡ���˹�Ҩ�
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // �ӹǳ��ȷҧ�ҡ���˹觼�������ѧ���˹觷���ԡ
        Vector3 direction = (mousePosition - character.transform.position).normalized;

        character.GetComponent<Rigidbody2D>().AddRelativeForce(direction * dashSpeed * 50, ForceMode2D.Impulse);

        Debug.Log("Dash skill completed");

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}