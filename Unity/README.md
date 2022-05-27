# Atentsgame_GameAlgorithm_Lecture_Material

## Unity 관련

### 씬(Scene) = 맵(Map)
- 씬(Scene) : 게임 오브젝트의 합
- 씬에 배치할 수 있는 최소단위는 게임 오브젝트
- 게임 오브젝트 = 컴포넌트(Component)의 합
- 컴포넌트(Component) = 기능의 최소 단위(특정한 기능 하나)

***

## 유니티 움직임 관련
### Input Manager
- 전통적인 유니티 입력 방식
- 폴링(polling)을 사용 -> 키 입력 상태를 매 프레임 확인 -> Busy wait 발생 -> 모바일 게임에서 배터리 소모가 심하다.

### Input System
- Event driven(이벤트 드리븐)
- 이벤트 : 컴퓨터에서 발생하는 모든 일
- 배터리 소모를 아낄 수 있다.

### 벡터
- 힘의 방향과 크기를 표현하는 용어
- Dircetion(방향) + Scalar(크기)

***

## 경계박스

### 원, 캡슐, 박스
- 속도는 원이 제일 빠르다.

***

## Animation

### Animator
- 애니메이션을 재생하기 위해 필요한 컴포넌트

### Animator Contoller
- Animator가 어떤 애니메이션을 재생할지 결정하는 에셋(파일)

### Animation Clip
- 실제 애니메이션 하나

### Animator Contoller에는 여러개의 Animation Clip가 있음.
