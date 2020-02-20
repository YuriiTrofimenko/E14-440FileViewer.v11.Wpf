using E14_440FileViewer.NET.model.interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.tyaa.e14_440fileviewernet.model
{
    /*Модель "Пакет каналов"*/
    class ChannelsBundle : IEnumerable, IChannelsBundle
    {
        //Порядковые номера каналов
        private readonly int[] mNumbersArray;
        //Коды коэффициентов усиления каналов
        private readonly int[] mAmpsArray;
        //Частота записи (число значений каждого канала, сохраненных за секунду)
        //(общее число для всех канлов)
        private readonly int mFrequency;
        //Массив каналов
        //private Channel[] mChannelArray;
        private ArrayList mChannelArrayList;

        public ArrayList channelArrayList
        {
            get { return mChannelArrayList; }
            set { mChannelArrayList = value; }
        }

        //Поля mNumbersArray, mAmpsArray, mFrequency инициализируем только в конструкторе
        //public ChannelsBundle(int _length, int[] _numbersArray, int[] _ampsArray, int _frequency)
        public ChannelsBundle(int[] _numbersArray, int[] _ampsArray, int _frequency)
        {
            //mChannelArray = new Channel[_length];
            mChannelArrayList = new ArrayList();
            mNumbersArray = _numbersArray;
            mAmpsArray = _ampsArray;
            mFrequency = _frequency;
        }

        //Свойства для чтения защищенных от записи полей
        public int[] numbersArray
        {
            get { return mNumbersArray; }
        }
        public int[] ampsArray
        {
            get { return mAmpsArray; }
        }
        public int frequency
        {
            get { return mFrequency; }
        } 

        //Индексатор для получения каналов
        public Channel this[int _channelPos]
        {
            get
            {
                //if (_channelPos >= mChannelArray.Length || _channelPos < 0)
                if (_channelPos >= mChannelArrayList.Count || _channelPos < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return (Channel)mChannelArrayList[_channelPos];
                }
            }
            set
            {
                //if (mChannelArrayList.Capacity <= _channelPos)
                //{
                //    mChannelArrayList.Capacity = _channelPos + 1;
                //}
                //Console.WriteLine(mChannelArrayList.Capacity);
                //Console.WriteLine(_channelPos);
                //mChannelArrayList[_channelPos] = (Channel)value;
                mChannelArrayList.Add((Channel)value);
            }
        }

        //Перечислитель для массива каналов
        IEnumerator IEnumerable.GetEnumerator()
        {
            //return mChannelArray.GetEnumerator();
            //mChannelArrayList.
            return mChannelArrayList.GetEnumerator();
        }

        //Получение длинны набора данных в массиве
        public int length()
        {
            //return mChannelArray.Length;
            return mChannelArrayList.Count;
        }
    }
}
