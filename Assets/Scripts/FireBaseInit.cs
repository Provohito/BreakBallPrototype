using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;


public class FireBaseInit : MonoBehaviour
{
    
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var app = FirebaseApp.DefaultInstance;
        });
    }

    
}
