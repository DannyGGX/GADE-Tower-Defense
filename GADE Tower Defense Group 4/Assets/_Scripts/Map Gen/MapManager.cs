using DannyG.Buildings;
using DannyG.Paths;
using UnityEngine;
using UnityUtils;

namespace DannyG
{
	public class MapManager : Singleton<MapManager>
	{
		[SerializeField] private TerrainGenerator terrainGenerator;
		public Border mapBorder;
		[SerializeField] private int overallMapXSize = 100;
		[SerializeField] private int overallMapZSize = 100;
		[SerializeField] private MainTowerCreator mainTowerCreator;
		
		private void Start()
		{
			terrainGenerator.externallyControlled = true;
			terrainGenerator.CreateNewMap();
			mainTowerCreator.PlaceTower();
			PathsManager.Instance.CreatePaths();
		}
		
		
	}
}

