using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMapArea {

	public static readonly NavigationMapArea NULL = new NavigationMapArea("NULL");
	public static readonly NavigationMapArea PARK = new NavigationMapArea("Park");

	// This is ????
	private string areaName = "";
	// He is by the ????
	private string[] areaContents = null;

	public NavigationMapArea(string areaName) {
		this.areaName = areaName;
		this.areaContents = new string[0];
	}

	public NavigationMapArea(string areaName, params string[] areaContents) {
		this.areaName = areaName;
		this.areaContents = areaContents;
	}

	public string GetAreaName() {
		return areaName;
	}

	public string GetAreaContent(int index) {
		return areaContents[index];
	}

	public (int index, string content) GetRandomAreaContent() {
		int tmpIndex = Random.Range(0, areaContents.Length);
		return (tmpIndex, areaContents[tmpIndex]);
	}

	public static NavigationMapArea GetAreaByEnum(NavigationMapAreas value) {
		switch (value) {
			case NavigationMapAreas.PARK:
				return PARK;
			default:
				return NULL;
		}
	}

}

public enum NavigationMapAreas {
	PARK
}

