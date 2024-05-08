using System;
using LogicTower.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LogicTower.LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private ChallengeSettings[] challenges;
        [SerializeField] private LoadTransitionHandler transitionHandler;

        public static event Action OnLevelLoaded = delegate { };
        private ChallengeSettings CurrentChallengeSettings => challenges[_currentChallenge];

        private static LevelLoader _levelLoader;
        private static bool _isLoadingLevel;
        private int _currentChallenge = -1;
        private AsyncOperationHandle _loadOperation;
        private AsyncOperationHandle<GameObject> _instantiateOperation;

        private void Start()
        {
            _levelLoader = this;
            LoadNextChallenge();

            transitionHandler.OnTransitionFinished += ReleaseAndLoadNextLevel;
        }

        private void OnDestroy()
        {
            transitionHandler.OnTransitionFinished -= ReleaseAndLoadNextLevel;
        }

        public static void LoadNextChallenge()
        {
            if (!_isLoadingLevel)
            {
                _isLoadingLevel = true;
                
                if (_levelLoader.HasLoadedLevel())
                    _levelLoader.transitionHandler.FadeOut();
                else
                    _levelLoader.LoadNextChallengeInternal();
            }
        }
        
        private void ReleaseAndLoadNextLevel()
        {
            ReleaseCurrentChallenge();
            LoadNextChallengeInternal();
        }

        private void LoadNextChallengeInternal()
        {
            _currentChallenge++;
            
            _loadOperation = Addressables.LoadAssetAsync<GameObject>(CurrentChallengeSettings.LevelPrefab);
            _loadOperation.Completed += SpawnLevel;
        }

        private void SpawnLevel(AsyncOperationHandle handle)
        {
            _loadOperation.Completed -= SpawnLevel;
            _instantiateOperation = Addressables.InstantiateAsync(CurrentChallengeSettings.LevelPrefab);
            _instantiateOperation.Completed += RevealLevel;
        }

        private void RevealLevel(AsyncOperationHandle<GameObject> operation)
        {
            _instantiateOperation.Completed -= RevealLevel;
            _isLoadingLevel = false;
            OnLevelLoaded();
            transitionHandler.FadeIn();
        }

        private void ReleaseCurrentChallenge()
        {
            Addressables.ReleaseInstance(_instantiateOperation.Result);
            Addressables.Release(_loadOperation);
        }

        private bool HasLoadedLevel()
        {
            return _currentChallenge >= 0 && _loadOperation.IsValid() && _loadOperation.IsDone && _instantiateOperation.IsValid() && _instantiateOperation.IsDone;
        }
    }
}