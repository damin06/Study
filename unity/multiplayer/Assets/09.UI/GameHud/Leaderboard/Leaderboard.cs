using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class Leaderboard
{
    private VisualElement _root;
    private int _displayCount;
    private VisualElement _innerHolder;
    private Color _ownerColor;

    private VisualTreeAsset _boardItemAsset;
    private List<BoardItem> _itemList = new List<BoardItem>();

    public Leaderboard(VisualElement root, VisualTreeAsset itemAsset, Color ownerColor, int displayCount = 7)
    {
        _root = root;
        _innerHolder = _root.Q<VisualElement>("inner-holder");
        _boardItemAsset = itemAsset;
        _displayCount = displayCount;
        _ownerColor = ownerColor;
    }

    public bool CheckExistByClientID(ulong clientID)
    {
        return _itemList.Any(x => x.ClientID == clientID);
    }

    public void AddItem(LeaderboardEntityState state)
    {
        var root = _boardItemAsset.Instantiate().Q<VisualElement>("board-item");
        _innerHolder.Add(root);
        BoardItem item = new BoardItem(root, state, _ownerColor);
        _itemList.Add(item);
    }

    public void RemoveByClientID(ulong clientID)
    {
        BoardItem item = _itemList.FirstOrDefault(x => x.ClientID == clientID);

        if(item != null)
        {
            item.Root.RemoveFromHierarchy(); //이걸 해주면 UI에서 사라진다.
            _itemList.Remove(item); //이걸해주면 데이터바인드 리스트에서 사라진다.
        }
    }

    public void UpdateByClientID(ulong clientID, int coins)
    {
        //지정된 클라이언트 아이디의 state를 찾아서 coins을 업데이트 시키시오.
        BoardItem item = _itemList.FirstOrDefault(x => x.ClientID == clientID);

        if(item != null)
        {
            item.UpdateCoin(coins);
        }
    }

    public void SortOrder()
    {
        // b-a : 내림, a-b : 오름
        _itemList.Sort((a, b) => b.Coins.CompareTo(a.Coins));

        for(int i = 0; i < _itemList.Count; ++i)
        {
            var item = _itemList[i];
            item.rank = i + 1; //등수 기록하고
            item.Root.BringToFront();
            item.UpdateText();

            item.Show(i < _displayCount); //표현해야할 수보다 작으면 표시

            //자기꺼는 무조건 표시
            if(item.ClientID == NetworkManager.Singleton.LocalClientId)
            {
                item.Show(true);
            }
        }

        

    }
}
