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
            item.Root.RemoveFromHierarchy(); //�̰� ���ָ� UI���� �������.
            _itemList.Remove(item); //�̰����ָ� �����͹��ε� ����Ʈ���� �������.
        }
    }

    public void UpdateByClientID(ulong clientID, int coins)
    {
        //������ Ŭ���̾�Ʈ ���̵��� state�� ã�Ƽ� coins�� ������Ʈ ��Ű�ÿ�.
        BoardItem item = _itemList.FirstOrDefault(x => x.ClientID == clientID);

        if(item != null)
        {
            item.UpdateCoin(coins);
        }
    }

    public void SortOrder()
    {
        // b-a : ����, a-b : ����
        _itemList.Sort((a, b) => b.Coins.CompareTo(a.Coins));

        for(int i = 0; i < _itemList.Count; ++i)
        {
            var item = _itemList[i];
            item.rank = i + 1; //��� ����ϰ�
            item.Root.BringToFront();
            item.UpdateText();

            item.Show(i < _displayCount); //ǥ���ؾ��� ������ ������ ǥ��

            //�ڱⲨ�� ������ ǥ��
            if(item.ClientID == NetworkManager.Singleton.LocalClientId)
            {
                item.Show(true);
            }
        }

        

    }
}
