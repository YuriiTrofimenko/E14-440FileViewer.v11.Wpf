using org.tyaa.e14_440fileviewernet.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E14_440FileViewer.NET.model.interfaces
{
    internal interface IChannelsBundle
    {
        //Channel[] channelArray
        ArrayList channelArrayList
        {
            get;
            set;
        }
    }
}
