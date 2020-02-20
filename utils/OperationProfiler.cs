using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E14_440FileViewer.NET.utils
{
    internal sealed class OperationProfiler : IDisposable
    {
        private Int64 m_startTime;
        private String m_text;
        private Int32 m_collectionCount;

        //Объект наблюдения за оперативной памятью
        //private System.Diagnostics.PerformanceCounter mPerfCounter;
        //Измеренный объем памяти
        //private float m_startMem;

        public OperationProfiler(String text)
        {

            PrepareForOperation();

            m_text = text;

            //Сохранеяется количество сборок мусора, выполненное на текущий момент
            m_collectionCount = GC.CollectionCount(0);

            //Сохранеяется начальное время
            m_startTime = Stopwatch.GetTimestamp();

            //mPerfCounter = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");
            //m_startMem = mPerfCounter.NextValue();
        }

        /// <summary>
        /// Вызывается при разрушении объекта
        /// Выводит:
        /// значение времени, прошедшего с момента создания объекта до момента его удаления
        /// количество выполненных сборок мусора, выполненных за это время
        /// </summary>
        public void Dispose()
        {
            Console.WriteLine("{0,6:0.00} seconds (GCs={1,3}) {2} memory",
               (Stopwatch.GetTimestamp() - m_startTime) /
                  (Double)Stopwatch.Frequency,
               GC.CollectionCount(0) - m_collectionCount, m_text);
            //mPerfCounter.NextValue() - m_startMem)
        }
        /// <summary>
        ///Метод удаляются все неиспользуетме объекты
        ///Это надо для "чистоты эксперимента",
        ///т.е. чтобы сборка мусора происходила только для объектов,
        ///которые будут создаваться в профилируемом блоке кода
        ////// </summary>
        private static void PrepareForOperation()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
