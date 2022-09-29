# 작업 환경 및 개요
───────────────────────────────────────────
Unity : 2021.3.xxx [ 호환 가능 ]

Asset 
- Dotween [ 실행시 설치 必 ]
- UniRX [ 실험적 ]

MainScene 에서 시작
- InGame / Lobby 선택 가능

Scene 추가시 Scene_[ Scene 이름 ] 형태로 생성 
Build Setting 에 Scene 추가

# 프레임워크 특이점
───────────────────────────────────────────

Unity Life Cycle

MonoBehavior - IPlayManager

Awake -> OnEnter
Destory -> OnExit
Update -> cGameFlow [ Coroutine 으로 관리 ]

# 구현 사항
───────────────────────────────────────────
- Model - ModelManager 관리 [ 모델 불러오기 ]
- Scene - Load 관리 [ ILoadManager ]
- ObjectPool 


# 업데이트 예정
───────────────────────────────────────────
- ModelManager : 추후 에셋번들버전 추가 예정
- DBManager : CSV / JSON / TXT 기능 가능하게끔 구현
- CameraManager : 카메라 특이점 관리 
- UIManager : UI 관리
