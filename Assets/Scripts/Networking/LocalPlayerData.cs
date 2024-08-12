using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ローカル環境の入力データ（プレイヤー名、選択銃士や機銃など）を保存しておく静的クラス。
public static class LocalPlayerData
{
	private static string _nickName;
	public static string NickName
	{
		set => _nickName = value;
		get
		{
			if (string.IsNullOrWhiteSpace(_nickName))
			{
				var rngPlayerNumber = Random.Range(0, 9999);
				_nickName = $"Player {rngPlayerNumber.ToString("0000")}";
			}
			return _nickName;
		}
	}

	private static GunnerData _gunnerData;
	public static GunnerData GunnerData
	{
		set { _gunnerData = value; }
		get { return _gunnerData; }
	}

    private static GunsData _rightGunsData;
    public static GunsData RightGunsData
    {
        set { _rightGunsData = value; }
        get { return _rightGunsData; }
    }

    private static GunsData _leftGunsData;
    public static GunsData LeftGunsData
    {
        set { _leftGunsData = value; }
        get { return _leftGunsData; }
    }

    private static SkillData _skillData;
    public static SkillData SkillData
    {
        set { _skillData = value; }
        get { return _skillData; }
    }
}