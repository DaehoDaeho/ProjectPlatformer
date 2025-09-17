using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float moveSpeed = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ����ڰ� Ű������ ����Ű�� ������ �� �̵� ó��.
        // �� �����Ӹ��� �Է� üũ�� ó���� �ϱ� ���ؼ� Update �Լ� �ȿ� �̵� ��� ����.
        // ������� Ű �Է��� ����.
        // � Ű�� �������� �˻��ؼ� ������ �������� �̵� �ӵ��� �����ؼ� ������Ʈ�� ��ġ�� �������ش�.
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // �̵��� ������Ʈ�� ��ġ�� �������ִ� ó��.
        // ������ �����ǿ� �̵��� �������� �����ش�.
        // �� �� �̵��� �����ǿ� �̵��ӵ��� ���ؼ� ������ �ְ�,
        // ����� ��翡 ������� ������ �������� �̵��� ��Ű�� ���� Time.deltaTime�� �����ش�.
        Vector3 position = new Vector3(inputX, inputY, 0.0f);
        gameObject.transform.position = gameObject.transform.position + (position * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("�÷��̾ ������� �Ծ���. ������� = " + damage);
        // hp�� ���ҽ����ְ�, ���࿡ hp�� 0���� �۰ų� ������, 
    }
}
