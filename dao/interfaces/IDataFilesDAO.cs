using org.tyaa.e14_440fileviewernet.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E14_440FileViewer.NET.interfaces
{
    interface IDataFilesDAO
    {
        //Считать все данные из файла в набор объектов "Канал"
        ChannelsBundle getChannels();
        //Считать из файла данные одного канала в объект "Канал"
        Channel getChannel(int _number);
        //Сохранить набор объектов "Канал" в новый файл данных
        void persistChannels(ChannelsBundle _сhannelsBoundle);
    }
}
