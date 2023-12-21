using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public GameObject dropPrefab; // 드랍할 프리팹에 대한 참조. Unity 에디터에서 할당.

    // OnTriggerEnter 메소드는 트리거 콜라이더에 다른 콜라이더가 들어올 때 호출됨.
    private void OnTriggerEnter(Collider other)
    {
        // 'Player' 태그를 가진 오브젝트가 트리거에 닿았는지 확인.
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.PlaytrapEffect();
            // 플레이어가 함정에 닿으면 콘솔에 메시지 출력
            Debug.Log("함정 발동");

            // 드랍 프리팹을 프리팹 자체의 위치에 생성.
            // Instantiate 함수는 새 게임 오브젝트를 생성하고, 이를 게임 월드에 배치함.
            // dropPrefab.transform.position은 프리팹의 위치를 참조함.
            // Quaternion.identity는 회전을 적용하지 않음을 나타냄.
            Instantiate(dropPrefab, dropPrefab.transform.position, Quaternion.identity);
        }
        else if(other.CompareTag("Cube"))
        {
            Debug.Log("함정 발동");
            Instantiate(dropPrefab, dropPrefab.transform.position, Quaternion.identity);
        }


    }
}

