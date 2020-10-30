using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().onHit(damage);
            SoundEffectPlaylist soundEffectPlaylist = GetComponent<SoundEffectPlaylist>();
            Debug.Log("boop");
            if (soundEffectPlaylist != null)
            {
                soundEffectPlaylist.PlayRandomFromClips();
            }
        }
    }
}
