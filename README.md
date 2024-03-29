Prohibition
====
> 금주법이 공표된 세상에서 술을 팔아 큰돈을 벌고자 하는 사람의 이야기 입니다.

> 낮에는 술을 팔고, 밤에는 낮에 판매할 물품들을 구매하거나 강화할 수 있습니다.
- - -
# 1. 낮
술을 손님에게 판매하는 장사시간이다.
## 1.1. 장사시간
4분으로 설정 되어 있으며, 4분이 지난 뒤에는 손님이 더이상 가게로 들어오지 않는다.
장사시간이 끝난 뒤 손님이 남아 있는 경우 밤 시간으로 넘어가지 않으며, 가게 안에 손님이 없어야 밤 시간으로 넘어갈 수 있다.
## 1.2. 손님
장사시간에 가게에 들어오며 술과 음식을 주문한다.
일정시간 동안 술과 음식 모두 제공하지 않으면 손님은 화를 내며 가게를 나간다.
손님이 가게 안으로 들어오면 비어 있는 자리에 앉는다.

# 2. 밤
낮에 판매할 물건을 보충하거나 강화하는 시간이다.
## 2.1. 상점
가득 쌓여있는 상자 근처에 가서 상호작용을 하면 물건을 구매할 수 있다.
## 2.2. 강화
중앙 상단에 위치한 곳으로 가면 판매할 물품들을 더 비싼 값에 판매할 수 있도록 연구한다.
## 2.3. 빚 납부
중앙 하단의 테이블로 가면 마피아에게 빌린 돈을 갚을 수 있다.
## 2.4. 장사시작
우측 하단의 차량으로 가면 가게로 돌아가 장사를 시작 할 수 있다.

# 3. 공용
밤낮 상관없이 사용하는 기능 모음
## 3.1. 인벤토리
Tab키를 누르는 동안 자신이 가지고 있는 물건, 돈을 볼 수 있다.
## 3.2. 이동
W,S,A,D 키로 상하좌우 움직일 수 있다.

---
SecondMain 브런치에 최신화가 되어있습니다.
---------------------

### 코드리뷰 - 이형석

- NPCSpawner - Prohibition/Assets/LHS/Scripts/NPCSpawner.cs
<주의점> 조건에 따른 코루틴을 호출하는 것이 맞는 것인지 확인 부탁드립니다, 코드 작성 과정에서 주석 처리가 많이 되어 있는데 정리를 위해 반드시 삭제해야 하는지 조언 부탁드리겠습니다.
- NPCInteraction - Prohibition/Assets/LHS/Scripts/NPCInteraction.cs
<주의점> 현재 플레이어와의 상호작용을 충돌할 때 태그 확인을 조건으로 두었는데 현재 시점에서 좋은 방법인지 확인 부탁드리겠습니다.
- NPCController - Prohibition/Assets/LHS/Scripts/NPCController.cs
<주의점> NPC의 움직임을 관리하지만, 동시에 자가파괴하는 역할을 맡고 있습니다. 자가파괴를 NPC 오브젝트가 직접 처리하는 것이 옳은 것인가요? 조언부탁드리겠습니다.

### 코드리뷰 - 장영준

1. Prohibition/Assets/JangYeongjun/Scripts/Use/Managers/InteractionManager.cs
   - 오브젝트의 collider에 들어갔을 때 Trigger에 의해서 보내지는 Index값에 따라 나타나는 판넬이 달라지는데 숫자하니까 게임이 복잡해 졌을 때 뭔가 불안합니다.
2. Prohibition/Assets/JangYeongjun/Scripts/Use/Store/StoreChanger.cs
   - Find랑 Get을 너무 많이 사용하는 것 같은데 그냥 slot을 미리 만들어 놓고 이미지만 바꾸는 방식으로 할지 고민입니다.
3. Prohibition/Assets/JangYeongjun/Scripts/Use/Game/DayController.cs
   - 일단 낮과 밤의 전환을 코루틴으로 구현을 해 봤는데 괜찮은지 궁금합니다.
  
### 코드리뷰 - 김준영
1. Prohibition/Assets/KJY_chara/Scripts/Inventory.cs
   - TemporaryDataManager.cs에서 현재 가지고 있는 gold에 대한 정보를 받아서 haveGold 변수에 넣어 사용하는데 변수를 따로 할당하지 않고 사용하는게 더 효율적인지 궁금합니다.
2. Prohibition/Assets/KJY_chara/Scripts/Penalties.cs
   - 데이터를 너무 직접적으로 건들여 다른 문제가 생기는 것은 아닌지 걱정입니다.
