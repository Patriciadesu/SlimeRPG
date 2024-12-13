using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMusicOnCutscene : MonoBehaviour
{
    /// <summary>
    /// โค้ดนี้โฟกัสเป็นคนเขียนฉันเข้ามาแทรกแทรงเพื่อช่วยโลกใบนี้ไว้ (ถ้าจะโทษกันกรุณาโทษไอฮิว เพราะทำกับมัน🥶🥶🥶)
    /// </summary>
    
  [SerializeField] private GameObject TGTMusic;
  [SerializeField] private GameObject PHCMusic;
    void Awake()
    {
        TGTMusic.SetActive(false);
        StartCoroutine(MusicPauseTime());
    }

    IEnumerator MusicPauseTime()
    {
        yield return new WaitForSeconds(53f);
        PHCMusic.SetActive(true);
    }
    

    
}
