using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Sensor> sensorList = Sensor.FillList(3);
            List<SensorData> warningTemperature = new List<SensorData>();
            SensorData[,] allData = new SensorData[10,3];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < sensorList.Count; j++)
                {
                    sensorList[j].SetRandomCurrentValue(0, 50);
                    if (sensorList[j].CurrentTemp < sensorList[j].MinTemp || sensorList[j].CurrentTemp > sensorList[j].MaxTemp)
                    {
                        warningTemperature.Add(new SensorData(sensorList[j]));
                    }
                    allData[i, j] = new SensorData(sensorList[j]);
                }
            }
        }
    }

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

        public void SetRandomCurrentValue(int minValue, int maxValue)
        {
            Random rnd = new Random();
            CurrentTemp = rnd.Next(minValue, maxValue);
        }
    }

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

