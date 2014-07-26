using System;
using System.Diagnostics;
using System.Threading;

namespace MusicPopulation
{
    public class AreaThread
    {
        private int _indexOfArea;
        //private ManualResetEvent _doneEvent;

        public AreaThread(int index/*, ManualResetEvent doneEvent*/)
        {
            _indexOfArea = index;
            //_doneEvent = doneEvent;
        }

        public void EvolvePart1(/*Object threadContext*/)
        {
            /*try
            {
                Debug.WriteLine("Thread {0} Part 1 started...", _indexOfArea);*/

                Simulation.Areas[_indexOfArea].KillWeaksWhoDoesNotServeTheEmperorWell();
                Simulation.Areas[_indexOfArea].SelectChampionWhoCanBecomeCommissar();
                Simulation.Areas[_indexOfArea].ReproduceMenToHaveMoreServantsOfTheEmperor();
                Simulation.Areas[_indexOfArea].MutateWeaksSoTheyCanServeEmperorBetter();
                Simulation.Areas[_indexOfArea].InfluenceMenWithSongsGlorifyingEmperor();
                Simulation.Areas[_indexOfArea].MoveYourMenSergant();

            /*     Debug.WriteLine("Thread {0} Part 1 calculated...", _indexOfArea);

                 if (_doneEvent != null)
                 {
                     _doneEvent.Set();
                 }
             }
             catch (Exception ex)
             {
                 //Console.WriteLine("kd");
                 //Logger.AddError(ex, null);
             }*/
        }

        public void EvolvePart2(/*Object threadContext*/)
        {
            /*try
            {
                Debug.WriteLine("Thread {0} Part 2 started...", _indexOfArea);*/

                Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(0);

          /*      Debug.WriteLine("Thread {0} Part 2 calculated...", _indexOfArea);

                if (_doneEvent != null)
                {
                    _doneEvent.Set();
                }
            }
            catch(Exception ex)
            {
                //Logger.AddError(ex, null);
            }*/
        }

        public void EvolvePart3(/*Object threadContext*/)
        {
            /*try
            {
                Debug.WriteLine("Thread {0} Part 3 started...", _indexOfArea);*/

                Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(1);

           /*     Debug.WriteLine("Thread {0} Part 3 calculated...", _indexOfArea);

                if (_doneEvent != null)
                {
                    _doneEvent.Set();
                }
            }
            catch (Exception ex)
            {
                //Logger.AddError(ex, null);
            }*/
        }

        public void EvolvePart4(/*Object threadContext*/)
        {
            /*try
            {
                Debug.WriteLine("Thread {0} Part 4 started...", _indexOfArea);*/

            Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(2);

            /*     Debug.WriteLine("Thread {0} Part 4 calculated...", _indexOfArea);

                 if (_doneEvent != null)
                 {
                     _doneEvent.Set();
                 }
             }
             catch (Exception ex)
             {
                 //Logger.AddError(ex, null);
             }*/
        }

        public void EvolvePart5(/*Object threadContext*/)
        {
            /*try
            {
                Debug.WriteLine("Thread {0} Part 5 started...", _indexOfArea);*/

                Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(3);

            /*    Debug.WriteLine("Thread {0} Part 5 calculated...", _indexOfArea);

                if (_doneEvent != null)
                {
                    _doneEvent.Set();
                }
            }
            catch(Exception ex)
            {
                //Logger.AddError(ex, null);
            }*/
        }
    }
}
