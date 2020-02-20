using E14_440FileViewer.NET.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.tyaa.e14_440fileviewernet.model;

namespace E14_440FileViewer.NET.dao.implements
{
    class OldDataFileDAO : IDataFilesDAO
    {

        public ChannelsBundle getChannels()
        {
            //throw new NotImplementedException();
            Console.WriteLine("OldDataFileDAO: getChannels");
            return null;
        }

        public Channel getChannel(int _number)
        {
            throw new NotImplementedException();
        }

        public void persistChannels(ChannelsBundle _сhannelsBoundle)
        {
            throw new NotImplementedException();
        }
    }
}
