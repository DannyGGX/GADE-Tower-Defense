using DannyG.Buildings;
using UnityEngine;
using UnityUtils;

namespace DannyG
{
	public class MapManager : Singleton<MapManager>
	{
		[SerializeField] private TerrainGenerator terrainGenerator;
		[SerializeField] private int overallMapXSize = 100;
		[SerializeField] private int overallMapZSize = 100;
		[SerializeField] private MainTowerCreator mainTowerCreator;
		
		private void Start()
		{
			terrainGenerator.externallyControlled = true;
			terrainGenerator.CreateNewMap();
			mainTowerCreator.PlaceTower();
		}
		
		
	}
}

