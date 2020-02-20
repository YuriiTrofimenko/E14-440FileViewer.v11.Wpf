using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.tyaa.e14_440fileviewernet.model.generics
{
    /*Модель "Канал" (generic)*/
    class Channel<T> where T: struct
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
        private List<T> mDataList;

        //public double?[] dataArray
        public List<T> dataArrayList
        {
            get { return mDataList; }
            set { mDataList = value; }
        }

        //Поля mNumber и mAmp инициализируем только в конструкторе
        public Channel(int _number, int _amp)
        {
            mNumber = _number;
            mAmp = _amp;
            //Сначала выделяем место в массиве только для одного элемента данных
            //mDataArray = new double?[1];
            mDataList = new List<T>();
        }

        //Добавление элемента данных в массив
        public void addDataItem(T _dataItem)
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
            mDataList.Add(_dataItem);
        }
        //Получение элемента данных из массива по его позиции
        public T getDataItem(int _dataItemPos)
        {
            return mDataList[_dataItemPos];
        }
        //Получение всего массива данных
        //public double?[] getDataArray()
        public List<T> getDataArray()
        {
            return mDataList;
        }
        //Получение длинны набора данных в массиве
        public int length()
        {
            //return mDataList.Length;
            return mDataList.Count;
        }
    }
}
