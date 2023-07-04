using UnityEngine;

public class PlaySound : MonoBehaviour
{
    AudioSource audioSource;

    // 在Start函數中獲取AudioSource組件
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // 當其他Collider進入觸發區域時調用
    void OnTriggerEnter(Collider other)
    {
        // 檢查進入觸發區域的Collider是否是玩家
        if (other.CompareTag("Player"))
        {
            // 如果是玩家，則播放音頻
            audioSource.Play();
        }
    }
}
