using UnityEngine;

class Visual : MonoBehaviour
{
    [SerializeField] GameObject _jetpack;
    [SerializeField] GameObject _magnet;
    [SerializeField] GameObject _armorBubble;
    [SerializeField] GameObject _gun;
    [SerializeField] GameObject _rocket;
    [SerializeField] GameObject _laser;

    [SerializeField] ParticleSystem _jetpackParticle;

    public void StartJetpack() 
    { 
        _jetpack.SetActive(true);
        _jetpackParticle.Play();
    }
    public void StopJetpack() 
    {
        _jetpack.SetActive(false);
        _jetpackParticle.Stop();
    }

    public void StartMagnet() { _magnet.SetActive(true); }
    public void StopMagnet() { _magnet.SetActive(false); }

    public void StartArmor() { _armorBubble.SetActive(true); }
    public void StopArmor() { _armorBubble.SetActive(false); }
}
