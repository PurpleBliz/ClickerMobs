using GoogleMobileAds.Api;
using UnityEngine;

[RequireComponent(typeof(RewardAD))]
public class AdMobinit : MonoBehaviour
{
    private void Awake()
    {
        MobileAds.Initialize(init => { });
    }
}
