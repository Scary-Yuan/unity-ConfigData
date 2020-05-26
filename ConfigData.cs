using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Reflection;

public class ConfigData : SingleTon<ConfigData>
{
    public void ConfigDatas(string name, object obj, string path)
    {
        Type type = obj.GetType();

        PropertyInfo[] propertyInfos = type.GetProperties();

        string[] datas = ReadData(path, name);

        //遍历属性的长度
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            //得到新增的属性的长度 ：i
            if (propertyInfos[i].Name.ToString().LastIndexOf("Name") == propertyInfos[i].Name.ToString().Length - 4 && propertyInfos[i].Name.ToString().LastIndexOf("Name") != -1)
            {
                //给属性赋值 从 i 开始赋值
                for (int j = 0; j < datas.Length - i; j++)
                {
                    switch (propertyInfos[i + j].PropertyType.ToString())
                    {
                        case "System.Byte":
                            propertyInfos[i + j].SetValue(obj, Byte.Parse(datas[j].Trim()));
                            break;
                        case "System.Int":
                            propertyInfos[i + j].SetValue(obj, int.Parse(datas[j].Trim()));
                            break;
                        case "System.Float":
                            propertyInfos[i + j].SetValue(obj, float.Parse(datas[j].Trim()));
                            break;
                        case "System.Boolean":
                            propertyInfos[i + j].SetValue(obj, bool.Parse(datas[j].Trim()));
                            break;
                        case "System.String":
                            propertyInfos[i + j].SetValue(obj, datas[j].Trim());
                            break;
                        default:
                            break;
                    }
                }
                for (int m = 0; m < i; m++)
                {
                    switch (propertyInfos[m].PropertyType.ToString())
                    {
                        case "System.Byte":
                            propertyInfos[m].SetValue(obj, Byte.Parse(datas[datas.Length - i + m].Trim()));
                            break;
                        case "System.Int":
                            propertyInfos[m].SetValue(obj, int.Parse(datas[datas.Length - i + m].Trim()));
                            break;
                        case "System.Float":
                            propertyInfos[m].SetValue(obj, float.Parse(datas[datas.Length - i + m].Trim()));
                            break;
                        case "System.Boolean":
                            propertyInfos[m].SetValue(obj, bool.Parse(datas[datas.Length - i + m].Trim()));
                            break;
                        case "System.String":
                            propertyInfos[m].SetValue(obj, datas[datas.Length - i + m].Trim());
                            break;
                        default:
                            break;
                    }
                }
                break;
            }
        }
    }
    public string[] ReadData(string path, string name)
    {
        string data = Read(path, name);

        string[] datas = data.Split('/');

        string[] temproryData = null;

        for (int i = 0; i < datas.Length; i++)
        {
            datas[i] = datas[i].Trim();

            if (datas[i].Split(',')[0].Trim() == name)
            {
                temproryData = datas[i].Split(',');

                for (int j = 0; j < temproryData.Length; j++)
                {
                    temproryData[j] = temproryData[j].Trim();

                    return temproryData;
                }
            }
        }

        return null;
    }

    private string Read(string path, string name)
    {
        StreamReader sr = new StreamReader(path);

        string data = sr.ReadToEnd();

        sr.Close();

        return data;
    }
}
