# IDLE_CardMatchGame 카드 맞추기 게임 프로젝트

![TeamLogo](./Assets/Images/Logo_triangle.png/)

</br>

## 팀원: 조병웅(팀장), 박준형, 문원정, 김종욱, 김국민

> Unity로 개발한 카드 맞추기 게임 프로젝트 입니다.  
> 5인 팀 프로젝트이며, 배웠던 내용의 연장선으로 개발한 내용입니다.  ㅁ
> 소통과 협업을 위주로 개발하였고, 개발 기간은 5일입니다.  
> 구현된 내용은 다음과 같습니다.

</br>

## 주요 기능
* 프리뷰 로고
* 배경 음악

* 게임 시작 화면
    * 옵션 플레이어 데이터 삭제 기능
    * 난이도 별 게임 시작
    * 난이도 별 게임 해금 기능(Normal, Hard)

* 게임 진행 화면
    * 카드 등장 애니메이션
    * 카드 랜덤 배치 기능
    * 배경 음악 루프, 매치 시 사운드
    * 일시 정지기능(Retry, Continue, Home)
    * 일정 시간이 지날 시 빨간 경고 점멸
    * 매치 횟수가 일정 수 미만일 때 빨간 색으로 경고
    * 결과 판넬(현재점수, 최고점수, 매치횟수, 홈, 다시하기)



## 기능 세부 설명
* 게임 시작 / 게임 시작 화면  
    * Easy, Normal, Hard 세 가지 난도별로 게임 구분  
    * 옵션에서 게임 종료 및 현재 데이터 삭제
    * 배경 음악  
    
* 게임 진행(Easy)  
    * 4 X 3 카드를 랜덤으로 배치
    * 50초 카운트 진행
    * 40초 카운트 이후 배경음악 속도 2배 및 빨간 경고 점멸  
    * 게임 종료시 실패하면 Failed text 출력, 성공시 현재 점수 및 최고점수 기록판 띄움  

* 게임 진행(Normal)
    * 4 X 6 카드를 랜덤으로 배치
    * 140초 카운트 진행  
    * 120초 카운트 이후 배경음악 속도 2배 및 빨간 경고 점멸  
    * 게임 종료시 실패하면 Failed text 출력, 성공시 현재 점수 및 최고점수 기록판 띄움  

* 게임 진행(Hard)
    * 4 X 6 카드를 랜덤으로 배치
    * 140초 카운트 진행  
    * 120초 카운트 이후 배경음악 속도 2배 및 빨간 경고 점멸 
    * 카드 매치 최대 횟수 30회
    * 게임 종료시 실패하면 Failed text 출력, 성공시 현재 점수 및 최고점수 기록판 띄움  


##  기술 스택

![Unity](https://img.shields.io/badge/-Unity-%23000000?style=flat-square&logo=Unity)
![C#](https://img.shields.io/badge/-C%23-%7ED321?logo=Csharp&style=flat)

## 와이어 프레임

![WierFrame](./Assets/Images/IDLE_wireFrame.png)

## 시연 영상

[프로젝트 시연영상] https://www.youtube.com/watch?v=OXB477_D4LE

## 구현한 기능


### 게임 시작 화면

![GameStartScene](./Assets/Images/GameStartDeactivated.png)

 __옵션__  
 <br/>
![alt](./Assets/Images/OptionFile.png)

* 3가지 난이도로 나뉘어져 있으며 우측 상단에는 옵션버튼이 있음  
* 플레이 초기 화면이기에 Noraml Hard난이도는 잠금, Easy만 활성화  

<br/>

__데이터 삭제__  

![Option](./Assets/Images/OptionDeleteFile.png)  

* 플레이어 데이터 초기화 가능  
* 초기화시 경고문구 출력 및 3초 후 사라짐

<br>

### 게임 진행 공통 부분

__매치 실패 시 +5초 추가__  

![penalty](./Assets/Images/PenaltyFile.png)


__일시정지__  

![Pause](./Assets/Images/PauseFile.png)  
* 일시정지시 시간 멈춤
* 계속진행, 다시하기, 홈으로가기 구현  

<br/>  

__게임 종료시 판넬__  

![EndPanel](./Assets/Images/EndPanelFile.png)  
<br/>

__노말게임 판넬__  

![NormalEndPanel](./Assets/Images/NormalEndPanel.png)
<br/>

* 난도별 최고점수 따로 기록
* 최고점수 및 이번판 점수 출력
* 비교 후 점수가 높으면 최고점수 갱신
* 최고 점수 갱신시 다른 음악 재생 
* 매치 시도 출력
* 홈 다시하기 버튼 구현
 
 <br/>

__특정 카운드 초과시 빨간등 점멸__

![Warning](./Assets/Images/WarningFile.png)

* 모든 난도 일정 시간 초과시 빨간 경고등 점멸


### 게임 진행 화면

<br/>

__Easy 모드__

![EasyGame](./Assets/Images/EasyGameFile.png)  
* 4 X 3 으로 진행  
* 특정 시간 이후 빨간 경고등 점멸  
* 50초 카운트 종료시 최고점수, 매치시도, 이번 판 점수 출력  
* 실패시 Failed 출력  

<br/>

__Normal 해금__

![NormalUnlocked](./Assets/Images/NormalUnlocked.png)  
<br/>

__Normal 모드__  

![NormalGame](./Assets/Images/NormalGameFile.png)  
* Easy 클리어 시 해금  
* 4 X 6 랜덤 카드 배치
* 140초 카운트

<br/>

__Hard 모드해금__  

![HardUnlocked](./Assets/Images/HardUnlocked.png)
<br/>   

__Hard 모드__  

![HardGame](./Assets/Images/HardGameFile.png)  

* Normal 클리어 시 해금
* Hard모드만 특별한 bgm 재생
* 4 X 6 랜덤 카드 배치  
* 140초 카운트 및 매치 수 30회 제한
<br/>

## 프로젝트 시 일어난 문제와 해결  

### 프로젝트

__문제__:  
 
데이터가 지워졌다는 경고문이 사라지지 않았음
구현하기로 한 내용 : 데이터를 삭제 버튼을 누르면 데이터가 지워졌다는 경고가 뜨고 1.5초 후에 알아서 사라지도록 함.
문제가 발생한 상황 : 맨 처음 게임을 실행한 상태에서는 이상 없이 창이 닫혔지만 어떻게든 게임이 끝나거나 게임씬에서 스타트씬으로 돌아온 경우에는 (데이터 유무 관계 없이) 창이 닫히지 않음.
문제 해결을 위해 노력한 것 : 처음 의심은 데이터 여부가 영향을 준다고 생각했음. 해결을 위해 Coroutine 함수 등을 이용해보았으나, 동일한 증상이 반복되어 튜터님과 함께 해당 기능에 대하여 이야기를 나눔  

__결과__:  

 “씬 이동“에 문제가 있는 것 같다는 결론에 도달.
해결방안 : 우선 Invoke 함수가 실행되지 않음을 확인. 그러다 게임씬 시간을 확인해보니 0에 고정이 되어 있었음. timeScale에 대한 이야기 후 게임씬에서 스타트씬을 다시 로드할 때 timeScale을 0으로 만들도록 설정이 되어 있음을 확인. 시간이 고정되면 Invoke 함수가 참고할 시간이 없으므로 작동 불가. 홈으로 돌아가는 버튼에 timeScale을 1로 재설정하는 기능을 추가하여 시간이 다시 흐르도록 함. > 해결!  

### GitHub  

__문제__:  

Branch를 옮길 때 Scene 내부에서 작업한 것끼리 충돌 발생  

__증상__:  

GitHub 내에서 어떤 Scene 살릴 것을 몰라 둘다 사라짐 >> 작업내용이 날아가고, GitHub가 놓아주지 않음… (이동X)
다른 멤버가 대신 branch 삭제하고 GitHub 재접. (카드 뒤집기 모션 X)
브랜치 옮길 때, Bring change  사용 X (Stash로 고정)

## 프로젝트 소감

___조병웅___  

갓 개발의 길에 들어선 저를 팀원분들께서 많이 가르쳐주시고 또 끌어주신 덕분에 정말 많은 공부가 되었습니다:D 더 많이 배워서 다음번에는 더 큰 도움을 드릴 수 있도록 하겠습니다!  
<br/>
___박준형___  
협업과 소통을 하면 안될것도 된다는 것을 알았고 무엇보다 팀원들이 너무 잘해주셔서 편하게 프로젝트를 마무리했습니다  
<br/>
___문원정___  
배우려는 열정이 넘치는 팀원들 덕분에 저도 많이 배웠습니다!!  
<br/>
___김종욱___   
첫 팀 프로젝트로 짧은 시간이었지만 모든 팀원분들 덕에 많이 배우고, 즐거운 시간이었습니다. 감사합니다~  
<br/>
___김국민___  
하루 12시간이 빨리 지나갈만큼 힘들었지만 그만큼 많이 배운거 같습니다!  
<br/>
