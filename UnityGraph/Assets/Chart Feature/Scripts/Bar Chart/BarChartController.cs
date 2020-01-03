using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarChartController : MonoBehaviour
{
    private ListDataSetJson dataSetJson;
    [SerializeField] private LineRenderer lineRendererPrefab;
    [SerializeField] private Image barPrefab;

    private void Start()
    {
        //DataSetListToJson();
        LoadDataSetList();
        GenerateChatBorder();
        GenerateBarChart();
    }

    private void LoadDataSetList()
    {
        //dataSetJson = JsonConvert.DeserializeObject<ListDataSetJson>("{\"ListDataSet\":[{\"ListChartData\":[{\"DataMark\":\"Jan\",\"DataValue\":1.0},{\"DataMark\":\"Feb\",\"DataValue\":5.0},{\"DataMark\":\"Mar\",\"DataValue\":2.0}],\"ColorR\":0.0,\"ColorG\":1.0,\"ColorB\":1.0,\"ColorA\":1.0,\"DataSetName\":\"Test 1\"}]}");
        dataSetJson = JsonConvert.DeserializeObject<ListDataSetJson>("{\"ListDataSet\":[{\"ListChartData\":[{\"DataMark\":\"Jan\",\"DataValue\":1.0},{\"DataMark\":\"Feb\",\"DataValue\":5.0},{\"DataMark\":\"Mar\",\"DataValue\":2.0}],\"ColorR\":0.0,\"ColorG\":1.0,\"ColorB\":1.0,\"ColorA\":1.0,\"DataSetName\":\"Test 1\"},{\"ListChartData\":[{\"DataMark\":\"Jan\",\"DataValue\":2.0},{\"DataMark\":\"Feb\",\"DataValue\":4.0},{\"DataMark\":\"Mar\",\"DataValue\":3.0}],\"ColorR\":0.0,\"ColorG\":1.0,\"ColorB\":0.0,\"ColorA\":1.0,\"DataSetName\":\"Test 2\"}]}");
    }

    private void DataSetListToJson()
    {
        dataSetJson = new ListDataSetJson();
        dataSetJson.ListDataSet = new List<DataSet>();
        dataSetJson.ListDataSet.Add(
            new DataSet
            {
                ColorR = 0,
                ColorG = 1,
                ColorB = 1,
                ColorA = 1,
                DataSetName = "Test 1",
                ListChartData = new List<ChartData>()
                {
                    new ChartData { DataValue = 1, DataMark = "Jan"},
                    new ChartData{ DataValue = 5, DataMark = "Feb"},
                    new ChartData{ DataValue = 2, DataMark = "Mar"}
                }
            });

        dataSetJson.ListDataSet.Add(
            new DataSet
            {
                ColorR = 0,
                ColorG = 1,
                ColorB = 0,
                ColorA = 1,
                DataSetName = "Test 2",
                ListChartData = new List<ChartData>()
                {
                    new ChartData { DataValue = 2, DataMark = "Jan"},
                    new ChartData{ DataValue = 4, DataMark = "Feb"},
                    new ChartData{ DataValue = 3, DataMark = "Mar"}
                }
            });

        string json = JsonConvert.SerializeObject(dataSetJson);
    }

    //private float minValue = 2100000000; // set tạm, thuật toán thay đổi sau này
    //private float maxValue = -2100000000;
    private int minValue = 2100000000;
    private int maxValue = -2100000000;
    private int ChartDataPoint = 0;
    private LineRenderer currentLineRenderer;
    private Image currentBar;
    private void GenerateBarChart()
    {
        // mặc định tất cả data đều >=0

        for (int i = 0; i < dataSetJson.ListDataSet.Count; i++)
        {
            for (int j = 0; j < dataSetJson.ListDataSet[i].ListChartData.Count; j++)
            {
                if (dataSetJson.ListDataSet[i].ListChartData[j].DataValue < minValue) minValue = (int)dataSetJson.ListDataSet[i].ListChartData[j].DataValue;
                if (dataSetJson.ListDataSet[i].ListChartData[j].DataValue > maxValue) maxValue = (int)dataSetJson.ListDataSet[i].ListChartData[j].DataValue;
            }

            ChartDataPoint = dataSetJson.ListDataSet[i].ListChartData.Count;
        }

        // tạo các cột dọc
        for (int i = 0; i <= ChartDataPoint; i++)
        {
            currentLineRenderer = Instantiate(lineRendererPrefab, transform);
            currentLineRenderer.positionCount = 2;
            currentLineRenderer.SetPosition(0, new Vector3(-xBorder + 2 * xBorder / ChartDataPoint * i, -yBorder, 0));
            currentLineRenderer.SetPosition(1, new Vector3(-xBorder + 2 * xBorder / ChartDataPoint * i, yBorder, 0));
        }

        // tạo các cột ngang
        // đang xét maxValue int và các hàng cách nhau 1
        for (int i = 0; i < maxValue + 1; i++)
        {
            currentLineRenderer = Instantiate(lineRendererPrefab, transform);
            currentLineRenderer.positionCount = 2;
            currentLineRenderer.SetPosition(0, new Vector3(-xBorder, -yBorder + 2 * yBorder / maxValue * i, 0));
            currentLineRenderer.SetPosition(1, new Vector3(xBorder, -yBorder + 2 * yBorder / maxValue * i, 0));
        }

        xSize = xCanvasBorder / ChartDataPoint; // chiều ngang chỉ bằng 1/2 của khối (cho dễ nhìn)
        ySize = 2 * yCanvasBorder / maxValue; // maxValue là int
        xSizeOfBar = xSize / dataSetJson.ListDataSet.Count;
        // tạo các cột bar
        for (int i = 0; i < dataSetJson.ListDataSet.Count; i++)
        {
            for (int j = 0; j < ChartDataPoint; j++)
            {
                currentBar = Instantiate(barPrefab, transform);
                currentBar.color = new Color(dataSetJson.ListDataSet[i].ColorR, dataSetJson.ListDataSet[i].ColorG, dataSetJson.ListDataSet[i].ColorB);
                currentBar.rectTransform.sizeDelta = new Vector2(xSizeOfBar, ySize * dataSetJson.ListDataSet[i].ListChartData[j].DataValue);

                currentBar.rectTransform.anchoredPosition = new Vector3(-xCanvasBorder + (j + 0.5f) * 2 * xSize - xSize / 2 + (i + 0.5f) * xSizeOfBar, -yCanvasBorder);
            }
        }

    }


    // đổi lại sau này tùy theo ratio
    private float xBorder, yBorder;
    private float xCanvasBorder, yCanvasBorder;
    private float xSize; // size ngang của 1 block (canvas)
    private float ySize; // size cao của 1 block (canvas)
    private float xSizeOfBar; // size ngang của 1 bar

    private void GenerateChatBorder()
    {
        xBorder = 5.5f;
        yBorder = 3f;

        xCanvasBorder = Screen.height / 10 * xBorder;
        yCanvasBorder = Screen.height / 10 * yBorder;
    }
}
