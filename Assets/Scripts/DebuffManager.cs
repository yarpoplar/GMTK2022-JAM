using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class DebuffManager : MonoBehaviour
{
	public enum Debuff
	{
		None,
		FastEnemies,
		SlowerPlayer,
		SlowerWeapons,
		CloserCamera,
		ExplosingEnemies,
	}

	public Debuff currentDebuff;

	public void ResetAll()
	{
		switch (currentDebuff)
		{
			case Debuff.FastEnemies:
				// code block
				break;
			case Debuff.SlowerPlayer:
				GameManager.Instance.Player.GetComponent<Player>().moveSpeed *= 1.33f;
				break;
			case Debuff.SlowerWeapons:
				foreach (Weapon weapon in GameManager.Instance.playerWeapons)
				{
					weapon.fireSpeed *= .5f;
				}
				break;
			case Debuff.CloserCamera:
				Camera.main.gameObject.transform.parent.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens.FieldOfView = 60;
				break;
			case Debuff.ExplosingEnemies:
				// code block
				break;
			case Debuff.None:
				break;
		}
	}

	public void MakePlayerSlower()
	{
		GameManager.Instance.Player.GetComponent<Player>().moveSpeed *= .75f;
		currentDebuff = Debuff.SlowerPlayer;
	}

	public void MakeWeaponsSlower()
	{
		foreach (Weapon weapon in GameManager.Instance.playerWeapons)
		{
			weapon.fireSpeed *= 2;
		}
		currentDebuff = Debuff.SlowerWeapons;
	}

	public void MakeCameraCloser()
	{
		Camera.main.gameObject.transform.parent.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens.FieldOfView = 40;
		currentDebuff = Debuff.CloserCamera;
	}
}
