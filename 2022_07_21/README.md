# 2022.07.18 이론 공부

## 2022.07.23 시험 대비 공부

***

### 유니티(Unity)
- 게임 개발용 소프트웨어
- 게임 엔진
- 장점
    - 가볍다
    - 무료(개인용, 일정 금액 이하로 수익이 날 때)
    - 멀티 플랫폼을 지원한다.

### 유니티 스크립트
- C#을 스크립트 언어로 사용
- 기본 클래스 : MonoBehavior
- 이벤트 함수들(Awake, Start, Update, OnDestroy, OnEnable, OnDisable)
    - Awake : 게임 오브젝트가 생성된 후 호출
    - OnEnable : 게임 오브젝트가 활성화 될 때 호출
    - Start : 첫 번째 Update 실행되기 직전에 한번 호출
    - Update : 매 프레임마다 호출
    - OnDisable : 게임 오브젝트가 비활성화 될 때 호출
    - OnDestroy : 게임 오브젝트가 삭제 될 때 호출 

### 유니티 씬(Scene) - 게임 오브젝트의 합
- 맵, 레벨, 스테이지 등등으로 부르는 것
- 구성요서 : 게임 오브젝트, 게임 오브젝트 간의 계층 구조

### 게임 오브젝트(GameObject)
- 씬에 배치할 수 있는 최소 단위
- 컴포넌트의 합(최소 하나의 컴포넌트는 있어야 한다. Transform)

### 컴포넌트
- 게임 오브젝트를 구성하는 기능의 최소 단위

### GetComponent
- public T GetComponent<T>() where T : Component;
- 현재 스크립트가 포함된 게임 오브젝트에서 컴포넌트를 불러오는 함수

### AddComponent
- public T AddCompoenet<T>() where T : Component;
- 현재 스크립트가 포함된 게임 오브젝트에 컴포넌트를 추가하는 함수

### Line
- 시작지점, 도착지점
- Position 2개

### Ray
- 시작지점, 방향
- Position 1개, Vector 1개
- Raycast를 하는데 사용 (레이에 닿는 컬라이더를 감지)

### VR(Virual Reality)
- 가상현실
- 주요 특징
    - 헤드 트래킹을 통해 머리의 움직임과 게임 카메라를 연동시켜 현실감과 몰입감을 증가 시킨다.
    - 사람의 눈처럼 서로 떨어져 있는 두개의 카메라를 이용해 각각 랜더링하여 VR기기의 두개의 스크린에 각각 재생한다.
    - 동작 인식을 위해 자이로스코프 센서 사용
