using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public interface IDamagable

{
    void TakePhysicalDamage(float damageAmount);
    // ���ظ� �޴� ����� �����ϴ� �޼ҵ�
    // �Ű�����: damageAmount - �޴� ������ ���� ��Ÿ���ϴ�. �� ���� �Ϲ�������
    // ���� ���� ǥ���Ǹ�, ��ü�� ü�� � ������ �ش�.
}

public class PlayerConditions : MonoBehaviour, IDamagable
{
    // Condition Ŭ������ �÷��̾��� ü�°� ���õ� ������ ����
    [System.Serializable]
    public class Condition
    {
        public float curValue;  // ���� ü�� ��
        public float maxValue;  // �ִ� ü�� ��
        public float startValue;// ���� �� ü�� ��
        public Image uiBar;     // UI ���� ü�� ��

        // ü���� ������Ű�� �޼ҵ�
        public void Add(float amount) => curValue = Mathf.Min(curValue + amount, maxValue);

        // ü���� ���ҽ�Ű�� �޼ҵ�
        public void Subtract(float amount) => curValue = Mathf.Max(curValue - amount, 0.0f);

        // ���� ü���� ������� ��ȯ�ϴ� �޼ҵ�
        public float GetPercentage() => curValue / maxValue;
    }

    public Condition health; // �÷��̾��� ü�� ����
    public UnityEvent onTakeDamage; // ���ظ� �޾��� �� �߻��ϴ� �̺�Ʈ
    private bool isDead = false; // �÷��̾��� ��� ���¸� �����ϴ� �÷���

    void Start()
    {
        health.curValue = health.startValue; // ���� �� ü���� �ʱ�ȭ�մϴ�.
        UpdateHealthUI(); // UI�� ������Ʈ�մϴ�.
    }

    // ���ظ� �޴� �������̽� �޼ҵ� ����
    public void TakePhysicalDamage(float damageAmount)
    {
        if (isDead) return; // �̹� ����� ��� �߰� ���ظ� �����մϴ�.

        health.Subtract(damageAmount); // ü�� ����
        UpdateHealthUI(); // UI ������Ʈ

        // ü���� 0 ���Ϸ� �������� ��� ó��
        if (health.curValue <= 0.0f && !isDead)
        {
            Die();
        }
    }

    // ü�� UI�� ������Ʈ�ϴ� �޼ҵ�
    private void UpdateHealthUI()
    {
        if (health.uiBar != null)
        {
            health.uiBar.fillAmount = health.GetPercentage(); // ü�� ���� fillAmount�� ������Ʈ�մϴ�.
        }
    }

    // ��� ó�� �޼ҵ�
    public void Die()
    {
        if (!isDead)
        {
            Debug.Log("�÷��̾ �׾���.");
            isDead = true;

            // �ӽ÷� ���� ���� �ٽ� �ε��Ͽ� ������ ������մϴ�.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // ������ ���� ó���� ���� �ڷ�ƾ
    public bool IsDead() => isDead;

    public void DelayedDamage(int damageAmount, float delay) => StartCoroutine(DelayDamage(damageAmount, delay));

    private IEnumerator DelayDamage(int damageAmount, float delay)
    {
        yield return new WaitForSeconds(delay); // ������ �ð���ŭ ���
        if (!isDead) TakePhysicalDamage(damageAmount); // ��� �� ���� ����
    }
}


