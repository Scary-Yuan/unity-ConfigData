using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//读取文件
using System.IO;
//反射（type需要使用）
using System;
//反射
using System.Reflection;

public class ConfigData : SingleTon<ConfigData>
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    /// <param name="name">需要查找的物体的名称</param>
    /// <param name="obj">需要赋值的对象</param>
    /// <param name="path">读取文件的路径</param>
    public void ConfigDatas(string name, object obj, string path)
    {
        //反射得到需要查找的物体的类型
        Type type = obj.GetType();

        //得到需要配置的属性（出了自身写的属性还有自带的属性），只需要赋值自定义的属性
        PropertyInfo[] propertyInfos = type.GetProperties();

        //得到需要查找的对象的数据datas
        string[] datas = ReadData(path, name);

        //遍历属性的长度
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            //父类拥有一些公有 属性，子类继承父类时，通过反射得到的子类的属性是以 子类新增的属性为开始，
            //然后是父类的公有属性
            //最后是自带的属性
            //只需要赋值自定义的属性
            //定义配置文件的开头都为 xxxName,查找第一个以 Name 结尾的属性
            //得到的就是父类的第一个自定义的属性
            //得到子类自定义的属性的长度 ：i
            if (propertyInfos[i].Name.ToString().LastIndexOf("Name") == propertyInfos[i].Name.ToString().Length - 4 && propertyInfos[i].Name.ToString().LastIndexOf("Name") != -1)
            {
                //给属性赋值 从 i 开始赋值
                //即从父类的第一个自定义属性开始赋值，只需要赋值所有自定义属性长度减去子类自定义属性的长度
                //即 datas.length - i 
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
                //父类属性赋值完成后需要给子类的属性赋值
                //即 从 0 开始到 i 都是子类的属性
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
    /// <summary>
    /// 查找所需要的对象的数据
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="name">对象名称</param>
    /// <returns>该对象的数据 或 null</returns>
    public string[] ReadData(string path, string name)
    {
        //查找该路径下的数据
        string data = Read(path);

        //将改路径下的数据按‘/’分割成字符数组
        string[] datas = data.Split('/');

        //定义一个空字符数组储存所需的数据
        string[] temproryData = null;

        //遍历该路径下的所有数据
        for (int i = 0; i < datas.Length; i++)
        {
            //去除空格
            datas[i] = datas[i].Trim();

            //查找所需的数据
            if (datas[i].Split(',')[0].Trim() == name)
            {
                //赋值给temproryData
                temproryData = datas[i].Split(',');

                //去空格
                for (int j = 0; j < temproryData.Length; j++)
                {
                    temproryData[j] = temproryData[j].Trim();

                    //返回所需数据
                    return temproryData;
                }
            }
        }
        //如无查找的数据则返回null
        return null;
    }
    /// <summary>
    /// 读取文件所有数据
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>返回文件所有数据，类型为string</returns>
    private string Read(string path)
    {
        StreamReader sr = new StreamReader(path);

        string data = sr.ReadToEnd();

        sr.Close();

        return data;
    }
}
