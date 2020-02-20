using org.tyaa.e14_440fileviewernet.model.generics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace E14_440FileViewer.NET.dao.implements.NewDataFileDAO_Parts
{
    /*Класс создания списка объектов "Канал" с записью в них информации из файла параметров*/

    class ParamsReader
    {
        private List<Channel<double>> mChannelsArrayList;

        private int mCount;
        private int mFrequency;

        public int Count
        {
            get { return mCount; }
            //set { mCount = value; }
        }

        public int Frequency
        {
            get { return mFrequency; }
            //set { mFrequency = value; }
        }

        public List<Channel<double>> getParams(String _filePathString) {
            mChannelsArrayList = new List<Channel<double>>();
            //массив для параметров
            int[] paramsArray = new int[66];
            //вызов метода заполнения массива параметров
            getParamsCS(_filePathString, ref paramsArray);
            mCount = paramsArray[0];
            mFrequency = paramsArray[65];
            for (int i_channel = 1; i_channel <= mCount; i_channel++)
            {
                Channel<double> currentChannel =
                    new Channel<double>(
                        paramsArray[i_channel],
                        paramsArray[i_channel + 32]
                    );
                mChannelsArrayList.Add(currentChannel);
            }
            return mChannelsArrayList;
        }

        private void getParamsCS(String _filePathString, ref int[] _paramsArray)
        {

            int itemIdx = 0;
            using (BinaryReader reader = new BinaryReader(File.Open(_filePathString, FileMode.Open)))
            {
                // пока не достигнут конец файла -
                // считываем каждое значение из файла
                while (true)
                {
                    //
                    try
                    {
                        _paramsArray[itemIdx] = reader.ReadInt16();
                        itemIdx++;
                    }
                    //если достигнут конец файла - выходим из цикла, перехватив исключение
                    catch (EndOfStreamException ex)
                    {
                        
                        break;
                    }
                }
            }
        }

        [DllImport("E440ReadParams.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getParamsCPPNative(String _filePathString);
        //[return: MarshalAs(UnmanagedType.SafeArray)]
        //public static extern IntPtr getParamsCPPNative(String _filePathString);
        //[DllImport("E440ReadParams.dll")]
        //public static extern IntPtr getParamsCPPNative(String _filePathString, out UIntPtr _ptr);
        //public static extern IntPtr getParamsCPPNative(String _filePathString);

        //public static extern void getParamsCPPNative(String _filePathString, out UIntPtr _ptr);
        

        [DllImport("E440ReadParams.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void paramsCPPNativeRelease(IntPtr ptr);


        private void getParamsCPP(String _filePathString, ref int[] _paramsArray)
        {
            //UIntPtr ptr;
            //getParamsCPPNative(_filePathString, out ptr);
            //Marshal.Copy(ptr, _paramsArray, 0, 66);
            //paramsCPPNativeRelease(ptr);

            IntPtr ptr = getParamsCPPNative(_filePathString);
            Marshal.Copy(ptr, _paramsArray, 0, 66);
            paramsCPPNativeRelease(ptr);

            /*foreach (int _valueInt in _paramsArray)
            {
                Console.WriteLine(_valueInt);
            }*/
        }
    }
}
