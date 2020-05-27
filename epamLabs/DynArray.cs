using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epamLabs
{

    class DynArray<T>
    {
        private T[] array;
        public int Capacity
        {
            get => array.Length;
            set
            {
                if (value >= Capacity)
                {
                    AddCapacity(value - Capacity);
                }
                else
                {
                    throw new Exception("Can't decrease array");
                }
            }
        }
        private int length;
        public int Length
        {
            get=>length;
            set {
                if (value > Capacity || value < 0)
                {
                    throw new IndexOutOfRangeException("Length out of bounds");
                }
                else {
                    length = value;
                }
            }
        }


        public DynArray()
        {
            array = new T[8];
            Length = 0;
        }
        public DynArray(int cap)
        {
            array = new T[cap];
            Length = 0;
        }
        public DynArray(T[] initArray)
        {
            array = new T[initArray.Length];
            Length = initArray.Length;
            for (int i = 0; i < initArray.Length; i++)
            {
                array[i] = initArray[i];
            }
        }
        public void Add(T element)
        {
            if (Length < Capacity)
            {
                array[Length] = element;
            }
            else
            {
                AddCapacity(Capacity);
                array[Length] = element;
            }
            Length++;
        }

        public void AddRange(T[] elements)
        {
            if (Length + elements.Length < Capacity)
            {
                /*for (int i = 0; i < elements.Length; i++)
                {
                    array[Length] = elements[i];
                    Length++;
                }*/
                elements.CopyTo(array, this.Length);
                Length += elements.Length;
            }
            else
            {
                AddCapacity(elements.Length-(Capacity-Length));
                elements.CopyTo(array, this.Length);
                Length += elements.Length;
            }

        }
        public bool Remove(T element) //Удаление первого вхождения элемента
        {
            for (int i = 0; i < Length; i++)
            {
                if (array[i].Equals(element))
                {
                    Length--;
                    T[] localArr = new T[Capacity];//массив с тем же capacity
                    /*for (int j = 0; j < i; j++)//добавляем элементы до первого вхождения
                    {
                        localArr[j] = array[j];
                    }
                    for (int j = i + 1; j < Length + 1; j++)//добавляем элементы поле вхождения
                    {
                        localArr[j - 1] = array[j];
                    }*/
                    Array.Copy(array, 0, localArr, 0, i);
                    Array.Copy(array, i + 1, localArr, i, this.Length - i);
                    array = localArr;
                    return true;
                }
            }
            return false;
        }

        /// <exception cref="ArgumentOutOfRangeException">Может выбросить выход за массив</exception>
        public void Insert(T element, int position) //вставить element в position
        {
            if ((position > Capacity) || (position < 0))
            {
                throw new IndexOutOfRangeException("ArgumentOutOfRangeException");
            }
            else
            {
                if (Length<Capacity)
                {
                    for (int i = Length; i >= position; i--) {//переставляем все элементы с конца на 1 позицию вправо
                        array[i + 1] = array[i];
                    }
                    array[position] = element;
                }
                else
                {
                    AddCapacity(1);
                    for (int i = Length-1; i >= position; i--)
                    {
                        array[i + 1] = array[i];
                    }
                    array[position] = element;
                }
                if (Length < position)
                {
                    length = position+1;
                }
                else {
                    Length++;
                }
                
            }

        }

        public override string ToString()
        {
            String arrayStr = "";
            for (int i = 0; i < array.Length; i++)
            {
                arrayStr += array[i].ToString() + " ";
            }
            return arrayStr +"Cap:"+ Capacity + " Len:" + Length;
        }

        /// <exception cref="ArgumentOutOfRangeException">Может выбросить выход за массив</exception>
        public T this[int i]
        {
            get
            {
                if ((i >= Length) || (i < 0))
                {
                    throw new IndexOutOfRangeException("ArgumentOutOfRangeException");
                }
                else return array[i];
            }
            set
            {
                if ((i >= Length) || (i < 0))
                {
                    throw new IndexOutOfRangeException("ArgumentOutOfRangeException");
                }
                else array[i] = value;
            }
        }

        private void AddCapacity(int cap) {
            T[] tempArray =array;
            array = new T[array.Length+cap];
            for (int i = 0; i < tempArray.Length; i++)
            {
                array[i] = tempArray[i];
            }
        }
        //делегаты
        public void Filter(Func<T, bool> isRight) {
            int last_index=0;
            for (int i = 0; i < this.Length; i++)
            {
                if (isRight(array[i])) {
                    array[last_index] = array[i];
                    last_index++;
                }
            }
            for (int i = last_index+1; i < this.Length; i++)
            {
                array[i] = default;
            }
            this.Length = ++last_index;
        }



        public void Sort(Func<T,T,int> compare) {
            for (int i = 0; i < this.Length; i++)
            {
                for (int j = 0; j < this.Length; j++)
                {
                    if (compare(array[i], array[j]) == 1) {
                        T temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
        public int compareTwoInt(int item1,int item2) {
            if (item1 < item2) return -1;
            if (item1 == item2) return 0;
            if (item1 > item2) return 1;
            throw new Exception("??");
        }
    }
}
