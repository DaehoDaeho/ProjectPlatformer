using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float moveSpeed = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Monster[] mon = new Monster[4];
        mon[0] = new FlyingMonster();
        mon[1] = new AquaMonster();
        mon[2] = new FourLegsMonster();
        mon[3] = new HealingMonster();

        for (int i = 0; i < mon.Length; ++i)
        {
            mon[i].WalkOnGround();
        }

        //FlyingMonster mon2 = new FlyingMonster();
        //AquaMonster mon3 = new AquaMonster();
        //FourLegsMonster mon4 = new FourLegsMonster();

        //mon2.WalkOnGround();
        //mon3.WalkOnGround();
        //mon4.WalkOnGround();
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자가 키보드의 방향키를 눌렀을 때 이동 처리.
        // 매 프레임마다 입력 체크와 처리를 하기 위해서 Update 함수 안에 이동 기능 구현.
        // 사용자의 키 입력을 감지.
        // 어떤 키를 눌렀는지 검사해서 적합한 방향으로 이동 속도를 적용해서 오브젝트의 위치를 변경해준다.
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // 이동은 오브젝트의 위치를 변경해주는 처리.
        // 기존의 포지션에 이동할 포지션을 더해준다.
        // 이 때 이동할 포지션에 이동속도를 곱해서 젹용해 주고,
        // 기기의 사양에 관계없이 일정한 간격으로 이동을 시키기 위해 Time.deltaTime을 곱해준다.
        Vector3 position = new Vector3(inputX, inputY, 0.0f);
        gameObject.transform.position = gameObject.transform.position + (position * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("플레이어가 대미지를 입었다. 대미지는 = " + damage);
        // hp를 감소시켜주고, 만약에 hp가 0보다 작거나 같으면, 
    }
}
