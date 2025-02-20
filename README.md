# 프로젝트 이름

## 📖 목차
1. [프로젝트 소개](#프로젝트-소개)
2. [주요기능](#주요기능)
3. [개발기간](#개발기간)
12. [Trouble Shooting](#trouble-shooting)
    
## 👨‍🏫 프로젝트 소개
Unity 메타버스 제작


## 💜 주요기능

- 기능 1 캐릭터가 wasd로 움직이고, 움직임에 따라 카메라가 함께 이동

- 기능 2 여신상에 접촉시, 스코어보드(미니게임 최고점수) UI 표시

- 기능 3 북상단에 제단에 접촉시, 버튼으로 미니게임 선택 1. FlappyPlane 2. Stack(9:16)

- 기능 4 우측 비석에 있는 NPC 접촉시 이동해야 할 곳 힌트 제공


## ⏲️ 개발기간
- 2024.02.13(월) ~ 2024.02.20(목)

## Trouble Shooting
1. Stack 미니게임 제작 중 토대인 첫 블럭이 움직임
   -> 오탈자 확인 및 빠진 구문 확인 -> Stackbounds.x -= DeltaX 구문이 빠져있었으나, 초석의 문제는 아니였음.
   -> 주석처리를 하며, 문제의 구문을 확인 -> MoveStack() 이후 동작의 오류를 발견 -> 메서드를 다시 봤더니, SpawnBlock()를 통해 큐브를 제작하고, 이를 움직이는 것을 MoveStack()에서 작업하는 것을 확인 -> SpawnBlock()을 두번 실행시켜, 첫 블럭을 그냥 움직임 없이 설치하면 해결되는 문제

2. 제작을 하는 과정 중 실행을 하며, 결과를 즉시적으로 보고싶어하는 경향이 있음. 이가 스크립트를 수정할 때엔 문제가 없으나, Unity 컴포넌트를 만질때에는 중지시 롤백된다.
   -> 재생 켜둔것을 잊고, UI 제작을 하다가 구현을 보기위해 시작을 했으나 작업한 내용이 다 사라져서, NullExceptionError 가 떴음. -> 다시 제작했다..

3. Flappy 미니게임을 제작하며, 아래와 같은 구문 제작 -> 클릭을 해도 점프가 작동되지 않았음 (IsFlap = true가 작동 안함) -> else if 구문이 if(IsDead)와 연결이 되어야하는데, if(deathCooldown<=0)와 연결이 되어있었음 -> 줄 맞춤을 다시 해서, 다시 if문에 맞춰 놓음
   -> 제작을 위해 빈 곳을 두다보니 이처럼 실수할 수도 있다는걸 자각# 프로젝트 이름

## 📖 목차
1. [프로젝트 소개](#프로젝트-소개)
2. [주요기능](#주요기능)
3. [개발기간](#개발기간)
4. [Trouble Shooting](#trouble-shooting)
5. [출처]
   
## 👨‍🏫 프로젝트 소개
Unity 메타버스 제작


## 💜 주요기능

- 기능 1 캐릭터가 wasd로 움직이고, 움직임에 따라 카메라가 함께 이동

- 기능 2 여신상에 접촉시, 스코어보드(미니게임 최고점수) UI 표시

- 기능 3 북상단에 제단에 접촉시, 버튼으로 미니게임 선택 1. FlappyPlane 2. Stack(9:16)

- 기능 4 우측 비석에 있는 NPC 접촉시 이동해야 할 곳 힌트 제공


## ⏲️ 개발기간
- 2024.02.13(월) ~ 2024.02.20(목)

## Trouble Shooting
1. Stack 미니게임 제작 중 토대인 첫 블럭이 움직임
   -> 오탈자 확인 및 빠진 구문 확인 -> Stackbounds.x -= DeltaX 구문이 빠져있었으나, 초석의 문제는 아니였음.
   -> 주석처리를 하며, 문제의 구문을 확인 -> MoveStack() 이후 동작의 오류를 발견 -> 메서드를 다시 봤더니, SpawnBlock()를 통해 큐브를 제작하고, 이를 움직이는 것을 MoveStack()에서 작업하는 것을 확인 -> SpawnBlock()을 두번 실행시켜, 첫 블럭을 그냥 움직임 없이 설치하면 해결되는 문제

2. 제작을 하는 과정 중 실행을 하며, 결과를 즉시적으로 보고싶어하는 경향이 있음. 이가 스크립트를 수정할 때엔 문제가 없으나, Unity 컴포넌트를 만질때에는 중지시 롤백된다.
   -> 재생 켜둔것을 잊고, UI 제작을 하다가 구현을 보기위해 시작을 했으나 작업한 내용이 다 사라져서, NullExceptionError 가 떴음. -> 다시 제작했다..

3. Flappy 미니게임을 제작하며, 아래와 같은 구문 제작 -> 클릭을 해도 점프가 작동되지 않았음 (IsFlap = true가 작동 안함) -> else if 구문이 if(IsDead)와 연결이 되어야하는데, if(deathCooldown<=0)와 연결이 되어있었음 -> 줄 맞춤을 다시 해서, 다시 if문에 맞춰 놓음
   -> 제작을 위해 빈 곳을 두다보니 이처럼 실수할 수도 있다는걸 자각
   
 if (IsDead)
{
    if (deathCooldown <= 0)
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {

        }
        else
        {
            deathCooldown -= Time.deltaTime;
        }
    }
}
else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)))
{
    IsFlap = true;
}

4. 오브젝트의 움직임에 따라 카메라를 이동시키는 스크립트 제작 -> 카메라가 움직이질 않음 -> Target에 하위 개체를 넣어봤더니 움직임 -> 스크립트 및 컴포넌트를 상위 개체가 아닌 하위 개체에 넣었고, Target에는 상위 개체를 넣음 -> 스크립트 및 컴포넌트를 상위 개체로 옮겨줌    

5. Collider 컴포넌트 이슈
   Bg Looper를 이용해 배경 및 장애물들을 회수해, 진행하는 방향 정면에 붙여줘야함 -> 장애물은 이동이 되나, 배경이 움직이질 않음 -> 충돌의 부재를 깨닫고, 배경에 컴포넌트 추가 -> 캐릭터가 배경을 뚫고 천장으로 올라감 -> Collider 중 IsTrigger를 통해, Enter시에, 회수 하도록 작성했으나
   IsTrigger를 안 켰음 -> 천장과 바닥을 뚫음 -> 배경에서 천장과 바닥 역할을 하는 오브젝트도 넣었으나, 이에 Polygon Collider를 IsTrigger를 켜버림 -> Polygon의 IsTrigger를 꺼주고 Box Collider를 컴포넌트에 넣어 다시 충돌 처리를 해줬음 -> 해결 완료 (해당 충돌 컴포넌트는 모두 2D)

##사용한 에셋 출처
맵 https://assetstore.unity.com/packages/2d/environments/pixel-art-top-down-basic-187605
캐릭터 https://assetstore.unity.com/packages/2d/characters/miniature-army-2d-v-1-medieval-style-72935
