using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    static public AudioPlayer Inst;

    [SerializeField] private AudioClip AlienDie;
    [SerializeField] private AudioClip AlienMelee;
    [SerializeField] private AudioClip AlienRange;
    [SerializeField] private AudioClip AlienHeal;
    [SerializeField] private AudioClip AlienStun;
    [SerializeField] private AudioClip CogCollect;
    [SerializeField] private AudioClip PyramidBuild;
    [SerializeField] private AudioClip AstronautDie;
    [SerializeField] private AudioClip AstronautShoot;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Inst = this;
    }

    public void PlayAlienDie() {audioSource.PlayOneShot(AlienDie);}
    public void PlayAlienMelee() {audioSource.PlayOneShot(AlienMelee);}
    public void PlayAlienRange() {audioSource.PlayOneShot(AlienRange);}
    public void PlayAlienHeal() {audioSource.PlayOneShot(AlienHeal);}
    public void PlayAlienStun() {audioSource.PlayOneShot(AlienStun);}
    public void PlayCogCollect() {audioSource.PlayOneShot(CogCollect);}
    public void PlayPyramidBuild() {audioSource.PlayOneShot(PyramidBuild);}
    public void PlayAstronautDie() {audioSource.PlayOneShot(AstronautDie);}
    public void PlayAstronautShoot() {audioSource.PlayOneShot(AstronautShoot);}
}
