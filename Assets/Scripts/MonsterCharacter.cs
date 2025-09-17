using UnityEngine;

public class MonsterCharacter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AttackPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AttackPlayer()
    {
        PlayerCharacter player = GameObject.FindAnyObjectByType<PlayerCharacter>();
        player.TakeDamage(10);
    }
}
