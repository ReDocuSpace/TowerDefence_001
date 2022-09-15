using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompanyName.FrameWork
{
    public class ILoadManager : MonoBehaviour
    {
        protected Coroutine _corLoading = null;

        private void Awake()
        {
            if (_corLoading == null)
            {
                if (GameManager.Instance != null)
                    GameManager.Instance.loadState = true;
                _corLoading = StartCoroutine(LoadScene(GameManager.Instance.curScene));
            }
        }

        private IEnumerator LoadScene(string nextScene)
        {
            UIManager.Instance.FadeOut();

            Debug.Log("´ÙÀ½ ¾À : " + nextScene);

            yield return YieldInstructionCache.WaitForSecond(1.0f);

            AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
            op.allowSceneActivation = false;

            OnStart();

            while(!op.isDone)
            {
                yield return null;

                float progress = Mathf.Clamp01(op.progress / 0.9f);

                OnPlay(progress);

                if (op.progress >= 0.9f)
                {
                    yield return YieldInstructionCache.WaitForSecond(1.0f);

                    OnEnd();

                    yield return YieldInstructionCache.WaitForSecond(5.0f);

                    UIManager.Instance.FadeIn();
                    yield return YieldInstructionCache.WaitForSecond(1.0f);

                    op.allowSceneActivation = true;
                    break;
                }

            }

            
            _corLoading = null;
        }

        protected virtual void OnStart()
        {
        }

        protected virtual bool OnPlay(float progress)
        {
            return false;
        }

        protected virtual void OnEnd()
        {
        }
    }
}
