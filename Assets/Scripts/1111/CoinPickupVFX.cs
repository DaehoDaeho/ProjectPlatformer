using UnityEngine;

// ����: CoinPickupVFX.cs
// ��ġ: ���� ������(CollectibleCoin�� �Բ�) ����.
// ������ �ǵ帮�� �ʰ�, �ǵ�� �̺�Ʈ�� ����մϴ�.
[RequireComponent(typeof(Collider2D))]
public class CoinPickupVFX : MonoBehaviour
{
    [SerializeField] private int displayValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isPlayer = (other.CompareTag("Player") == true);
        if (isPlayer == true)
        {
            GameplayEvents.RaiseCoinCollected(transform.position, displayValue);
            // �� ������Ʈ�� ���� �ı��� �������� �ʽ��ϴ�.
            // �ı��� ���� CollectibleCoin�� ����մϴ�.
        }
    }
}
