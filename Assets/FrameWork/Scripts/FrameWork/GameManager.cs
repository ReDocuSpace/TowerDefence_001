using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public enum EPlay_Scene
{
    None,
    // Play
    Lobby,
    InGame,

    // Game

}
public enum ELoad_Scene
{
    // Loading
    None,
    Loading_Title,
    Loading_Common
}

namespace CompanyName.FrameWork
{
    public class GameManager : MonoSingleton<GameManager>
    {

        // Scene Management

        private readonly WaitForSeconds m_delayTime = new WaitForSeconds(1.0f);

        // 로드 관리
        [HideInInspector] public string curScene = "";
        [HideInInspector] public string curLoadScene = "";
        [HideInInspector] public bool loadState = false;
        [HideInInspector] public IPlayManager currentPlay;

        EPlay_Scene sceneCheckSum;
        ELoad_Scene loadCheckSum;

        private Coroutine m_cChangeScene = null;

        public void ChangeScene(EPlay_Scene scene, ELoad_Scene load = ELoad_Scene.None)
        {
            if (m_cChangeScene == null)
                m_cChangeScene = StartCoroutine(cChangeScene(scene, load));
        }
        private IEnumerator cChangeScene(EPlay_Scene scene, ELoad_Scene load = ELoad_Scene.None)
        {
            UIManager.Instance.FadeIn();

            yield return YieldInstructionCache.WaitForSecond(1.0f);

            UIManager.Instance.CloseDialog();

            // 예외처리
            if (sceneCheckSum == EPlay_Scene.None) sceneCheckSum = EPlay_Scene.Lobby;
            if (loadCheckSum == ELoad_Scene.None) loadCheckSum = ELoad_Scene.Loading_Title;

            // 저장된 Scene 가져오기
            if (scene == EPlay_Scene.None) scene = sceneCheckSum;
            if (load == ELoad_Scene.None) load = loadCheckSum;

            // 문자열 저장
            string nextScene = string.Format("Scene_{0}", scene.ToString());
            string loadScene = string.Format("Scene_{0}", load.ToString());

            // CheckSUM
            sceneCheckSum = scene;
            loadCheckSum = load;

            curScene = nextScene;
            curLoadScene = loadScene;

            if (!loadState)
                SceneManager.LoadScene(curLoadScene);

            m_cChangeScene = null;
        }
        //

        


        // Singleton Override
        public override void Init()
        {
        }

        public override void Release()
        {
        }

        
    }
}

