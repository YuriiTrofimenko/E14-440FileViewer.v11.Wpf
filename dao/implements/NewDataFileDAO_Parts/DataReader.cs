using org.tyaa.e14_440fileviewernet.model.generics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E14_440FileViewer.NET.dao.implements.NewDataFileDAO_Parts
{
    /**/

    class DataReader
    {
        private int mCount;

        public void getData(String _filePathString, ref List<Channel<double>> _channelsArrayList) {

        int mCount = _channelsArrayList.Count;
        int channelIdx = 0;

            using (BinaryReader reader = new BinaryReader(File.Open(_filePathString, FileMode.Open)))
            {
                while (true)
                {
                    //
                    try
                    {
                        _channelsArrayList[channelIdx].addDataItem(reader.ReadInt16() * 10D / 8192D);
                        if (channelIdx == mCount - 1) {
                            channelIdx = 0;
                        } else {
                            channelIdx++;
                        }
                    }
                    catch (EndOfStreamException ex)
                    {
                        
                        break;
                    }
                }
            }
        }
    }
}
