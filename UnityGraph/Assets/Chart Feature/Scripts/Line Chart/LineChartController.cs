using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChartController : MonoBehaviour
{
    private void Start()
    {
        
    }
}


// dùng cho Line và Bar Chart
[Serializable]
public class ChartData
{
    public string DataMark;
    public float DataValue;
}

[Serializable]
public class DataSet
{
    public List<ChartData> ListChartData;
    public float ColorR;
    public float ColorG;
    public float ColorB;
    public float ColorA;
    public string DataSetName;
}

public class ListDataSetJson
{
    public List<DataSet> ListDataSet;
}