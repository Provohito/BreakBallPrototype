using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class MobAdRewarded : MonoBehaviour
{
    [SerializeField] private UIManagerGame _ui;
    private RewardedAd _rewardedAd;

#if UNITY_ANDROID
    private const string _rewardedUnitID = "ca-app-pub-5281254441931005/4447654501"; // тестовый айди
#elif UNITY_IPHONE
    private const string _bannerUnitID = "";
#else
    private const string _bannerUnitID = "unexpected_platform";
#endif
    private void OnEnable()
    {
        _rewardedAd = new RewardedAd(_rewardedUnitID);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);

        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        
    }

    private void OnDisable()
    {
        _rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        _ui.GGame();
    }

    public void ShowRewardedAd(GameObject obj)
    {
        if (_rewardedAd.IsLoaded())
        {
            
            _rewardedAd.Show();
            obj.GetComponent<Button>().interactable = false;
        }
    }


}
