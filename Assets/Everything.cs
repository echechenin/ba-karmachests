using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Everything : MonoBehaviour {
	public Text[] freeChestChancesView;
	public Text[] battleChestChancesView;
	public Text[] donate1ChancesView;
	public Text[] donate2ChancesView;
	public Text[] donate3ChancesView;
	public Text leagueView;
	public Text karmaView;
	public Text logView;
	public Text donateSimulation;


	private Dictionary<int,float[]> freeChestChances = new Dictionary<int,float[]> ();
	private Dictionary<int,float[]> battleChestChances = new Dictionary<int,float[]> ();
	private Dictionary<int,float[]> donate1ChestChances = new Dictionary<int,float[]> ();
	private Dictionary<int,float[]> donate2ChestChances = new Dictionary<int,float[]> ();
	private Dictionary<int,float[]> donate3ChestChances = new Dictionary<int,float[]> ();
	private float[] prices;

	private int league;
	private float karma;
	private string log;

	private float[] currentFreeChestChances = new float[5];
	private float[] currentBattleChestChances = new float[5];
	private float[] currentDonate1ChestChances = new float[5];
	private float[] currentDonate2ChestChances = new float[5];
	private float[] currentDonate3ChestChances = new float[5];

	private int donate3chestsOpened = 0;
	private int legendariesDropped = 0;

	// Use this for initialization
	void Start () {
		league = 7;
		karma = 0;
		log = "";

		freeChestChances.Add (1, new float[] { 4541, 2211, 3248, 0, 0 });
		freeChestChances.Add (2, new float[] { 5449, 405, 3989, 248, 0 });
		freeChestChances.Add (3, new float[] { 6812, 0, 2878, 310, 0 });
		freeChestChances.Add (4, new float[] { 9082, 0, 497, 414, 7 });
		freeChestChances.Add (5, new float[] { 2488, 0, 6933, 569, 10 });
		freeChestChances.Add (6, new float[] { 7030, 0, 2180, 776, 14 });
		freeChestChances.Add (7, new float[] { 2706, 0, 6242, 1034, 18 });
		battleChestChances.Add (1, new float[] { 4986, 4294, 720, 0, 0 });
		battleChestChances.Add (2, new float[] { 7983, 0, 1198, 819, 0 });
		battleChestChances.Add (3, new float[]{ 2479, 417, 6080, 1024, 0 });
		battleChestChances.Add (4, new float[]{ 8611, 0, 0, 1365, 24 });
		battleChestChances.Add (5, new float[]{ 1212, 0, 6877, 1877, 34 });
		battleChestChances.Add (6, new float[]{ 6198, 996, 200, 2560, 46 });
		battleChestChances.Add (7, new float[] { 4931, 0, 1595, 3413, 61 });
		donate1ChestChances.Add (1, new float[]{ 6251, 0, 3749, 0, 0 });
		donate1ChestChances.Add (2, new float[]{ 278, 0, 7658, 2064, 0 });
		donate1ChestChances.Add (3, new float[]{ 4306, 1735, 1689, 2270, 0 });
		donate1ChestChances.Add (4, new float[]{ 7485, 0, 0, 2476, 39 });
		donate1ChestChances.Add (5, new float[]{ 2362, 0, 4912, 2683, 43 });
		donate1ChestChances.Add (6, new float[]{ 6390, 343, 332, 2889, 46 });
		donate1ChestChances.Add (7, new float[]{ 418, 3225, 3213, 3095, 49 });
		donate2ChestChances.Add (1, new float[]{ 418, 6369, 3213, 0, 0 });
		donate2ChestChances.Add (2, new float[]{ 557, 0, 5316, 4127, 0 });	
		donate2ChestChances.Add (3, new float[]{ 696, 2124, 2021, 5159, 0 });
		donate2ChestChances.Add (4, new float[]{ 835, 0, 2876, 6191, 98 });
		donate2ChestChances.Add (5, new float[]{ 974, 858, 830, 7223, 115 });
		donate2ChestChances.Add (6, new float[]{ 1114, 0, 501, 8254, 131 });
		donate2ChestChances.Add (7, new float[]{ 567, 0, 0, 9286, 147 });
		donate3ChestChances.Add (1, new float[]{ 1671, 5478, 2851, 0, 0 });
		donate3ChestChances.Add (2, new float[]{ 2784, 0, 6580, 636, 0 });
		donate3ChestChances.Add (3, new float[]{ 1110, 0, 0, 8890, 0 });
		donate3ChestChances.Add (4, new float[]{ 2265, 0, 0, 7145, 590 });
		donate3ChestChances.Add (5, new float[]{ 3880, 0, 0, 5399, 721 });
		donate3ChestChances.Add (6, new float[]{ 5494, 0, 0, 3654, 852 });
		donate3ChestChances.Add (7, new float[]{ 7109, 0, 0, 1908, 983 });
		prices = new float[] { 0.41f, 0.03f, 0.17f, 2.38f, 67.57f };


		for (int i = 0; i < freeChestChancesView.Length; i++) {
			freeChestChancesView [i].text = probToPercent (freeChestChances [league] [i]);
			battleChestChancesView [i].text = probToPercent (battleChestChances [league] [i]);
			donate1ChancesView [i].text = probToPercent (donate1ChestChances [league] [i]);
			donate2ChancesView [i].text = probToPercent (donate2ChestChances [league] [i]);
			donate3ChancesView [i].text = probToPercent (donate3ChestChances [league] [i]);
		}
			
		leagueView.text = "League: " + league.ToString ();
		karmaView.text = "Karma: " + karma.ToString ();
		logView.text = log;

		for (int i = 0; i < currentFreeChestChances.Length; i++) {
			currentFreeChestChances [i] = freeChestChances [league] [i];
			currentBattleChestChances [i]= battleChestChances [league][i];
			currentDonate1ChestChances [i]= donate1ChestChances [league][i];
			currentDonate2ChestChances [i]= donate2ChestChances [league][i];
			currentDonate3ChestChances [i]= donate3ChestChances [league][i];
		}

	}

	public void leagueUp() {
		if (league < 7) {
			league++;
			leagueView.text = "League: " + league.ToString ();
			karmaUpdate ();
			updateView ();
		}
	}

	public void leagueDown() {
		if (league > 1) {
			league--;
			leagueView.text = "League: " + league.ToString ();
			karmaUpdate ();
			updateView ();

		}
	}


	public void openFreeChest() {
		//calculating default average drop for karma
		float chestAverage = 0f;
		float summaryChances = 0;
		for (int i = 0; i < freeChestChances[league].Length; i++) {
			summaryChances += freeChestChances[league] [i];
		}

		for (int i = 0; i < freeChestChances[league].Length; i++) {
			chestAverage += freeChestChances[league] [i] * prices [i] / summaryChances;
		}

		summaryChances = 0f;
		for (int i = 0; i < currentFreeChestChances.Length; i++) {
			summaryChances += currentFreeChestChances [i];
		}

		float random = Random.Range (0, summaryChances);

		for (int i = 0; i < currentFreeChestChances.Length; i++) {
			Debug.Log (i + ": " + currentFreeChestChances [i]);
		}

		//using CURRENT chances to know which item to drop (and adjust karma)
		if (random < currentFreeChestChances [0]) {
			//shard dropped
			karma += prices [0] - chestAverage;
			log += "FreeChest: Shard dropped \t";
		} else if (random < currentFreeChestChances [0] + currentFreeChestChances [1]) {
			//common dropped
			karma += prices [1] - chestAverage;
			log += "FreeChest: Common dropped \t";
		} else if (random < currentFreeChestChances [0] + currentFreeChestChances [1] + currentFreeChestChances [2]) {
			//rare dropped
			karma += prices [2] - chestAverage;
			log += "FreeChest: Rare dropped \t";
		} else if (random < currentFreeChestChances [0] + currentFreeChestChances [1] + currentFreeChestChances [2] + currentFreeChestChances [3]) {
			//epic dropped
			karma += prices [3] - chestAverage;
			log += "FreeChest: Epic dropped \t";
		} else {
			//legendary dropped
			karma += prices [4] - chestAverage;
			log += "FreeChest: LEGENDARY dropped \t";
			legendariesDropped++;
		}
		karmaUpdate ();
		updateView ();
	}

	public void openBattleChest() {

		//calculating default average drop for karma
		float chestAverage = 0f;
		float summaryChances = 0;
		for (int i = 0; i < battleChestChances[league].Length; i++) {
			summaryChances += battleChestChances[league] [i];
		}

		for (int i = 0; i < battleChestChances[league].Length; i++) {
			chestAverage += battleChestChances[league] [i] * prices [i] / summaryChances;
		}

		summaryChances = 0f;
		for (int i = 0; i < currentBattleChestChances.Length; i++) {
			summaryChances += currentBattleChestChances [i];
		}

		float random = Random.Range (0, summaryChances);


		//using CURRENT chances to know which item to drop (and adjust karma)
		if (random < currentBattleChestChances [0]) {
			//shard dropped
			karma += prices [0] - chestAverage;
			log += "battleChest: Shard dropped \t";
		} else if (random < currentBattleChestChances [0] + currentBattleChestChances [1]) {
			//common dropped
			karma += prices [1] - chestAverage;
			log += "battleChest: Common dropped \t";
		} else if (random < currentBattleChestChances [0] + currentBattleChestChances [1] + currentBattleChestChances [2]) {
			//rare dropped
			karma += prices [2] - chestAverage;
			log += "battleChest: Rare dropped \t";
		} else if (random < currentBattleChestChances [0] + currentBattleChestChances [1] + currentBattleChestChances [2] + currentBattleChestChances [3]) {
			//epic dropped
			karma += prices [3] - chestAverage;
			log += "battleChest: Epic dropped \t";
		} else {
			//legendary dropped
			karma += prices [4] - chestAverage;
			log += "battleChest: LEGENDARY dropped \t";
			legendariesDropped++;

		}
		karmaUpdate ();
		updateView ();
	}

	public void opendonate1Chest() {

		//calculating default average drop for karma
		float chestAverage = 0f;
		float summaryChances = 0;
		for (int i = 0; i < donate1ChestChances[league].Length; i++) {
			summaryChances += donate1ChestChances[league] [i];
		}

		for (int i = 0; i < donate1ChestChances[league].Length; i++) {
			chestAverage += donate1ChestChances[league] [i] * prices [i] / summaryChances;
		}

		summaryChances = 0f;
		for (int i = 0; i < currentDonate1ChestChances.Length; i++) {
			summaryChances += currentDonate1ChestChances [i];
		}

		float random = Random.Range (0, summaryChances);


		//using CURRENT chances to know which item to drop (and adjust karma)
		if (random < currentDonate1ChestChances [0]) {
			//shard dropped
			karma += prices [0] - chestAverage;
			log += "donate1Chest: Shard dropped \t";
		} else if (random < currentDonate1ChestChances [0] + currentDonate1ChestChances [1]) {
			//common dropped
			karma += prices [1] - chestAverage;
			log += "donate1Chest: Common dropped \t";
		} else if (random < currentDonate1ChestChances [0] + currentDonate1ChestChances [1] + currentDonate1ChestChances [2]) {
			//rare dropped
			karma += prices [2] - chestAverage;
			log += "donate1Chest: Rare dropped \t";
		} else if (random < currentDonate1ChestChances [0] + currentDonate1ChestChances [1] + currentDonate1ChestChances [2] + currentDonate1ChestChances [3]) {
			//epic dropped
			karma += prices [3] - chestAverage;
			log += "donate1Chest: Epic dropped \t";
		} else {
			//legendary dropped
			karma += prices [4] - chestAverage;
			log += "donate1Chest: LEGENDARY dropped \t";
			legendariesDropped++;

		}
		karmaUpdate ();
		updateView ();
	}

	public void opendonate2Chest() {

		//calculating default average drop for karma
		float chestAverage = 0f;
		float summaryChances = 0;
		for (int i = 0; i < donate2ChestChances[league].Length; i++) {
			summaryChances += donate2ChestChances[league] [i];
		}

		for (int i = 0; i < donate2ChestChances[league].Length; i++) {
			chestAverage += donate2ChestChances[league] [i] * prices [i] / summaryChances;
		}

		summaryChances = 0f;
		for (int i = 0; i < currentDonate2ChestChances.Length; i++) {
			summaryChances += currentDonate2ChestChances [i];
		}

		float random = Random.Range (0, summaryChances);


		//using CURRENT chances to know which item to drop (and adjust karma)
		if (random < currentDonate2ChestChances [0]) {
			//shard dropped
			karma += prices [0] - chestAverage;
			log += "donate2Chest: Shard dropped \t";
		} else if (random < currentDonate2ChestChances [0] + currentDonate2ChestChances [1]) {
			//common dropped
			karma += prices [1] - chestAverage;
			log += "donate2Chest: Common dropped \t";
		} else if (random < currentDonate2ChestChances [0] + currentDonate2ChestChances [1] + currentDonate2ChestChances [2]) {
			//rare dropped
			karma += prices [2] - chestAverage;
			log += "donate2Chest: Rare dropped \t";
		} else if (random < currentDonate2ChestChances [0] + currentDonate2ChestChances [1] + currentDonate2ChestChances [2] + currentDonate2ChestChances [3]) {
			//epic dropped
			karma += prices [3] - chestAverage;
			log += "donate2Chest: Epic dropped \t";
		} else {
			//legendary dropped
			karma += prices [4] - chestAverage;
			log += "donate2Chest: LEGENDARY dropped \t";
			legendariesDropped++;

		}
		karmaUpdate ();
		updateView ();
	}

	public void opendonate3Chest() {
		donate3chestsOpened++;
		//calculating default average drop for karma
		float chestAverage = 0f;
		float summaryChances = 0;
		for (int i = 0; i < donate3ChestChances[league].Length; i++) {
			summaryChances += donate3ChestChances[league] [i];
		}
		for (int i = 0; i < donate3ChestChances[league].Length; i++) {
			chestAverage += donate3ChestChances[league] [i] * prices [i] / summaryChances;
		}

		summaryChances = 0f;
		for (int i = 0; i < currentDonate3ChestChances.Length; i++) {
			summaryChances += currentDonate3ChestChances [i];
		}			
		float random = Random.Range (0, summaryChances);


		//using CURRENT chances to know which item to drop (and adjust karma)
		if (random < currentDonate3ChestChances [0]) {
			//shard dropped
			karma += prices [0] - chestAverage;
			log += "donate3Chest: Shard dropped \t";
		} else if (random < currentDonate3ChestChances [0] + currentDonate3ChestChances [1]) {
			//common dropped
			karma += prices [1] - chestAverage;
			log += "donate3Chest: Common dropped \t";
		} else if (random < currentDonate3ChestChances [0] + currentDonate3ChestChances [1] + currentDonate3ChestChances [2]) {
			//rare dropped
			karma += prices [2] - chestAverage;
			log += "donate3Chest: Rare dropped \t";
		} else if (random < currentDonate3ChestChances [0] + currentDonate3ChestChances [1] + currentDonate3ChestChances [2] + currentDonate3ChestChances [3]) {
			//epic dropped
			karma += prices [3] - chestAverage;
			log += "donate3Chest: Epic dropped \t";
		} else {
			//legendary dropped
			karma += prices [4] - chestAverage;
			log += "donate3Chest: LEGENDARY dropped \t";
			legendariesDropped++;

		}
		karmaUpdate ();
		updateView ();
	}


	private string probToPercent(float probability) {
		float f = Mathf.Round(probability) / 100f;
		return f.ToString () + "%";	
	}

	private void karmaUpdate() {
		//calculating average for computing karma changes
		float chestAverage = 0f;
		float summaryChances = 0;
		for (int i = 0; i < freeChestChances[league].Length; i++) {
			summaryChances += freeChestChances[league] [i];
		}
		for (int i = 0; i < freeChestChances[league].Length; i++) {
			chestAverage += freeChestChances[league] [i] * prices [i] / summaryChances;
		}
		//updating current chances for next chests
		for (int i = 0; i < currentFreeChestChances.Length; i++) {				
			if (prices [i] - chestAverage >= 0) {
				currentFreeChestChances [i] = freeChestChances [league] [i]* (1 - karma / prices [4]);
				if (currentFreeChestChances [i] < 0)
					currentFreeChestChances [i] = 0;
			} else {
				currentFreeChestChances [i] = freeChestChances [league] [i]* (1 + karma / prices [4]);
			}
		}

		//calculating average for computing karma changes
		chestAverage = 0f;
		summaryChances = 0;
		for (int i = 0; i < battleChestChances[league].Length; i++) {
			summaryChances += battleChestChances[league] [i];
		}
		for (int i = 0; i < battleChestChances[league].Length; i++) {
			chestAverage += battleChestChances[league] [i] * prices [i] / summaryChances;
		}
		//updating current chances for next chests
		for (int i = 0; i < currentBattleChestChances.Length; i++) {				
			if (prices [i] - chestAverage >= 0) {
				currentBattleChestChances [i] = battleChestChances [league] [i]* (1 - karma / prices [4]);
				if (currentBattleChestChances [i] < 0)
					currentBattleChestChances [i] = 0;
			} else {
				currentBattleChestChances [i] = battleChestChances [league] [i]* (1 + karma / prices [4]);
			}
		}

		//calculating average for computing karma changes
		chestAverage = 0f;
		summaryChances = 0;
		for (int i = 0; i < donate2ChestChances[league].Length; i++) {
			summaryChances += donate2ChestChances[league] [i];
		}
		for (int i = 0; i < donate2ChestChances[league].Length; i++) {
			chestAverage += donate2ChestChances[league] [i] * prices [i] / summaryChances;
		}
		//updating current chances for next chests
		for (int i = 0; i < currentDonate2ChestChances.Length; i++) {				
			if (prices [i] - chestAverage >= 0) {
				currentDonate2ChestChances [i] = donate2ChestChances [league] [i]* (1 - karma / prices [4]);
				if (currentDonate2ChestChances [i] < 0)
					currentDonate2ChestChances [i] = 0;
			} else {
				currentDonate2ChestChances [i] = donate2ChestChances [league] [i]* (1 + karma / prices [4]);
			}
		}

		//calculating average for computing karma changes
		chestAverage = 0f;
		summaryChances = 0;
		for (int i = 0; i < donate1ChestChances[league].Length; i++) {
			summaryChances += donate1ChestChances[league] [i];
		}
		for (int i = 0; i < donate1ChestChances[league].Length; i++) {
			chestAverage += donate1ChestChances[league] [i] * prices [i] / summaryChances;
		}
		//updating current chances for next chests
		for (int i = 0; i < currentDonate1ChestChances.Length; i++) {				
			if (prices [i] - chestAverage >= 0) {
				currentDonate1ChestChances [i] = donate1ChestChances [league] [i]* (1 -  karma / prices [4]);
				if (currentDonate1ChestChances [i] < 0)
					currentDonate1ChestChances [i] = 0;
			} else {
				currentDonate1ChestChances [i] = donate1ChestChances [league] [i]* (1 + karma / prices [4]);
			}
		}

		//calculating average for computing karma changes
		chestAverage = 0f;
		summaryChances = 0;
		for (int i = 0; i < donate3ChestChances[league].Length; i++) {
			summaryChances += donate3ChestChances[league] [i];
		}
		for (int i = 0; i < donate3ChestChances[league].Length; i++) {
			chestAverage += donate3ChestChances[league] [i] * prices [i] / summaryChances;
		}
		//updating current chances for next chests
		for (int i = 0; i < currentDonate3ChestChances.Length; i++) {				
			if (prices [i] - chestAverage >= 0) {
				currentDonate3ChestChances [i] = donate3ChestChances [league] [i]* (1 - karma/ prices [4]);
				if (currentDonate3ChestChances [i] < 0)
					currentDonate3ChestChances [i] = 0;
			} else {
				currentDonate3ChestChances [i] = donate3ChestChances [league] [i]* (1 + karma / prices [4]);
			}
		}
	}

	private void updateView() {
		karmaView.text = "Karma: " + karma.ToString ();
		logView.text = log;
		log = "";

		float summaryChances = 0;
		for (int i = 0; i < currentFreeChestChances.Length; i++) {
			summaryChances += currentFreeChestChances [i];
		}

		for (int i = 0; i < currentFreeChestChances.Length; i++) {
			freeChestChancesView [i].text = probToPercent (currentFreeChestChances [i]/summaryChances *10000);
		}

		summaryChances = 0;
		for (int i = 0; i < currentBattleChestChances.Length; i++) {
			summaryChances += currentBattleChestChances [i];
		}

		for (int i = 0; i < currentBattleChestChances.Length; i++) {
			battleChestChancesView [i].text = probToPercent (currentBattleChestChances [i]/summaryChances *10000);
		}

		summaryChances = 0;
		for (int i = 0; i < currentDonate1ChestChances.Length; i++) {
			summaryChances += currentDonate1ChestChances [i];
		}

		for (int i = 0; i < currentDonate1ChestChances.Length; i++) {
			donate1ChancesView [i].text = probToPercent (currentDonate1ChestChances [i]/summaryChances *10000);
		}

		summaryChances = 0;
		for (int i = 0; i < currentDonate2ChestChances.Length; i++) {
			summaryChances += currentDonate2ChestChances [i];
		}

		for (int i = 0; i < currentDonate2ChestChances.Length; i++) {
			donate2ChancesView [i].text = probToPercent (currentDonate2ChestChances [i]/summaryChances *10000);
		}

		summaryChances = 0;
		for (int i = 0; i < currentDonate3ChestChances.Length; i++) {
			summaryChances += currentDonate3ChestChances [i];
		}

		for (int i = 0; i < currentDonate3ChestChances.Length; i++) {
			donate3ChancesView [i].text = probToPercent (currentDonate3ChestChances [i]/summaryChances *10000);
		}

		donateSimulation.text = "Donate3 chests opened: " + donate3chestsOpened + "\n Legendaries dropped: " + legendariesDropped;
	}

	public void open1000donate3Chests() {
		for (int i = 0; i < 1000; i++) {
			opendonate3Chest ();
		}
	}
}
