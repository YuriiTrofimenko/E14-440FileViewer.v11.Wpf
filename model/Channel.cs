using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.tyaa.e14_440fileviewernet.model
{
    /*Модель "Канал"*/
    struct Channel
    {
        //Порядковый номер 
        private readonly int mNumber;

        public int number
        {
            get { return mNumber; }
        } 

        //Код коэффициента усиления
        private readonly int mAmp;

        public int amp
        {
            get { return mAmp; }
        } 

        //Массив nullable-данных
        //private double?[] mDataArray;
        private ArrayList mDataArrayList;

        //public double?[] dataArray
        public ArrayList dataArrayList
        {
            get { return mDataArrayList; }
            set { mDataArrayList = value; }
        }

        //Поля mNumber и mAmp инициализируем только в конструкторе
        public Channel(int _number, int _amp)
        {
            mNumber = _number;
            mAmp = _amp;
            //Сначала выделяем место в массиве только для одного элемента данных
            //mDataArray = new double?[1];
            mDataArrayList = new ArrayList();
        }

        //Добавление элемента данных в массив
        public void addDataItem(double _dataItem)
        {
            //Если первая позиция уже заполнена
            //if (mDataArray[0] != null)
            //{
            //    //Создаем новый массив, на 1 место более объемный, чем текущий
            //    double?[] newDataArray = new double?[mDataArray.Length + 1];
            //    //Копируем значения из текущего массива в новый
            //    mDataArray.CopyTo(newDataArray, 0);
            //    //Делаем новый массив текущим
            //    mDataArray = newDataArray;
            //    //Добавляем значение в последнюю позицию в массиве
            //    mDataArray[mDataArray.Length - 1] = _dataItem;

            //}
            ////Если первая позиция в массиве еще не заполнена...
            //else
            //{
            //    //Заполняем ее значением
            //    mDataArray[0] = _dataItem;
            //}
            mDataArrayList.Add(_dataItem);
        }
        //Получение элемента данных из массива по его позиции
        public double? getDataItem(int _dataItemPos)
        {
            return (double?)mDataArrayList[_dataItemPos];
        }
        //Получение всего массива данных
        //public double?[] getDataArray()
        public ArrayList getDataArray()
        {
            return mDataArrayList;
        }
        //Получение длинны набора данных в массиве
        public int length()
        {
            //return mDataArrayList.Length;
            return mDataArrayList.Count;
        }
    }
}
