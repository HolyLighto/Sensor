using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            //количество датчиков
            int sensorCount = 3;
            //количество итераций для каждого из датчиков
            int iterationCount = 10;
            //Список(массив) датчиков, создаем 3 штуки
            List<Sensor> sensorList = Sensor.FillList(sensorCount); 
            //Список показаний датчиков, когда температура была предельной(сохраняется название датчика и температура. которая была)
            List<SensorData> warningTemperature = new List<SensorData>();
            //Двумерный массив, в котором будут храниться все показания. 3 датчика и 10 значений у каждого в данном случае
            SensorData[,] allData = new SensorData[iterationCount, sensorCount];

            //Делаем 10 итераций для каждого из 3х датчиков
            for (int i = 0; i < iterationCount; i++)
            {
                for (int j = 0; j < sensorList.Count; j++)
                {
                    //Меняем текущую температуру
                    sensorList[j].SetRandomCurrentValue(0, 50);
                    //Если температура больше или меньше предельной, 
                    if (sensorList[j].CurrentTemp < sensorList[j].MinTemp || sensorList[j].CurrentTemp > sensorList[j].MaxTemp)
                    {
                        //то записываем в массив с критическими температурами
                        warningTemperature.Add(new SensorData(sensorList[j]));
                    }
                    //и в конце в любом случае заполняем массив со всеми показаниями
                    allData[i, j] = new SensorData(sensorList[j]);
                }
            }
        }
    }

    /// <summary>
    /// Класс датчика с его параметрами
    /// </summary>
    public class Sensor
    {
        public int CurrentTemp { get; set; }
        public string Name { get; set; }
        public int MaxTemp { get; }
        public int MinTemp { get; }

        public Sensor(string name, int minTemp, int maxTemp)
        {
            CurrentTemp = 0;
            Name = name;
            MinTemp = minTemp;
            MaxTemp = maxTemp;
        }

        /// <summary>
        /// Создание списка(массива) датчиков
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Sensor> FillList(int count)
        {
            Random rnd = new Random();
            List<Sensor> list = new List<Sensor>();
            for(int i = 1; i < count+1; i++)
            {
                list.Add(new Sensor($"Датчик{i}", rnd.Next(0, 10), rnd.Next(30, 50)));
            }
            return list;
        }

        /// <summary>
        /// Меняем текущее значение датчика на рандомное
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public void SetRandomCurrentValue(int minValue, int maxValue)
        {
            Random rnd = new Random();
            CurrentTemp = rnd.Next(minValue, maxValue);
        }
    }

    /// <summary>
    /// Класс, в котором содержатся данные из датчика, используется для сохранения данных
    /// </summary>
    public class SensorData
    {
        public string Name;
        public int Temp;

        public SensorData(Sensor sens)
        {
            Name = sens.Name;
            Temp = sens.CurrentTemp;
        }
    }
}

