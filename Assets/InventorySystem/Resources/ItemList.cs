using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ItemList : ScriptableObject
{
	public List<ItemData> Sheet1; // Replace 'EntityType' to an actual type that is serializable.
}
